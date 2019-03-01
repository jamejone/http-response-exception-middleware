using Microsoft.VisualStudio.TestTools.UnitTesting;
using http_response_exception_middleware.Controllers;
using http_response_exception_middleware.Exceptions;

namespace http_response_exception_middleware_tests
{
    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void Get_ThrowsException()
        {
            var controller = new ValuesController();

            controller.Get();
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetById_ThrowsException()
        {
            var controller = new ValuesController();

            controller.Get(12);
        }
    }
}
