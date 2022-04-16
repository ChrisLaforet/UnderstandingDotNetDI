namespace UnderstandingDI
{
	public class ServiceA
	{

		private ServiceB serviceB;

		public ServiceA(ServiceB serviceB) => this.serviceB = serviceB;
		
		public string jobA()
		{
			return "jobA(" + serviceB.jobB() + ")";
		}
	}
}