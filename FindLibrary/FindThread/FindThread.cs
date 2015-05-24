using System;
using System.Text;
using System.Threading;

namespace FindLibrary.FThread
{
	public class FindThread : IFindThread
	{
		public void Init( uint valueToFind, uint delay, string name )
		{
			ValueToFind = valueToFind;
			Delay = delay;
			_workThread = new Thread( new ThreadStart( find ) );
			_workThread.Name = name;
		}

		private Thread _workThread;
		private Action<Result> _showResultAct;

		public uint ValueToFind { get; private set; }
		public uint Delay { get; private set; }
		public bool IsAborted { get; private set; }
		public Action<Result> ShowResultAct { get { return _showResultAct; } }

		public void Start( Action<Result> showResultAct )
		{
			_showResultAct = showResultAct;
			_workThread.Start();
		}

		public void Stop()
		{
			_workThread.Abort();
			IsAborted = true;
		}

		private void find()
		{
			Random rand = new Random();
			while( true )
			{

				Result result = new Result();
				Thread.Sleep( 10 );
				var tryValue = rand.Next( 0, 100 );
				if( ValueToFind == tryValue )
				{
					result.Text = string.Format( "{0},{1}", tryValue, Thread.CurrentThread.Name );
					ShowResultAct( result );
					IsAborted = true;
				}
				else
				{
					var output = new StringBuilder();
					output.Append( ValueToFind < tryValue ? "> " : "< " ); // для консольного представления
					output.Append( tryValue + " " );
					output.Append( Thread.CurrentThread.Name + " " );
					output.Append( DateTime.Now.ToString( "dd.MM HH:mm:ss" ) );
					result.Text = output.ToString();
					result.Color = ValueToFind < tryValue ? "Red" : "Green"; // для графического представления
					ShowResultAct( result );
				}
				Thread.Sleep( (int)Delay );

			}
		}




	}
}
