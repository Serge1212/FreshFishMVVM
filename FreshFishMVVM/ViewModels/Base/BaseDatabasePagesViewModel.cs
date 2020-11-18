using FreshFishMVVM.Helpers;
using System;

namespace FreshFishMVVM.ViewModels.Base
{
    public class BaseDatabasePagesViewModel : BaseViewModel, IClosable
    {
        protected bool edited = true;
        public bool isDeleteButtonHidden { get; set; }
        public Action Close { get; set; }

        private RelayCommand _closeWindowCommand;
        public RelayCommand CloseWindowCommand
        {
            get => _closeWindowCommand ??= new RelayCommand(CloseWindow);
        }

        protected void CloseWindow(object obj)
        {
            Close?.Invoke();
        }
    }
}
