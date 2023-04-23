using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyChange.Controllers;
using Xunit;

namespace ToyChange.Tests.ControllersTest
{
    public class ControllerTests
    {
        [Fact]
        public void Index_Page_Test()
        {
            var controller = new HomeController(new NullLogger<HomeController>());
            var result = controller.Index();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_Page_Test()
        {
            var controller = new HomeController(new NullLogger<HomeController>());
            var result = controller.Privacy();
            Assert.NotNull(result);
        }
    }
}
