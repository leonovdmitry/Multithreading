using FindLibrary.FThread;

namespace FindLibrary.Builders
{
	public interface IFindThreadBuilder : IBuilder<IFindThread>
	{
		IFindThreadBuilder SetValueToFind( uint valueToFind );
		IFindThreadBuilder SetDelay( uint delay );
		IFindThreadBuilder SetName( string name );
	}
}
