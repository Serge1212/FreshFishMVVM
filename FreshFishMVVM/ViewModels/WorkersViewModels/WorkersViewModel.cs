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
    public class WorkersViewModel : BaseViewModel, IViewModelCommands
    {
        #region Private Fields
        private WorkerHelper workersHelper = new WorkerHelper();
        private static bool executed = true;
        private Worker _selectedWorker;
        private RelayCommand _addCommand;
        private RelayCommand _editCommand;
        private RelayCommand _removeCommand;
        #endregion

        #region Public Properties 
        public ObservableCollection<Worker> WorkersCollection { get; set; }
        public Worker SelectedWorker
        {
            get => _selectedWorker; 
            set
            {
                _selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }
        public RelayCommand AddCommand
        {
            get => _addCommand ??= new RelayCommand(OpenWindowForNewWorker);    
        }
        public RelayCommand EditCommand
        {
            get => _editCommand ??= new RelayCommand(OpenWindowForSelectedWorkerEdit, (obj) => SelectedWorker != null);    
        }
        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(async obj =>
                {
                    Worker worker = obj as Worker;
                    if (worker != null)
                    {
                        await workersHelper.DeleteAsync(worker.Id);
                    }
                },
                 (obj) => WorkersCollection.Count > 0 && SelectedWorker != null);
            
        }
        #endregion

        #region Methods
        private void OpenWindowForSelectedWorkerEdit(object obj)
        {
            var selectedWorkertWindow = new SelectedWorkerWindow();
            var selectedWorkerViewModel = new SelectedWorkerViewModel(SelectedWorker);
            selectedWorkertWindow.DataContext = selectedWorkerViewModel;
            selectedWorkertWindow.Show();
        }

        private void OpenWindowForNewWorker(object obj)
        {
            var selectedWorkertWindow = new SelectedWorkerWindow();
            var selectedWorkerViewModel = new SelectedWorkerViewModel(null);
            selectedWorkertWindow.DataContext = selectedWorkerViewModel;
            selectedWorkertWindow.Show();


        }
        #endregion

        public WorkersViewModel()
        {
            if (executed)
            {
                WorkersCollection = Globals.Client
                .Child("workers")
                .AsObservable<Worker>()
                .ObserveOnDispatcher()
                .AsObservableCollection();

                executed = false;
            }
        }
    }
}
