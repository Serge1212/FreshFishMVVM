

using FreshFishMVVM.ViewModels.Base;

namespace FreshFishMVVM.Helpers
{
    public interface IViewModelCommands
    {
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get;}
        public RelayCommand RemoveCommand { get; }
    }
}
