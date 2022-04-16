using System;

namespace UnderstandingDI
{
	public class UnderstandingDI
	{
		static void Main(string[] args)
		{
			var serviceB = new ServiceB();
			var serviceA = new ServiceA(serviceB);
			
			Console.WriteLine(serviceA.jobA());
		}
	}
}