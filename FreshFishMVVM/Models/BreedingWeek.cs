using FreshFishMVVM.ViewModels.Base;

namespace FreshFishMVVM.Models
{
    public class BreedingWeek : BaseViewModel
    {
        #region Private Fields
        private string _weekNumber;
        private string _weekDate;
        private string _waterLevel;
        private string _breedingDetailsID;
        #endregion

        #region Public Properties
        public string Id { get; set; }
       
        public string WeekNumber
        {
            get => _weekNumber;
            
            set
            {
                _weekNumber = value;
                OnPropertyChanged("WeekNumber");

            }
        }

        public string WeekDate
        {
            get => _weekDate;
            
            set
            {
                _weekDate = value;
                OnPropertyChanged("WeekDate");

            }
        }
        public string WaterLevel
        {
            get => _waterLevel;
            
            set
            {
                _waterLevel = value;
                OnPropertyChanged("WaterLevel");

            }
        }

        public string BreedingDetailsID
        {
            get => _breedingDetailsID;
            
            set
            {
                _breedingDetailsID = value;
                OnPropertyChanged("BreedingDetailsID");
            }
        }
        #endregion
    }
}
