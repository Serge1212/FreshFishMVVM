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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            DataContext = new ProductsViewModel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchByCombobox.SelectedIndex == 0)
            {
                var SearchedList = (from product in ProductsViewModel.ProductsCollection
                                    where product.ProductName.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select product).ToList();
                ProductsDataGrid.ItemsSource = SearchedList;
            }

            if (SearchByCombobox.SelectedIndex == 1)
            {
                var SearchedList = (from product in ProductsViewModel.ProductsCollection
                                    where product.Price.ToLower().Contains(SearchTextBox.Text.ToLower())
                                    select product).ToList();
                ProductsDataGrid.ItemsSource = SearchedList;
            }

            if (SearchByCombobox.SelectedIndex == 2)
            {
                var SearchedList = (from product in ProductsViewModel.ProductsCollection
                                    where product.Date.ToLower().StartsWith(SearchTextBox.Text.ToLower())
                                    select product).ToList();
                ProductsDataGrid.ItemsSource = SearchedList;
            }

            if (SearchByCombobox.SelectedIndex == 3)
            {
                var SearchedList = (from product in ProductsViewModel.ProductsCollection
                                    where product.Status.ToLower().Contains(SearchTextBox.Text.ToLower())
                                    select product).ToList();
                ProductsDataGrid.ItemsSource = SearchedList;
            }
        }
    }
}
