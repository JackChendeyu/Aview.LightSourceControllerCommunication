using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AviewLightSource.Views;

namespace AviewLightSource_Test
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WpfMN200_YH.Connect.ConnectFactory connectFactory = new WpfMN200_YH.Connect.ConnectFactory();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AviewLightSource.MainWindow mainWindow = new AviewLightSource.MainWindow(WpfMN200_YH.Connect.ConnectFactory.optLeft);
            
            mainWindow.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
