namespace UnderstandingDI
{
	public interface IServiceB
	{
		IServiceA ServiceA { get; set; }
		
		string jobB();
	}
}