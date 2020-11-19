using Firebase.Database;
using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Helpers;
using FreshFishMVVM.Models;
using FreshFishMVVM.ViewModels.Base;
using FreshFishMVVM.Windows;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace FreshFishMVVM.ViewModels
{
    class VehiclesViewModel : BaseViewModel, IViewModelCommands
    {
        #region Private Fields
        private VehicleHelper vehiclesHelper = new VehicleHelper();
        private static bool executed = true;
        private Vehicle _selectedVehicle;
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private RelayCommand _removeCommand;
        #endregion


        #region Public Properties
        public static ObservableCollection<Vehicle> VehiclesCollection { get; set; }
        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set
            {
                _selectedVehicle = value;
                OnPropertyChanged("SelectedVehicle");
            }
        }
        public RelayCommand AddCommand
        {
            get => _addCommand ??= new RelayCommand(OpenWindowForNewVehicle);
        }
        public RelayCommand EditCommand
        {
            get => _editCommand ??= new RelayCommand(OpenWindowForSelectedVehicleEdit, (obj) => SelectedVehicle != null);
        }
        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(async obj =>
            {
                Vehicle vehicle = obj as Vehicle;
                if (vehicle != null)
                {
                    await vehiclesHelper.DeleteAsync(vehicle.Id);
                }
            },
                 (obj) => VehiclesCollection.Count > 0 && SelectedVehicle != null);

        }
        #endregion

        #region Methods
        private void OpenWindowForSelectedVehicleEdit(object obj)
        {
            var selectedVehicleWindow = new SelectedVehicleWindow();
            var selectedVehicleViewModel = new SelectedVehicleViewModel(SelectedVehicle);
            selectedVehicleWindow.DataContext = selectedVehicleViewModel;
            selectedVehicleWindow.Show();
        }

        private void OpenWindowForNewVehicle(object obj)
        {
            var selectedVehicleWindow = new SelectedVehicleWindow();
            var selectedVehicleViewModel = new SelectedVehicleViewModel();
            selectedVehicleWindow.DataContext = selectedVehicleViewModel;
            selectedVehicleWindow.Show();
        }

        #endregion

        public VehiclesViewModel()
        {
            if (executed)
            {
                VehiclesCollection = Globals.Client
                .Child("vehicles")
                .AsObservable<Vehicle>()
                .ObserveOnDispatcher()
                .AsObservableCollection();

                executed = false;
            }
        }
    }
}
