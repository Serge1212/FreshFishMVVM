using Firebase.Database.Query;
using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreshFishMVVM.Helpers
{
    public class VehicleHelper : IHelper<Vehicle>
    {
        public async Task<List<Vehicle>> GetAllAsync()
        {
            return (await Globals.Client
                .Child("vehicles")
                .OnceAsync<Vehicle>()).Select(item => new Vehicle
                {
                    Id = item.Object.Id,
                    Model = item.Object.Model,
                    Mark = item.Object.Mark,
                    ManufactureDate = item.Object.ManufactureDate,
                    Mileage = item.Object.Mileage,
                    FuelConsumption = item.Object.FuelConsumption
                }).ToList();
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await Globals.Client
                .Child("vehicles/")
                .PostAsync(new Vehicle()
                {
                    Id = GetRandomId(),
                    Model = vehicle.Model,
                    Mark = vehicle.Mark,
                    ManufactureDate = vehicle.ManufactureDate,
                    Mileage = vehicle.Mileage,
                    FuelConsumption = vehicle.FuelConsumption
                });
        }
        public async Task UpdateAsync(Vehicle vehicle)
        {
            var toUpdateVehicle = (await Globals.Client
               .Child("vehicles")
               .OnceAsync<Vehicle>()).Where(a => a.Object.Id == vehicle.Id).FirstOrDefault();

            await Globals.Client
                .Child("vehicles")
                .Child(toUpdateVehicle.Key)
                .PutAsync(new Vehicle
                {
                    Id = vehicle.Id,
                    Model = vehicle.Model,
                    Mark = vehicle.Mark,
                    ManufactureDate = vehicle.ManufactureDate,
                    Mileage = vehicle.Mileage,
                    FuelConsumption = vehicle.FuelConsumption

                });
        }

        public async Task DeleteAsync(string id)
        {
            var toDeleteVehicle = (await Globals.Client
                .Child("vehicles")
                .OnceAsync<Vehicle>()).Where(v => v.Object.Id == id).FirstOrDefault();
            await Globals.Client.Child("vehicles").Child(toDeleteVehicle.Key).DeleteAsync();
        }

        public async Task<Vehicle> GetVehicleAsync(string id)
        {
            var allVehicles = await GetAllAsync();
            await Globals.Client
                .Child("vehicles")
                .OnceAsync<Vehicle>();

            return allVehicles.Where(w => w.Id == id).FirstOrDefault();
        }


        #region Random ID FOR VEHICLES
        string GetRandomId()
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string x = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
            const string nums = "123456789";
            string y = new string(Enumerable.Repeat(nums, 4)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());

            string sDate = DateTime.Now.ToString();
            DateTime value = (Convert.ToDateTime(sDate.ToString()));

            return x + y +
                value.Day.ToString() +
                value.Month.ToString() +
                value.Year.ToString() +
                value.Minute.ToString() +
                value.Hour.ToString() +
                value.Second.ToString() +
                "v";

        }
        #endregion
    }
}
