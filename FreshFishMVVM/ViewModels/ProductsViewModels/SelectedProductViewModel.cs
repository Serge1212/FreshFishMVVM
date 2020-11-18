using FreshFishMVVM.Helpers;
using FreshFishMVVM.Models;
using FreshFishMVVM.ViewModels.Base;
using System;
using System.Windows;

namespace FreshFishMVVM.ViewModels
{
    public class SelectedProductViewModel : BaseDatabasePagesViewModel
    {
        #region Private Fields  
        private ProductHelper productsHelper = new ProductHelper();
        private Product _selectedProduct;
        private RelayCommand _saveCommand;
        private RelayCommand _removeCommand;
        #endregion

        #region Public Properties

        public Product SelectedProduct
        {
            get => _selectedProduct; 
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        public RelayCommand SaveCommand
        {
            get => _saveCommand ??= new RelayCommand(SaveProduct, obj => SelectedProduct.CanSave == true);
            
        }
        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(async obj =>
                {
                    Product product = obj as Product;
                    if (product != null)
                    {
                        CloseWindow(obj);
                        await productsHelper.DeleteAsync(product.Id);
                    }
                });
            
        }
        #endregion

        #region Methods
        private async void SaveProduct(object obj)
        {
            CloseWindow(obj);
            if (!edited)
            {
                await productsHelper.AddAsync(SelectedProduct);
            }
            if (edited)
            {
                await productsHelper.UpdateAsync(SelectedProduct);
            }

        }
        
        #endregion

        public SelectedProductViewModel(Product product = null)
        {
            SelectedProduct = product;
            if (product == null)
            {
                SelectedProduct = new Product();
                edited = false;
                isDeleteButtonHidden = false;
            }
            else
            {
                isDeleteButtonHidden = true;
            }

        }
    }
}
