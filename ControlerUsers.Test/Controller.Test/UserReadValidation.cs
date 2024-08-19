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
    public class UserReadValidation
    {
        [Fact]
        public async Task RetornaNullQuandoListaEstaVazia()
        {
            //arrange
            var users = Enumerable.Empty<UserRead>();

            var mockUsers = new Mock<IUserService>();
            mockUsers.Setup(service => service.GetAll())
                .ReturnsAsync(users);

            var userController = new UserControllers(mockUsers.Object);
            //act

            var result = await userController.GetAll();

            //assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<UserRead>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task RetornaTodosOsElementosDaLista()
        {
            //arrange
            var faker = new Faker<UserRead>()
                .RuleFor(n => n.Name, f => f.Name.FirstName())
                .RuleFor(e => e.Email, f => f.Internet.Email())
                .RuleFor(i => i.Idade, f => f.Random.Int(18, 100));

            var fakerUsers = faker.Generate(100);

            var mockService = new Mock<IUserService>();


            mockService.Setup(service => service.GetAll())
                .ReturnsAsync(fakerUsers);
            var controller = new UserControllers(mockService.Object);

            //act

            var result = await controller.GetAll();

            //assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<UserRead>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<UserRead>>(okResult.Value);
            Assert.Equal(fakerUsers.Count, returnValue.Count());

        }

        [Fact]
        public async Task RetornaBadRequestQuandoOcorreAlgumErro()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetAll())
                .ThrowsAsync(new Exception("Test exception"));

            var userController = new UserControllers(mockService.Object);

            // Act
            var actionResult = await userController.GetAll();

            // Assert
            var result = Assert.IsType<ActionResult<IEnumerable<UserRead>>>(actionResult);
            var objectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
            Assert.Equal("Exception", objectResult.Value);
        }
    }
}
