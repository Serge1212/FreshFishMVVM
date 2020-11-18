using Firebase.Database;
using Firebase.Database.Query;
using FreshFishMVVM.GlobalVariables;
using FreshFishMVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FreshFishMVVM.Helpers
{
    public class ProductHelper : IHelper<Product>
    {
        //TODO: initiate this variable in the required method
        double sum;
        public async Task<List<Product>> GetAllAsync()
        {
            return (await Globals.Client 
                .Child("products")                
                .OnceAsync<Product>()).Select(item => new Product
                {
                    Id = item.Object.Id,
                    ProductName = item.Object.ProductName,
                    Price = item.Object.Price,
                    Date = item.Object.Date,
                    Status = item.Object.Status,
                    Driver = item.Object.Driver,
                    Packer = item.Object.Packer                
                }).ToList();
        }

        public async Task<double> GetPricesSumAsync()
        {
            var productsList = await GetAllAsync();

            var prices = from p in productsList
                         where p.Status == "Yes"
                         select p.Price;

            sum = prices.Sum(v => Convert.ToDouble(v));

            return sum;
        }

        public async Task AddAsync(Product product)
        {
            await Globals.Client
                .Child("products/")
                .PostAsync(new Product()
                {
                    Id = GetRandomId(),
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Date = product.Date,
                    Status = product.Status,
                    Packer = product.Packer,
                    Driver = product.Driver
                });
        }

        public async Task UpdateAsync(
            Product product)
        {
            var toUpdateProduct = (await Globals.Client
                .Child("products")
                .OnceAsync<Product>()).Where(a => a.Object.Id == product.Id).FirstOrDefault(); //шукаємо продукт за переданим в метод айді

            await Globals.Client
                .Child("products")
                .Child(toUpdateProduct.Key)//звертаємося до конкретного запису в сервері за ключем
                .PutAsync(new Product
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Date = product.Date,
                    Status = product.Status,
                    Packer = product.Packer,
                    Driver = product.Driver
                });
        }
        public async Task DeleteAsync(string ID)
        {
            var toDeleteProduct = (await Globals.Client
                .Child("products")
                .OnceAsync<Product>()).Where(a => a.Object.Id == ID).FirstOrDefault();
            await Globals.Client.Child("products").Child(toDeleteProduct.Key).DeleteAsync();
        }
            #region Random ID FOR PRODUCTS
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
                "P";

        }
        #endregion
    }
}
