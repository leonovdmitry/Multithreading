using FindLibrary.Builders;
using FindLibrary.FindThreadManager;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace multithread
{
	class Program
	{
		static IUnityContainer unitycontainer;
		static void Main( string[] args )
		{
			unitycontainer = new UnityContainer();
			unitycontainer.RegisterType<IFindThreadBuilder, FindThreadBuilder>();
			unitycontainer.RegisterType<IThreadManager, ThreadManager>();
			AppStart();
		}

		private static void AppStart()
		{

			var manager = unitycontainer.Resolve<IThreadManager>();
			Console.Write( "Value to find: " );
			var valueToFind = uint.Parse( Console.ReadLine() );
			Console.Write( "threads: " );
			var threadsCount = uint.Parse( Console.ReadLine() );
			Console.Write( "delay(sec): " );
			var delayValue = uint.Parse( Console.ReadLine() ) * 1000;
			manager.Init( valueToFind, threadsCount, delayValue );
			Console.WriteLine( "press key s -> Stop \r\n x -> Exit " );
			var syncObj = new Object();
			manager.Start( ( x ) =>
			{
				lock( syncObj )
					if( !manager.IsAborted )
						Console.WriteLine( x );
					else
						manager.Stop();
			} );
			IsStop( manager );
		}
		private static void IsStop( IThreadManager manager )
		{
			var s = Console.ReadLine();
			switch( s )
			{
				case "s":
					manager.Stop();
					AppStart();
					break;
				case "x":
					break;
				default:
					IsStop( manager );
					break;
			}
		}
	}



}
