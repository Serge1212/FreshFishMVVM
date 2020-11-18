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
    class SelectedWorkerViewModel : BaseDatabasePagesViewModel
    {
        #region Private fields
        private Worker _selectedWorker;
        private WorkerHelper workerHelper = new WorkerHelper();
        private RelayCommand _saveCommand;
        private RelayCommand _removeCommand;
        #endregion


        #region Public Properties
        public Worker SelectedWorker
        {
            get => _selectedWorker;
            set
            {
                _selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }

        public RelayCommand SaveCommand
        {
            get => _saveCommand ??= new RelayCommand(SaveWorker, obj => SelectedWorker.CanSave == true);
        }

        public RelayCommand RemoveCommand
        {
            get => _removeCommand ??= new RelayCommand(async obj =>
            {
                Worker worker = obj as Worker;
                if(worker != null)
                {
                    CloseWindow(obj);
                    await workerHelper.DeleteAsync(worker.Id);
                }

            });
        }
        #endregion

        #region Methods
        private async void SaveWorker(object obj)
        {
            CloseWindow(obj);
            if (!edited)
            {
                await workerHelper.AddAsync(SelectedWorker);
            }
            if(edited)
            {
                await workerHelper.UpdateAsync(SelectedWorker);
            }
        }
        #endregion

        public SelectedWorkerViewModel(Worker worker = null)
        {
            SelectedWorker = worker;
            if(worker == null)
            {
                SelectedWorker = new Worker();
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
