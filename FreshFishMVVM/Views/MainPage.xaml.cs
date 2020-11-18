using FreshFishMVVM.ViewModels;
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

namespace FreshFishMVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        private void ListViewMenu_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //int index = ListViewInMenu.SelectedIndex;
            //switch (index)
            //{
            //    case 0:
            //        MainFrame.Content = null;
            //        MainFrame.Content = new WorkerPage1();
            //        break;
            //    case 1:
            //        MainFrame.Content = null;
            //        MainFrame.Content = new FishPage();
            //        break;
            //    case 2:
            //        MainFrame.Content = null;
            //        MainFrame.Content = new VehiclesPage();
            //        break;
            //    case 3:
            //        MainFrame.Content = null;

            //        break;
            //    case 4:
            //        MainFrame.Content = null;
            //        MainFrame.Content = new FishBreedingPage();
            //        break;
            //    case 5:
            //        MainFrame.Content = null;
            //        MainFrame.Content = new IncomePage1();
            //        break;
            //    case 6:
            //        MainFrame.Content = null;
            //        MainFrame.Content = new DeliveryPage();
            //        break;
            //    case 7:
            //        MainFrame.Content = null;

            //        break;
            //    default:
            //        break;
            //}

        }
        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            ToggleButtonMenu.IsChecked = false;
        }
    }
}
