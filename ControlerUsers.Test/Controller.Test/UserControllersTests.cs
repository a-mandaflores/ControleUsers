using ControleUsers.Controllers.User;
using ControleUsers.DTOs;
using ControleUsers.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControlerUsers.Test.Controller.Test;

public class UserControllersTests
{
    [Fact]
    public async Task Should_sucess_when_model_is_valid()
    {
        var service = new Mock<IUserService>();

        var controller = new UserControllers(service.Object);

        var user = new UserPost();

        var result = await controller.Create(user);

        service.Verify(x => x.Create(It.Is<UserPost>(x => x == user)), Times.Once());
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Should_sucess_when_model_is_invalid()
    {
        var service = new Mock<IUserService>();

        var controller = new UserControllers(service.Object);
        controller.ModelState.AddModelError("FakeError", "");

        var result = await controller.Create(new());

        service.Verify(x => x.Create(It.IsAny<UserPost>()), Times.Never());
        Assert.IsType<BadRequestObjectResult>(result);
    }
}