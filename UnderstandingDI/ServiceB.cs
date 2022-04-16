namespace UnderstandingDI
{
	public class ServiceB : IServiceB
	{
		public IServiceA ServiceA { get; set; }

		public string jobB()
		{
			return "jobB()";
		}
	}
}