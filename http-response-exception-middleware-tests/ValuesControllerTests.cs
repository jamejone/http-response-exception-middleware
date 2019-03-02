using http_response_exception_middleware.Controllers;
using http_response_exception_middleware.Exceptions;
using Xunit;

namespace http_response_exception_middleware_tests
{
    public class ValuesControllerTests
    {
        [Fact]
        public void Get_ThrowsException()
        {
            var controller = new ValuesController();

            var exception = Assert.Throws<BadRequestException>(() => controller.Get());
        }

        [Fact]
        public void GetById_ThrowsException()
        {
            var controller = new ValuesController();

            Assert.Throws<NotFoundException>(() => controller.Get(12));
        }
    }
}
