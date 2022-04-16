using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnderstandingDI
{
	public class UnderstandingDI
	{
		static void Main(string[] args)
		{
			var context = CreateContextForAssembly();
			DoBusinessLogic(context);
		}

		private static DIContext CreateContextForAssembly()
		{
			return new DIContext(AssemblyScanner.GetAllServiceClassesInCurrentExecutingAssembly());
		}

		private static void DoBusinessLogic(DIContext context)
		{
			var serviceA = context.GetServiceInstance<IServiceA>();
			var serviceB = context.GetServiceInstance<IServiceB>();
			Console.WriteLine(serviceA.jobA());
			Console.WriteLine(serviceB.jobB());
		}
	}
}