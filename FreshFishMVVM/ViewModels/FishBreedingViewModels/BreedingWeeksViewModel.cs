using Firebase.Database;
using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Helpers;
using FreshFishMVVM.Models;
using FreshFishMVVM.ViewModels.Base;
using FreshFishMVVM.Views.Pages;
using FreshFishMVVM.Windows;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace FreshFishMVVM.ViewModels.FishBreedingViewModels
{
    public class BreedingWeeksViewModel : BaseViewModel, IViewModelCommands
    {
        #region Private Fields
        private static bool executed = true;
        private BreedingWeeksHelper helper;
        private BreedingWeek _selectedItem;
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private RelayCommand _removeCommand;
        #endregion

        #region Public Properties
        public ObservableCollection<BreedingWeek> BreedingWeeksCollection { get; set; }
        public BreedingWeek SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public RelayCommand AddCommand
        {
            get => _addCommand ??= new RelayCommand(OpenWindowForAdding);
        }

        public RelayCommand EditCommand
        {
            get => _editCommand ??= new RelayCommand(OpenWindowForEdit);
        }


        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(async obj =>
            {
                BreedingDetail bd = obj as BreedingDetail;
                if (bd != null)
                {
                    await helper.DeleteAsync(bd.Id);
                }
            },
                 (obj) => BreedingWeeksCollection.Count > 0 && SelectedItem != null);

        }
        #endregion

        #region Methods
        private void OpenWindowForAdding(object obj)
        {
            var selectedBreedingWeeksWindow = new SelectedBreedingWeeksWindow();
            var selectedBreedingWeeksViewModel = new SelectedBreedingWeeksViewModel();
            selectedBreedingWeeksWindow.DataContext = selectedBreedingWeeksViewModel;
            selectedBreedingWeeksWindow.Show();
        }
        private void OpenWindowForEdit(object obj)
        {
            var selectedBreedingWeeksWindow = new SelectedBreedingWeeksWindow();
            var selectedBreedingWeeksViewModel = new SelectedBreedingWeeksViewModel(SelectedItem);
            selectedBreedingWeeksWindow.DataContext = selectedBreedingWeeksViewModel;
            selectedBreedingWeeksWindow.Show();
        }
        #endregion

        public BreedingWeeksViewModel()
        {
            if (executed)
            {
                BreedingWeeksCollection = Globals.Client
             .Child("BreedingWeeks")
             .AsObservable<BreedingWeek>()
             .Where(bw => bw.Object.BreedingDetailsID == FishBreedingPage.BreedingDetailsFromDataGridSelected?.Id)
             .ObserveOnDispatcher()
             .AsObservableCollection();

                executed = false;
            }
        }
    }
}
