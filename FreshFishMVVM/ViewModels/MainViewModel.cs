using FreshFishMVVM.ViewModels.Base;
using FreshFishMVVM.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FreshFishMVVM.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Private Fields
        private Page ProductsPage;
        private Page WorkersPage;
        private Page VehiclesPage;
        private Page IncomePage;
        private Page BreedingPage;
        private Page DeliveryPage;
        private Page _currentPage;
        private int _index;
        private RelayCommand _openWorkersPage;
        private RelayCommand _openProductsPage;
        private RelayCommand _openVehiclesPage;
        private RelayCommand _openIncomePage;
        private RelayCommand _openFishBreedingPage;
        private RelayCommand _openDeliveryPage;
        #endregion

        #region Public Properties
        public Page CurrentPage
        {
            get => _currentPage; 
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        public int Index
        {
            get => _index;
            
            set
            {
                _index = value;
                OnPropertyChanged("Index");
            }
        }
        public RelayCommand OpenWorkersPage
        {
            get => _openWorkersPage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = WorkersPage;
                    Index = 0;
                });
        }
        public RelayCommand OpenProductsPage
        {
            get => _openProductsPage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = ProductsPage;
                    Index = 1;
                });  
        }
        public RelayCommand OpenVehiclesPage
        {
            get => _openVehiclesPage ??= new RelayCommand((obj) =>
            {
                CurrentPage = VehiclesPage;
                Index = 2;
            });
        }
        public RelayCommand OpenFishBreedingPage
        {
            get => _openFishBreedingPage ??= new RelayCommand((obj) =>
            {
                CurrentPage = BreedingPage;
                Index = 3;
            });
        }
        public RelayCommand OpenIncomePage
        {
            get => _openIncomePage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = IncomePage;
                    Index = 4;
                });
        }
        public RelayCommand OpenDeliveryPage
        {
            get => _openDeliveryPage ??= new RelayCommand((obj) =>
                {
                    CurrentPage = DeliveryPage;
                    Index = 5;
                });            
        }
        #endregion  

        public MainViewModel()
        {
            ProductsPage = new ProductsPage();
            WorkersPage = new WorkersPage();
            VehiclesPage = new VehiclesPage();
            IncomePage = new IncomePage();
            BreedingPage = new FishBreedingPage();
            DeliveryPage = new DeliveryPage();
            CurrentPage = WorkersPage;
            Index = 0;
        }
    }
}
