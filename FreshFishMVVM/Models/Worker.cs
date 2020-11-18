using FreshFishMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshFishMVVM.Models
{
    public class Worker : BaseViewModel, IDataErrorInfo
    {
        #region Validation
        //Поле, яке слугує для відправки винятків при некоректному вводі, в даному випадку, воно нічого не відправляє, так як не потрібно:
        public string Error { get { return null; } }

        //Словник помилок, який використовується для підказок(ToolTip), які з'являються біля контролу для визначення конкретної помилки
        public Dictionary<string, string> WorkersErrorCollection { get; private set; } = new Dictionary<string, string>();

        //Поле, яке безпосередньо робить перевірки:
        public string this[string columnName]
        {
            get
            {
                //на початку ніяких помилок немає, тому null
                string result = null;
                //"перебираємо" ймовірні помилки
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(_name)) //якщо ввід нульовий, тобто, немає ніякого значення
                        {
                            result = "Worker name cannot be empty";//"Ім'я користувача не може бути порожнім"
                        }
                        break;
                    case "Surname":
                        if (string.IsNullOrEmpty(_surname))
                        {
                            result = "Worker last name cannot be empty";
                        }
                        break;
                    case "Patronymic":
                        if (string.IsNullOrEmpty(_patronymic))
                        {
                            result = "Worker middle name cannot be empty";
                        }
                        break;
                    case "Position":
                        if (string.IsNullOrEmpty(_position))
                        {
                            result = "Worker position cannot be empty";
                        }
                        break;
                    case "Salary":
                        if (string.IsNullOrEmpty(_salary))
                        {
                            result = "Worker salary cannot be empty";
                        }
                        else if (_salary.All(char.IsDigit) == false) //Якщо в полі "зарплата" не всі введені значення є числові - помилка
                        {
                            result = "Worker salary cannot include any letter";//"Зарплата працівника не може вміщувати будь-які літери"
                        }
                        break;
                    case "PhoneNumber":
                        if (string.IsNullOrEmpty(_phonenumber))
                        {
                            result = "Worker phone number cannot be empty";
                        }
                        else if (_phonenumber.All(char.IsDigit) == false)//Якщо в полі "номер телефону" не всі введені значення є числові - помилка
                        {
                            result = "Worker phone number cannot include any letter";
                        }
                        break;
                    case "Address":
                        if (string.IsNullOrEmpty(_address))
                        {
                            result = "Worker address cannot be empty";
                        }
                        break;
                }

                //Додавання помилок у словник
                if (WorkersErrorCollection.ContainsKey(columnName))//Якщо колекція вже має ключ(тобто, наше поле), більше його не треба створювати, натомість, додати тільки текст помилки
                {
                    WorkersErrorCollection[columnName] = result;
                }
                else if (result != null)
                    WorkersErrorCollection.Add(columnName, result);//Якщо колекція ще не має такого ключа - додати і ключ, і текст помилки

                if (result != null)
                {
                    CanSave = false;
                }
                else
                {
                    CanSave = true;
                }

                OnPropertyChanged("WorkersErrorCollection");

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
        private string _name;
        private string _surname;
        private string _patronymic;
        private string _position;
        private string _salary;
        private string _phonenumber;
        private string _address;
        private string _additioninfo;
        #endregion

        #region Public properties
        public string Id { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Patronymic
        {
            get
            {
                return _patronymic;
            }
            set
            {
                _patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        public string Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }
        public string Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                _salary = value;
                OnPropertyChanged("Salary");
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }
        public string AdditionalInfo
        {
            get
            {
                return _additioninfo;
            }
            set
            {
                _additioninfo = value;
                OnPropertyChanged("AdditionalInfo");
            }
        }

        #endregion

        public override string ToString()
        {
            return _name + " " + _surname;
        }
    }
}
