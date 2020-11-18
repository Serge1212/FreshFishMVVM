using FreshFishMVVM.Helpers;
using FreshFishMVVM.Models;
using FreshFishMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshFishMVVM.ViewModels
{
    class SelectedVehicleViewModel : BaseDatabasePagesViewModel
    {
        #region Private fields
        private Vehicle _selectedVehicle;
        private VehicleHelper vehicleHelper = new VehicleHelper();
        private RelayCommand _saveCommand;
        private RelayCommand _removeCommand;
        #endregion


        #region Public Properties
        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set
            {
                _selectedVehicle = value;
                OnPropertyChanged("SelectedVehicle");
            }
        }

        public RelayCommand SaveCommand
        {
          
            get => _saveCommand ??= new RelayCommand(SaveVehicle, obj => SelectedVehicle.CanSave == true);
        }

        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(async obj =>
            {
                Vehicle vehicle = obj as Vehicle;
                if (vehicle != null)
                {
                    CloseWindow(obj);
                    await vehicleHelper.DeleteAsync(vehicle.Id);
                }

            });
        }
        #endregion

        #region Methods
        private async void SaveVehicle(object obj)
        {
            CloseWindow(obj);
            if (!edited)
            {
                await vehicleHelper.AddAsync(SelectedVehicle);
            }
            if (edited)
            {
                await vehicleHelper.UpdateAsync(SelectedVehicle);
            }
        }
        #endregion

        public SelectedVehicleViewModel(Vehicle vehicle = null)
        {
            SelectedVehicle = vehicle;
            if (vehicle == null)
            {
                SelectedVehicle = new Vehicle();
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
