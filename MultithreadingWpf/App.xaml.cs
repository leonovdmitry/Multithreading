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
			var mw = new MainWindow
			{
				DataContext = new MultithreadingViewModel()
			};

			mw.Show();
		}
	}
}
