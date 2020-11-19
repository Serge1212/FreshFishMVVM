using Firebase.Database;
using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Helpers;
using FreshFishMVVM.Models;
using FreshFishMVVM.ViewModels.Base;
using FreshFishMVVM.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace FreshFishMVVM.ViewModels
{
    class ProductsViewModel : BaseViewModel, IViewModelCommands
    {
        #region Private Fields
        private ProductHelper productHelper = new ProductHelper();
        private static bool executed = true;
        private Product _selectedProduct;
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private RelayCommand _removeCommand;
        #endregion

        #region Public Properties
        public static ObservableCollection<Product> ProductsCollection { get; set; }
        public Product SelectedProduct
        {
            get => _selectedProduct; 
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        public RelayCommand AddCommand
        {
            get => _addCommand ??= new RelayCommand(OpenWindowForNewProduct);
        }
        public RelayCommand EditCommand
        {
            get => _editCommand ??= new RelayCommand(OpenWindowForSelectedProductEdit, (obj) => SelectedProduct != null); 
        }
        public RelayCommand RemoveCommand
        {
            get =>
                _removeCommand ??= new RelayCommand(async obj =>
                {
                    Product product = obj as Product;
                    if (product != null)
                    {
                        await productHelper.DeleteAsync(product.Id);
                    }
                },
                 (obj) => ProductsCollection.Count > 0 && SelectedProduct != null);
        }
        #endregion

        #region Methods
        private void OpenWindowForSelectedProductEdit(object obj)
        {
            var selectedProductWindow = new SelectedProductWindow();
            var selectedProductViewModel = new SelectedProductViewModel(SelectedProduct);
            selectedProductWindow.DataContext = selectedProductViewModel;
            selectedProductWindow.Show();
        }

        private void OpenWindowForNewProduct(object obj)
        {
            var selectedProductWindow = new SelectedProductWindow();
            var selectedProductViewModel = new SelectedProductViewModel();
            selectedProductWindow.DataContext = selectedProductViewModel;
            selectedProductWindow.Show();
        }
        #endregion


        public ProductsViewModel()
        {
            if (executed)
            {
                ProductsCollection = Globals.Client
                .Child("products")
                .AsObservable<Product>()
                .ObserveOnDispatcher()
                .AsObservableCollection();

                executed = false;
            }
        }
    }
}
