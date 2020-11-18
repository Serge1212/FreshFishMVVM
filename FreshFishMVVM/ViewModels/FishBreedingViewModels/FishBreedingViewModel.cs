using Firebase.Database;
using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Helpers;
using FreshFishMVVM.Models;
using FreshFishMVVM.ViewModels.Base;
using FreshFishMVVM.Windows;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows;
using System;

namespace FreshFishMVVM.ViewModels
{
    public class FishBreedingViewModel : BaseViewModel, IViewModelCommands
    {
        #region Private Fields
        private static bool executed = true;
        private BreedingDetailsHelper helper = new BreedingDetailsHelper();
        private BreedingDetail _selectedItem;
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private RelayCommand _removeCommand;
        #endregion

        #region Public Properties
        public ObservableCollection<BreedingDetail> BreedingDetailsCollection { get; set; }
        public BreedingDetail SelectedItem
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
                 (obj) => BreedingDetailsCollection.Count > 0 && SelectedItem != null);

        }
        #endregion

        #region Methods
        private void OpenWindowForAdding(object obj)
        {
            var selectedBreedingDetailsWindow = new SelectedBreedingDetailsWindow();
            var selectedBreedingDetailsViewModel = new SelectedBreedingDetailsViewModel();
            selectedBreedingDetailsWindow.DataContext = selectedBreedingDetailsViewModel;
            selectedBreedingDetailsWindow.Show();
        }
        private void OpenWindowForEdit(object obj)
        {
            var selectedBreedingDetailsWindow = new SelectedBreedingDetailsWindow();
            var selectedBreedingDetailsViewModel = new SelectedBreedingDetailsViewModel(SelectedItem);
            selectedBreedingDetailsWindow.DataContext = selectedBreedingDetailsViewModel;
            selectedBreedingDetailsWindow.Show();
        }
        #endregion

        public FishBreedingViewModel()
        {
            var child = Globals.Client.Child("BreedingDetails");
            if (executed)
            {
                try
                {

                    BreedingDetailsCollection =
                     child
                    .AsObservable<BreedingDetail>()
                    .ObserveOnDispatcher()
                    .AsObservableCollection();

                    executed = false;
                }
                catch(FirebaseException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
