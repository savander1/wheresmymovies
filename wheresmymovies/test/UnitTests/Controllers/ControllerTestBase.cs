
namespace test.UnitTests.Controllers
{
    public abstract class ControllerTestBase
    {
        protected const string ID = "tt123456789";
        protected const string TITLE = "Marvelous Movie";
        protected const int BAD_REQUEST = 400;

        protected abstract void TestInitialize();
    }
}
