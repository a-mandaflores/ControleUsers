using Bogus;
using ControleUsers.DTOs;
using ControleUsers.Service.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlerUsers.Test.Controller.Test
{
    public class UserDeleteValidation
    {
        [Fact]
        public async Task RetornaOkResultSeDeletarComSucesso()
        {
            //assert
            var faker = new Faker<UserPost>()
              .RuleFor(n => n.Name, f => f.Name.FirstName())
              .RuleFor(e => e.Email, f => f.Internet.Email())
              .RuleFor(i => i.Idade, f => f.Random.Int(18, 100));


            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.Delete(1))
                .Returns(Task.CompletedTask);


                
        }
    }
}
