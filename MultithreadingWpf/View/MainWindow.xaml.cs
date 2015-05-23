using FindLibrary.Builders;
using FindLibrary.FindThreadManager;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultithreadingWpf.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			IUnityContainer conteiner = new UnityContainer();
			conteiner.RegisterType<IFindThreadBuilder, FindThreadBuilder>();
			conteiner.RegisterType<IThreadManager, ThreadManager>();
			InitializeComponent();
		}
	}
}
