using FreshFishMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshFishMVVM.Models
{
    public class BreedingDetail : BaseViewModel
    {
        #region Private Fields
        private string _initialWaterLevel;
        private string _temperature;
        private string _nitrogenAmount;
        private string _fishLaunchDate;
        private string _fishVolume;
        #endregion

        #region Public Properties
        public string Id { get; set; }

        public string InitialWaterLevel
        {
            get => _initialWaterLevel; 
            set
            {
                _initialWaterLevel = value;
                OnPropertyChanged("InitialWaterLevel");
            }
        }

        public string Temperature
        {
            get => _temperature; 
            set
            {
                _temperature = value;
                OnPropertyChanged("Temperature");
            }
        }

        public string NitrogenAmount
        {
            get => _nitrogenAmount; 
            set
            {
                _nitrogenAmount = value;
                OnPropertyChanged("NitrogenAmount");
            }
        }

        public string FishLaunchDate
        {
            get => _fishLaunchDate; 
            set
            {
                _fishLaunchDate = value;
                OnPropertyChanged("FishLaunchDate");
            }
        }

        public string FishVolume
        {
            get => _fishVolume; 
            set
            {
                _fishVolume = value;
                OnPropertyChanged("FishVolume");
            }
        }
        #endregion
    }
}
