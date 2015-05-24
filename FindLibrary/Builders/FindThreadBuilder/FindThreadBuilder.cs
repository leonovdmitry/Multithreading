using FindLibrary.FThread;
using System;

namespace FindLibrary.Builders
{
	public class FindThreadBuilder : IFindThreadBuilder
	{
		private uint _valueToFind;
		private uint _delay;
		private string _name;
		private Action _findAction;

		public IFindThreadBuilder SetValueToFind( uint valueToFind )
		{
			_valueToFind = valueToFind;
			return this;
		}

		public IFindThreadBuilder SetDelay( uint delay )
		{
			_delay = delay;
			return this;
		}

		public IFindThreadBuilder SetName( string name )
		{
			_name = name;
			return this;
		}

		public IFindThread Build()
		{
			IFindThread fThread = new FindThread();
			fThread.Init( _valueToFind, _delay, _name );
			return fThread;
		}
	}
}
