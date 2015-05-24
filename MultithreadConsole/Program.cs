using FindLibrary.FindThreadBuilder;
using FindLibrary.FindThreadManager;
using Microsoft.Practices.Unity;
using System;

namespace MultithreadConsole
{
	class Program
	{
		static IUnityContainer container;
		static void Main( string[] args )
		{
			container = new UnityContainer();
			container.RegisterType<IFindThreadBuilder, FindThreadBuilder>();
			container.RegisterType<IThreadManager, ThreadManager>( new ContainerControlledLifetimeManager() );
			AppStart();
		}

		private static void AppStart()
		{
			var manager = container.Resolve<IThreadManager>();
			Console.Write( "Значение для поиска: " );
			var valueToFind = uint.Parse( Console.ReadLine() );
			Console.Write( "Количество потоков: " );
			var threadsCount = uint.Parse( Console.ReadLine() );
			Console.Write( "Задержка(сек): " );
			var delayValue = uint.Parse( Console.ReadLine() ) * 1000;
			manager.Init( valueToFind, threadsCount, delayValue );
			Console.WriteLine( "Нажмите: \r\n s -> Стоп \r\n x -> Выход " );
			var syncObj = new Object();
			manager.Start( ( x ) =>
			{
				lock( syncObj )
					if( !manager.IsAborted )
						Console.WriteLine( x.Text );
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
