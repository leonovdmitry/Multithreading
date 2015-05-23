using FindLibrary.FThread;
using System;

namespace FindLibrary.Builders
{
	public interface IFindThreadBuilder : IBuilder<IFindThread>
	{
		IFindThreadBuilder SetValueToFind( uint valueToFind );
		IFindThreadBuilder SetDelay( uint delay );
		IFindThreadBuilder SetName( string name );
		IFindThreadBuilder SetFindAction( Action findAction );
	}
}
