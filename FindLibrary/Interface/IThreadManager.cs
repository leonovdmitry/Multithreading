using System;

namespace FindLibrary.FindThreadManager
{
	public interface IThreadManager
	{
		bool IsAborted { get; }
		void Init( uint valueToFind, uint threadsCount, uint delay );
		void Start( Action<Result> showResultAct );
		void Stop();
	}
}
