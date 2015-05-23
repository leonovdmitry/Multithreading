using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindLibrary.FindThreadManager
{
	public interface IThreadManager
	{
		bool IsAborted { get; }
		void Init( uint valueToFind, uint threadsCount, uint delay );
		void Start( Action<string> showResultAct );
		void Stop();
	}
}
