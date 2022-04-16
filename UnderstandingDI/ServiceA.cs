namespace UnderstandingDI
{
	public class ServiceA
	{
		public static string jobA()
		{
			return "jobA(" + ServiceB.jobB() + ")";
		}
	}
}