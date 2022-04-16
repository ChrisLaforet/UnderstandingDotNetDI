namespace UnderstandingDI
{
	public class ServiceA : IServiceA
	{

		private IServiceB serviceB;

		public ServiceA(IServiceB serviceB) => this.serviceB = serviceB;
		
		public string jobA()
		{
			return "jobA(" + serviceB.jobB() + ")";
		}
	}
}