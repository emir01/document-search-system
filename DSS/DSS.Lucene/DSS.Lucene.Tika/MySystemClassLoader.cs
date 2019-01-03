using ikvm.runtime;
using java.lang;

namespace DSS.Lucene.Tika
{
	public class MySystemClassLoader : ClassLoader
	{
		public MySystemClassLoader(ClassLoader parent)
			: base(new AppDomainAssemblyClassLoader(typeof(MySystemClassLoader).Assembly))
		{
		}
	}
}