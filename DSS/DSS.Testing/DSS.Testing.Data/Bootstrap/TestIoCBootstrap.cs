using DSS.Bootstrap.IoC.Config;

namespace DSS.Testing.Data.Bootstrap
{
    public  static class TestIoCBootstrap
    {
        public static void Bootstrap()
        {
            IoC.Initialize();
        }
    }
}
