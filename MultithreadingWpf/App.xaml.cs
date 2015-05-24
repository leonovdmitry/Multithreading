using FindLibrary.Builders;
using FindLibrary.FindThreadManager;
using Microsoft.Practices.Unity;
using MultithreadingWpf.View;
using MultithreadingWpf.ViewModel;
using System.Windows;

namespace MultithreadingWpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			IUnityContainer conteiner = new UnityContainer();
			conteiner.RegisterType<IFindThreadBuilder, FindThreadBuilder>();
			conteiner.RegisterType<IThreadManager, ThreadManager>( new ContainerControlledLifetimeManager() );
			var mainWindow = new MainWindow
			{
				DataContext = new MultithreadingViewModel( conteiner.Resolve<IThreadManager>() )
			};

			mainWindow.Show();
		}
	}
}
