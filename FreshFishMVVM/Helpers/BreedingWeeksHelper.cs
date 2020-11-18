using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System;

namespace FreshFishMVVM.Helpers
{
    public class BreedingWeeksHelper : IHelper<BreedingWeek>
    {
        public async Task AddAsync(BreedingWeek b)
        {
            await Globals.Client
               .Child("BreedingWeeks/")
               .PostAsync(new BreedingWeek()
               {
                   Id = Globals.GetRandomId(),
                   WeekNumber = b.WeekNumber,
                   WeekDate = b.WeekDate,
                   WaterLevel = b.WaterLevel,
                   BreedingDetailsID = b.BreedingDetailsID
               });
        }

        public async Task DeleteAsync(string ID)
        {
            var toDeleteBreedingWeek = (await Globals.Client
                .Child("BreedingWeeks")
                .OnceAsync<BreedingWeek>()).Where(bw => bw.Object.Id == ID).FirstOrDefault();
            await Globals.Client.Child("BreedingWeeks").Child(toDeleteBreedingWeek.Key).DeleteAsync();
        }

        public async Task<List<BreedingWeek>> GetAllAsync()
        => (await Globals.Client
                .Child("BreedingWeeks")
                .OnceAsync<BreedingWeek>()).Select(item => new BreedingWeek
                {
                    Id = item.Object.Id,
                    WeekNumber = item.Object.WeekNumber,
                    WeekDate = item.Object.WeekDate,
                    WaterLevel = item.Object.WaterLevel,
                    BreedingDetailsID = item.Object.BreedingDetailsID
                }).ToList();

        public async Task UpdateAsync(BreedingWeek d)
        {
            var toUpdateBreedingWeek = (await Globals.Client
               .Child("BreedingWeeks")
               .OnceAsync<BreedingWeek>()).Where(bw => bw.Object.Id == d.Id).FirstOrDefault();

            await Globals.Client
                .Child("BreedingWeeks")
                .Child(toUpdateBreedingWeek.Key)
                .PutAsync(new BreedingWeek
                {
                    Id = d.Id,
                    WeekNumber = d.WeekNumber,
                    WeekDate = d.WeekDate,
                    WaterLevel = d.WaterLevel,
                    BreedingDetailsID = d.BreedingDetailsID
                });
        }
    }
}
