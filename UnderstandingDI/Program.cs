using System;

namespace UnderstandingDI
{
	public class UnderstandingDI
	{
		static void Main(string[] args)
		{
			var serviceB = new ServiceB();
			var serviceA = new ServiceA();

			serviceA.ServiceB = serviceB;
			serviceB.ServiceA = serviceA;
			
			Console.WriteLine(serviceA.jobA());
		}
	}
}