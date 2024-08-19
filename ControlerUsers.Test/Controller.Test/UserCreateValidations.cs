using Bogus;
using ControlerUsers.Models;
using ControleUsers.Controllers.User;
using ControleUsers.DTOs;
using ControleUsers.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlerUsers.Test.Controller.Test
{

    public class UserCreateValidations
    {
        [Fact]
        public async Task RetornaStatusOkQuandoCriadoComSucesso()
        {
            //arrange
            var faker = new Faker<UserPost>()
                .RuleFor(n => n.Name, f => f.Name.FirstName())
                .RuleFor(e => e.Email, f => f.Internet.Email())
                .RuleFor(i => i.Idade, f => f.Random.Int(18, 100));

            var fakerUser = faker.Generate();

            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.Create(It.IsAny<UserPost>()))
                .ReturnsAsync(new UserPost { Id = 1, Name = fakerUser.Name, Email = fakerUser.Email });

            var userController = new UserControllers(mockService.Object);

            //act
            var result = await userController.Create(fakerUser);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UserPost>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(fakerUser.Name, returnValue.Name);
            Assert.Equal(fakerUser.Email, returnValue.Email);
            Assert.Equal(fakerUser.Idade, returnValue.Idade);
        }

        [Theory]
        [InlineData("", "invalid-email", 17, 3)]
        [InlineData("Valid Name", "valid.email@example.com", 10, 1)]
        [InlineData("Valid Name", "valid.email_example.com", 30, 1)]
        [InlineData("", "valid.email@example.com", 30, 1)]
        public async Task RetornBadRequestQuandoModeloInvalido(
            string name,
            string email,
            int idade,
            int expectedErrorCount
            )
        {
            //arrange
            var user = new UserPost
            {
                Name = name,
                Email = email,
                Idade = idade
            };

            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.Create(It.IsAny<UserPost>()))
                .ReturnsAsync(new UserPost { Id = 1, Name = user.Name, Email = user.Email });

            var userController = new UserControllers(mockService.Object);

            if (string.IsNullOrWhiteSpace(name))
            {
                userController.ModelState.AddModelError("Name", "Nome é obrigatório");
            }
            if (!IsValidEmail(email))
            {
                userController.ModelState.AddModelError("Email", "Email inválido");
            }
            if (idade < 18 || idade > 120)
            {
                userController.ModelState.AddModelError("Idade", "Idade deve ser entre 18 e 120");
            }

            //act
            var result = await userController.Create(user);

            //assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.Equal(expectedErrorCount, modelState.Count);
        }

        private bool IsValidEmail(string email)
        {
            // Implementar a validação de email
            return email.Contains("@");
        }

    }
}
