namespace UnderstandingDI
{
	public class ServiceA : IServiceA
	{
		public IServiceB ServiceB { get; set; }

		public string jobA()
		{
			return "jobA(" + this.ServiceB.jobB() + ")";
		}
	}
}