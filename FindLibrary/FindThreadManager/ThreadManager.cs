using FindLibrary.Builders;
using FindLibrary.FThread;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindLibrary.FindThreadManager
{
	public class ThreadManager : IThreadManager
	{
		public ThreadManager( IFindThreadBuilder builder )
		{
			_builder = builder;
		}

		List<IFindThread> _workThreads;
		IFindThreadBuilder _builder;
		uint _valueToFind;

		public void Init( uint valueToFind, uint threadsCount, uint delay )
		{
			_valueToFind = valueToFind;
			_workThreads = new List<IFindThread>();
			for( uint i = 1; i <= threadsCount; i++ )
			{
				_workThreads.Add( _builder.SetValueToFind( valueToFind ).SetDelay( delay ).SetName( "Поток " + i ).Build() );
			}
		}

		public void Start( Action<Result> showResultAct )
		{
			_workThreads.ForEach( t => t.Start( showResultAct ) );
		}

		public void Stop()
		{
			_workThreads.ForEach( t => t.Stop() );
		}

		public bool IsAborted
		{
			get { return _workThreads.Any( t => t.IsAborted ); }
		}
	}
}
