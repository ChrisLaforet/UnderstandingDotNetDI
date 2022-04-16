namespace UnderstandingDI
{
	public class ServiceA : IServiceA
	{
		[Inject] 
		public IServiceB ServiceB { get; set; }

		public string jobA()
		{
			return "jobA(" + this.ServiceB.jobB() + ")";
		}
	}
}