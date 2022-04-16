namespace UnderstandingDI
{
	public interface IServiceA
	{
		IServiceB ServiceB { get; set; }
		
		string jobA();
	}
}