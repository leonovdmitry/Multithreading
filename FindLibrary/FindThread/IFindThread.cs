using System;

namespace FindLibrary.FThread
{
	public interface IFindThread
	{
		uint ValueToFind { get; }
		uint Delay { get; }
		bool IsAborted { get; }
		Action<Result> ShowResultAct { get; }
		void Init( uint valueToFind, uint delay, string name );
		void Start( Action<Result> showResultAct );
		void Stop();
	}
}
