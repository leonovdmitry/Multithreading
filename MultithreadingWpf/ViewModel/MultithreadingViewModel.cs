using FindLibrary.Builders;
using FindLibrary.FindThreadManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MultithreadingWpf.ViewModel
{
	public class MultithreadingViewModel : INotifyPropertyChanged
	{
		private IThreadManager _threadManager;
		private bool _isRun;
		private uint _valueToFind;
		private uint _threadsCount;
		private uint _delay;
		private List<string> _output;

		#region ctor
		public MultithreadingViewModel()
		{
			_output = new List<string>();
			StartStopCommand = new Command( arg => StartStopHandler() );
			_threadManager = new ThreadManager( new FindThreadBuilder() );
		}
		#endregion

		#region Properties

		public uint ValueToFind
		{
			get
			{ return _valueToFind; }
			set
			{
				_valueToFind = value;
				OnPropertyChanged( "ValueToFind" );
			}
		}
		public uint ThreadsCount
		{
			get
			{ return _threadsCount; }
			set
			{
				_threadsCount = value;
				OnPropertyChanged( "ThreadsCount" );
			}
		}
		public uint Delay
		{
			get
			{ return _delay; }
			set
			{
				_delay = value;
				OnPropertyChanged( "Delay" );
			}
		}
		List<string> Output
		{
			get
			{ return _output; }
			set
			{
				_output = value;
				OnPropertyChanged( "Output" );
			}
		}

		public ICommand StartStopCommand { get; set; }

		#endregion
		private void StartStopHandler()
		{
			if( !_isRun )
			{
				_isRun = true;
				_threadManager.Init( ValueToFind, ThreadsCount, Delay );
				var syncObj = new Object();
				_threadManager.Start( ( x ) =>
			{
				lock( syncObj )
					if( !_threadManager.IsAborted )
					{

						Output.Add( x );
						OnPropertyChanged( "Output" );
					}
					else
					{
						_threadManager.Stop();
						_isRun = false;
					}
			} );
			}
			else
			{
				_threadManager.Stop();
				Output.Clear();
				_isRun = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged( string propertyName )
		{
			if( PropertyChanged != null )
			{
				PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
			}
		}
	}
}
