using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FreshFishMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly FirebaseClient FirebaseClient = new FirebaseClient("https://freshfish-bf927.firebaseio.com");
    }
}
