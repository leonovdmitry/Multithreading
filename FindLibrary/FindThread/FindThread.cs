using System;
using System.Collections.Generic;
using System.Linq;
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

		#region Fields

		private Thread _workThread;
		private Action _stopAllAct;
		private Action<String> _showResultAct;

		#endregion

		#region IFindThread
		public uint ValueToFind
		{
			get;
			private set;
		}

		public uint Delay
		{
			get;
			private set;
		}

		public bool IsAborted
		{
			get;
			private set;
		}

		public Action<string> ShowResultAct
		{
			get { return _showResultAct; }
		}

		public Action StopAllAct
		{
			get { return _stopAllAct; }
		}


		public void Start( Action<string> showResultAct, Action stopAllAct )
		{
			_showResultAct = showResultAct;
			_stopAllAct = stopAllAct;
			_workThread.Start();
		}

		public void Stop()
		{
			_workThread.Abort();
			IsAborted = true;
		}

		#endregion

		#region Private

		private void find()
		{
			//IFindThread parrent = (IFindThread)obj;
			Random rand = new Random();
			while( true )
			{
				var tryValue = rand.Next( 0, 100 );
				if( ValueToFind == tryValue )
				{
					ShowResultAct( string.Format( "{0},{1}", tryValue, Thread.CurrentThread.Name ) );
					IsAborted = true;
					StopAllAct();
				}
				else
				{
					var result = new StringBuilder();

					result.Append( ValueToFind < tryValue ? ">" : "<" );
					result.Append( tryValue + " " );
					result.Append( Thread.CurrentThread.Name + " " );
					result.Append( DateTime.Now.ToString( "dd.MM HH:mm:ss" ) );
					ShowResultAct( result.ToString() );
				}
				Thread.Sleep( (int)Delay );
			}
		}

		#endregion



	}
}
