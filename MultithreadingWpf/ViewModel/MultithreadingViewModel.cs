using FindLibrary;
using FindLibrary.Builders;
using FindLibrary.FindThreadManager;
using Microsoft.Practices.Unity;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MultithreadingWpf.ViewModel
{
	public class MultithreadingViewModel : INotifyPropertyChanged
	{
		private IThreadManager _threadManager;
		private bool _isRun;
		private uint _valueToFind;
		private uint _threadsCount;
		private uint _delay;

		public MultithreadingViewModel( IThreadManager threadManager )
		{
			Output = new ObservableCollection<Result>();
			StartStopCommand = new Command( arg => StartStopHandler() );
			_threadManager = threadManager;
		}

		public uint ValueToFind
		{
			get
			{
				if( _valueToFind < 0 || _valueToFind > 100 )
					_valueToFind = 0;
				return _valueToFind;
			}
			set { _valueToFind = value; }

		}
		public uint ThreadsCount
		{
			get
			{
				if( _threadsCount < 1 )
					_threadsCount = 1;
				return _threadsCount;
			}
			set { _threadsCount = value; }

		}
		public uint Delay
		{
			get
			{
				if( _delay < 0 )
					_delay = 0;
				return _delay;
			}
			set
			{
				_delay = value * 1000;
			}
		}
		public ObservableCollection<Result> Output { get; set; }
		public ICommand StartStopCommand { get; set; }
		public bool IsRun
		{
			get { return _isRun; }
			set
			{
				_isRun = value;
				OnPropertyChanged( "IsRun" );
			}
		}

		private void StartStopHandler()
		{
			var _uiDispatcher = Dispatcher.CurrentDispatcher;
			if( !IsRun )
			{
				IsRun = true;
				Output.Clear();
				_threadManager.Init( ValueToFind, ThreadsCount, Delay );
				var syncObj = new Object();
				_threadManager.Start( ( x ) =>
			{
				lock( syncObj )
				{
					if( !_threadManager.IsAborted )
					{
						Action act = () => Output.Add( x );
						_uiDispatcher.Invoke( act );
					}
					else
					{
						if( IsRun )
						{
							IsRun = false;
							Action msgBox = () => MessageBox.Show( "УРА!", "Сообщение" );
							_uiDispatcher.Invoke( msgBox );
							_threadManager.Stop();
						}
					}
				}
			} );
			}
			else
			{
				IsRun = false;
				_threadManager.Stop();

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
