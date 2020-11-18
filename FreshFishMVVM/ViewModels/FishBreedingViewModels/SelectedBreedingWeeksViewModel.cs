using FreshFishMVVM.Helpers;
using FreshFishMVVM.Models;
using FreshFishMVVM.ViewModels.Base;
using FreshFishMVVM.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshFishMVVM.ViewModels.FishBreedingViewModels
{
    public class SelectedBreedingWeeksViewModel : BaseDatabasePagesViewModel
    {
        #region Private Fields  
        private BreedingWeeksHelper helper = new BreedingWeeksHelper();
        private BreedingWeek _selectedItem;
        private RelayCommand _saveCommand;
        private RelayCommand _removeCommand;
        #endregion

        #region Public Properties

        public BreedingWeek SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public RelayCommand SaveCommand
        {
            get => _saveCommand ??= new RelayCommand(SaveBreedingDetails);

        }
        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(async obj =>
            {
                BreedingWeek b = obj as BreedingWeek;
                if (b != null)
                {
                    CloseWindow(obj);
                    await helper.DeleteAsync(b.Id);
                }
            });

        }
        #endregion

        #region Methods
        private async void SaveBreedingDetails(object obj)
        {
            CloseWindow(obj);
            SelectedItem.BreedingDetailsID = FishBreedingPage.BreedingDetailsFromDataGridSelected?.Id;
            if (!edited)
            {
                await helper.AddAsync(SelectedItem);
            }
            if (edited)
            {
                await helper.UpdateAsync(SelectedItem);
            }

        }

        #endregion

        public SelectedBreedingWeeksViewModel(BreedingWeek breedingWeek = null)
        {
            SelectedItem = breedingWeek;
            if (breedingWeek == null)
            {
                SelectedItem = new BreedingWeek();
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
