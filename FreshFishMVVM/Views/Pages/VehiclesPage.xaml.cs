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

namespace FreshFishMVVM.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для VehiclesPage.xaml
    /// </summary>
    public partial class VehiclesPage : Page
    {
        public VehiclesPage()
        {
            InitializeComponent();
            DataContext = new VehiclesViewModel();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchByCombobox.SelectedIndex == 0)
            {
                var SearchedList = (from vehicle in VehiclesViewModel.VehiclesCollection
                                    where vehicle.Model.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select vehicle).ToList();
                VehiclesDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 1)
            {
                var SearchedList = (from vehicle in VehiclesViewModel.VehiclesCollection
                                    where vehicle.Mark.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select vehicle).ToList();
                VehiclesDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 2)
            {
                var SearchedList = (from vehicle in VehiclesViewModel.VehiclesCollection
                                    where vehicle.ManufactureDate.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select vehicle).ToList();
                VehiclesDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 3)
            {
                var SearchedList = (from vehicle in VehiclesViewModel.VehiclesCollection
                                    where vehicle.Mileage.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select vehicle).ToList();
                VehiclesDataGrid.ItemsSource = SearchedList;
            }
            if (SearchByCombobox.SelectedIndex == 4)
            {
                var SearchedList = (from vehicle in VehiclesViewModel.VehiclesCollection
                                    where vehicle.FuelConsumption.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select vehicle).ToList();
                VehiclesDataGrid.ItemsSource = SearchedList;
            }
        }
    }
}
