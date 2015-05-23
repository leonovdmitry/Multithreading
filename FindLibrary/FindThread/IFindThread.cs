using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindLibrary.FThread
{
	public interface IFindThread
	{
		uint ValueToFind { get; }
		uint Delay { get; }
		bool IsAborted { get; }
		Action<String> ShowResultAct { get; }
		Action StopAllAct { get; }

		void Init( uint valueToFind, uint delay, string name );
		void Start( Action<string> showResultAct, Action stopAllAct );
		void Stop();
	}
}
