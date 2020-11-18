using FreshFishMVVM.Helpers;
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
using System.Windows.Shapes;

namespace FreshFishMVVM.Windows
{
    /// <summary>
    /// Логика взаимодействия для SelectedWorkerWindow.xaml
    /// </summary>
    public partial class SelectedWorkerWindow : Window
    {
        public SelectedWorkerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is IClosable c)
            {
                c.Close += () =>
                {
                    Close();
                };
            }
        }
    }
}
