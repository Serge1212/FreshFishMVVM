using Firebase.Database.Query;
using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishMVVM.Helpers
{
    public class WorkerHelper : IHelper<Worker>
    {
        public async Task<List<Worker>> GetAllAsync()
        {
            return (await Globals.Client
                .Child("workers")
                .OnceAsync<Worker>()).Select(item => new Worker
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Surname = item.Object.Surname,
                    Patronymic = item.Object.Patronymic,
                    Position = item.Object.Position,
                    Salary = item.Object.Salary,
                    PhoneNumber = item.Object.PhoneNumber,
                    Address = item.Object.Address,
                    AdditionalInfo = item.Object.AdditionalInfo
                }).ToList();
        }

        public async Task AddAsync(Worker worker)
        {
            await Globals.Client
                .Child("workers/")
                .PostAsync(new Worker()
                {
                    Id = GetRandomId(),//отримуємо нове згенероване айді
                    Name = worker.Name,
                    Surname = worker.Surname,
                    Patronymic = worker.Patronymic,
                    Position = worker.Position,
                    Salary = worker.Salary,
                    PhoneNumber = worker.PhoneNumber,
                    Address = worker.Address,
                    AdditionalInfo = worker.AdditionalInfo
                });
        }
        public async Task UpdateAsync(Worker worker)
        {
            var toUpdateProduct = (await Globals.Client
               .Child("workers")
               .OnceAsync<Worker>()).Where(a => a.Object.Id == worker.Id).FirstOrDefault();

            await Globals.Client
                .Child("workers")
                .Child(toUpdateProduct.Key)
                .PutAsync(new Worker
                {
                    Id = worker.Id,
                    Name = worker.Name,
                    Surname = worker.Surname,
                    Patronymic = worker.Patronymic,
                    Position = worker.Position,
                    Salary = worker.Salary,
                    PhoneNumber = worker.PhoneNumber,
                    Address = worker.Address,
                    AdditionalInfo = worker.AdditionalInfo

                });
        }

        public async Task DeleteAsync(string ID)
        {
            var toDeleteWorker = (await Globals.Client
                .Child("workers")
                .OnceAsync<Worker>()).Where(w => w.Object.Id == ID).FirstOrDefault();
            await Globals.Client.Child("workers").Child(toDeleteWorker.Key).DeleteAsync();
        }

        #region Random ID FOR WORKERS
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
                "w";

        }
        #endregion
    }
}
