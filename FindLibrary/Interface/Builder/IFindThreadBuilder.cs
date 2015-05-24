using FindLibrary.Builder;
using FindLibrary.FThread;

namespace FindLibrary.FindThreadBuilder
{
	public interface IFindThreadBuilder : IBuilder<IFindThread>
	{
		IFindThreadBuilder SetValueToFind( uint valueToFind );
		IFindThreadBuilder SetDelay( uint delay );
		IFindThreadBuilder SetName( string name );
	}
}
