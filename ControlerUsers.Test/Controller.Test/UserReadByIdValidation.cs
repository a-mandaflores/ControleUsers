using Bogus;
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
    public class UserReadByIdValidation
    {
        [Fact]
        public async Task RetornaUsuarioQuandoSolicitacaoCorreta()
        {
            //arrange
            var faker = new Faker<UserRead>()
               .RuleFor(n => n.Name, f => f.Name.FirstName())
               .RuleFor(e => e.Email, f => f.Internet.Email())
               .RuleFor(i => i.Idade, f => f.Random.Int(18, 100));

            var fakerUser = faker.Generate();

            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetById(1))
                .ReturnsAsync(fakerUser);

            var userController = new UserControllers(mockService.Object);

            //act

            var result = await userController.GetById(1);

            //assert
            var statusResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserRead>(statusResult.Value);
            Assert.NotNull(statusResult);
            Assert.Equal(fakerUser.Name, returnValue.Name);
            Assert.Equal(fakerUser.Email, returnValue.Email);
            Assert.Equal(fakerUser.Idade, returnValue.Idade);
        }

        [Fact]
        public async Task RetornNotFoundQuandoUsuarioNaoEncontrado()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetById(2))
                .ReturnsAsync((UserRead)null);

            var userController = new UserControllers(mockService.Object);

            //act
            var result = await userController.GetById(1);

            //assert
            var statusResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, statusResult.StatusCode);
        }

    }
}
