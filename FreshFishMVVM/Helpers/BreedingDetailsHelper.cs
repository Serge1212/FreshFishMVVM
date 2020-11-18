using Firebase.Database.Query;
using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshFishMVVM.Helpers
{
    public class BreedingDetailsHelper : IHelper<BreedingDetail>
    {
        public async Task AddAsync(BreedingDetail d)
        {
            await Globals.Client
                .Child("BreedingDetails/")
                .PostAsync(new BreedingDetail()
                {
                    Id = Globals.GetRandomId(),
                    InitialWaterLevel = d.InitialWaterLevel,
                    Temperature = d.Temperature,
                    NitrogenAmount = d.NitrogenAmount,
                    FishLaunchDate = d.FishLaunchDate,
                    FishVolume = d.FishVolume 
                });
        }

        public async Task DeleteAsync(string ID)
        {
            var toDeleteBreedingDetails = (await Globals.Client
               .Child("BreedingDetails")
               .OnceAsync<BreedingDetail>()).Where(bd => bd.Object.Id == ID).FirstOrDefault();
            await Globals.Client.Child("BreedingDetails").Child(toDeleteBreedingDetails.Key).DeleteAsync();
        }

        public async Task<List<BreedingDetail>> GetAllAsync()
        {
            return (await Globals.Client
                .Child("BreedingDetails")
                .OnceAsync<BreedingDetail>()).Select(item => new BreedingDetail
                {
                    Id = item.Object.Id,
                    InitialWaterLevel = item.Object.InitialWaterLevel,
                    Temperature = item.Object.Temperature,
                    NitrogenAmount = item.Object.NitrogenAmount,
                    FishLaunchDate = item.Object.FishLaunchDate,
                    FishVolume = item.Object.FishVolume
                }).ToList();
        }

        public async Task UpdateAsync(BreedingDetail d)
        {
            var toUpdateBreedingDetails = (await Globals.Client
               .Child("BreedingDetails")
               .OnceAsync<BreedingDetail>()).Where(bd => bd.Object.Id == d.Id).FirstOrDefault();

            await Globals.Client
                .Child("BreedingDetails")
                .Child(toUpdateBreedingDetails.Key)
                .PutAsync(new BreedingDetail
                {
                    Id = d.Id,
                    InitialWaterLevel = d.InitialWaterLevel,
                    Temperature = d.Temperature,
                    NitrogenAmount = d.NitrogenAmount,
                    FishLaunchDate = d.FishLaunchDate,
                    FishVolume = d.FishVolume
                });
        }
    }
}
