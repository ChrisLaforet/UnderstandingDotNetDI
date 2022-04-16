namespace UnderstandingDI
{
	public class ServiceB : IServiceB
	{
		[Inject]
		public IServiceA ServiceA { get; set; }

		public string jobB()
		{
			return "jobB()";
		}
	}
}