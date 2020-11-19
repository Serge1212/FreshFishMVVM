using FreshFishMVVM.ViewModels.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FreshFishMVVM.Models
{
    public class Vehicle : BaseViewModel, IDataErrorInfo
    {
        #region Validation
        public string Error { get => null; }

        public Dictionary<string, string> VehiclesErrorsCollection { get; private set; } = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get
            {
                Regex regex = new Regex(@"^[0-9.]+$");//дозволяє тільки цифри і крапку
                string result = null;

                switch (columnName)
                {
                    case "Model":
                        if (string.IsNullOrEmpty(_model) && modelChanged)
                        {
                            result = "Model cannot be empty";
                        }
                        break;
                    case "Mark":
                        if (string.IsNullOrEmpty(_mark) && markChanged)
                        {
                            result = "Mark cannot be empty";
                        }

                        break;
                    case "ManufactureDate":
                        if (string.IsNullOrEmpty(_manufactureDate) && manufacturerChanged)
                        {
                            result = "Manufacture date cannot be empty";
                        }
                        break;
                    case "Mileage":
                        if (mileageChanged)
                        {
                            if (string.IsNullOrEmpty(_mileage))
                            {
                                result = "Mileage cannot be empty";
                            }
                            else if (!regex.IsMatch(_mileage))
                            {
                                result = "Mileage can contain only digits and a single dot";
                            }
                        }
                        break;
                    case "FuelConsumption":
                        if (fuelConsumptionChanged)
                        {
                            if (string.IsNullOrEmpty(_fuelConsumption))
                            {
                                result = "Fuel consumption field cannot be empty";
                            }
                            else if (!regex.IsMatch(_fuelConsumption))
                            {
                                result = "Fuel consumption can contain only digits and a single dot";
                            }
                        }
                        break;
                }
                if (VehiclesErrorsCollection.ContainsKey(columnName))
                {
                    VehiclesErrorsCollection[columnName] = result;
                }
                else if (result != null)
                    VehiclesErrorsCollection.Add(columnName, result);

                if (result != null)
                {
                    CanSave = false;
                }
                else
                {
                    CanSave = true;
                }

                OnPropertyChanged("VehiclesErrorsCollection");

                return result == null ? string.Empty : "!";


            }
        }
        private bool canSave;
        public bool CanSave
        {
            get
            {
                return canSave;
            }
            set
            {
                canSave = value;
                OnPropertyChanged("CanSave");
            }
        }
        #endregion

        #region Private fields
        private string _model;
        private string _mark;
        private string _manufactureDate;
        private string _mileage;
        private string _fuelConsumption;

        bool modelChanged = false;
        bool markChanged = false;
        bool manufacturerChanged = false;
        bool mileageChanged = false;
        bool fuelConsumptionChanged = false;
        #endregion

        #region Public properties
        public string Id { get; set; }
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged("Model");
                modelChanged = true;
            }
        }
        public string Mark
        {
            get => _mark;
            set
            {
                _mark = value;
                OnPropertyChanged("Mark");
                markChanged = true;
            }
        }
        public string ManufactureDate
        {
            get => _manufactureDate;
            set
            {
                _manufactureDate = value;
                OnPropertyChanged("ManufactureDate");
                //manufacturerChanged = true;
            }
        }
        public string Mileage
        {
            get => _mileage;
            set
            {
                _mileage = value;
                OnPropertyChanged("Mileage");
                mileageChanged = true;
            }
        }

        public string FuelConsumption
        {
            get => _fuelConsumption;
            set
            {
                _fuelConsumption = value;
                OnPropertyChanged("FuelConsumption");
                fuelConsumptionChanged = true;
            }
        }
        #endregion

        public override string ToString()
        => Mark + " " + Model;
        
    }
}
