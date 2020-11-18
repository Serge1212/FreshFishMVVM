
using FreshFishMVVM.Helpers;
using FreshFishMVVM.Models;
using FreshFishMVVM.ViewModels.Base;

namespace FreshFishMVVM.ViewModels
{
    public class SelectedBreedingDetailsViewModel : BaseDatabasePagesViewModel
    {
        #region Private Fields  
        private BreedingDetailsHelper helper = new BreedingDetailsHelper();
        private BreedingDetail _selectedItem;
        private RelayCommand _saveCommand;
        private RelayCommand _removeCommand;
        #endregion

        #region Public Properties

        public BreedingDetail SelectedItem
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
                BreedingDetail d = obj as BreedingDetail;
                if (d != null)
                {
                    CloseWindow(obj);
                    await helper.DeleteAsync(d.Id);
                }
            });

        }
        #endregion

        #region Methods
        private async void SaveBreedingDetails(object obj)
        {
            CloseWindow(obj);
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

        public SelectedBreedingDetailsViewModel(BreedingDetail breedingDetail = null)
        {
            SelectedItem = breedingDetail;
            if (breedingDetail == null)
            {
                SelectedItem = new BreedingDetail();
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
