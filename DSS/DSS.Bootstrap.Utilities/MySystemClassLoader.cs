using ikvm.runtime;
using java.lang;

namespace DSS.Bootstrap.Utilities
{
	public class MySystemClassLoader : ClassLoader
	{
		public MySystemClassLoader(ClassLoader parent)
			: base(new AppDomainAssemblyClassLoader(typeof(MySystemClassLoader).Assembly))
		{
		}
	}
}