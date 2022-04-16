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
			var context = CreateContext();
			DoBusinessLogic(context);
		}

		private static DIContext CreateContext()
		{
			var serviceClasses = new HashSet<Type>();
			serviceClasses.Add(typeof(ServiceA));
			serviceClasses.Add(typeof(ServiceB));
			return new DIContext(serviceClasses);
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