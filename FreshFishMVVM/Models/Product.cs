using FreshFishMVVM.ViewModels.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FreshFishMVVM.Models
{
    public class Product : BaseViewModel, IDataErrorInfo
    {
        #region Validation
        public string Error { get => null; }

        public Dictionary<string, string> ProductsErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {
                    case "ProductName":
                        if (string.IsNullOrEmpty(_productname) && productNamechanged)
                        {
                            result = "Product name cannot be empty";
                        }
                        break;
                    case "Price":
                        Regex regex = new Regex(@"^[0-9.]+$");//дозволяє тільки цифри і крапку
                        if (priceChanged)
                        {
                            if (string.IsNullOrEmpty(_price))
                            {
                                result = "Price cannot be empty";
                            }
                            else if (!regex.IsMatch(_price))
                            {
                                result = "Price can contain only digits and a single dot";
                            }
                        }
                        break;
                    case "Status":
                        if (string.IsNullOrEmpty(_status) && statusChanged)
                        {
                            result = "Status cannot be empty";
                        }
                        break;
                }
                if (ProductsErrorCollection.ContainsKey(columnName))
                {
                    ProductsErrorCollection[columnName] = result;
                }
                else if (result != null)
                    ProductsErrorCollection.Add(columnName, result);

                if (result != null)
                {
                    CanSave = false;
                }
                else
                {
                    CanSave = true;
                }

                OnPropertyChanged("ProductsErrorCollection");

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
        private bool productNamechanged = false;
        private bool priceChanged = false;
        private bool statusChanged = false;

        private string _productname;
        private string _price;
        private string _date;
        private string _status;
        private string _driver;
        private string _packer;
        #endregion

        #region Public properties
        public string Id { get; set; }
        public string ProductName
        {
            get => _productname; 
            set
            {
                _productname = value;
                OnPropertyChanged("ProductName");
                productNamechanged = true;
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged("Price");
                priceChanged = true;
            }
        }

        public string Date
        {
            get => _date; 
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public string Status
        {
            get => _status; 
            set
            {
                _status = value;
                OnPropertyChanged("Status");
                statusChanged = true;
            }
        }
        public string Driver
        {
            get => _driver; 
            set
            {
                _driver = value;
                OnPropertyChanged("Driver");
            }
        }

        public string Packer
        {
            get => _packer; 
            set
            {
                _packer = value;
                OnPropertyChanged("Packer");
            }
        }
       
        #endregion
    }
}
