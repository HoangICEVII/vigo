using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using vigo.Domain.AccountFolder;
using vigo.Domain.Entity;
using vigo.Infrastructure.DBContext;
using vigo.Service.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vigo.Admin
{
    public class Startup
    {
        private readonly IServiceProvider _serviceProvider;
        public Startup(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void ConfigureServices()
        {
        }
        public void Configure(IApplicationBuilder app)
        {
            bool conActive = false;
            while (!conActive)
            {
                try
                {
                    Thread.Sleep(1000);
                    using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<VigoDatabaseContext>();
                        dbContext.Database.Migrate();
                    }
                    conActive = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public async Task Init()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                try
                {
                    var _vigoContext = scopedServices.GetRequiredService<VigoDatabaseContext>();
                    var adminRole = _vigoContext.Roles.Where(e => e.Name == "admin").FirstOrDefault();
                    if (adminRole == null)
                    {
                        Role role = new Role()
                        {
                            Id = 1,
                            Name = "admin",
                            Permission = "",
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            DeletedDate = null
                        };
                        _vigoContext.Roles.Add(role);
                        _vigoContext.SaveChanges();
                    }
                    var admin = _vigoContext.Accounts.Where(e => e.Email == "admin456@gmail.com").FirstOrDefault();
                    if (admin == null)
                    {
                        var salt = PasswordHasher.CreateSalt();
                        var hashedPassword = PasswordHasher.HashPassword("admin", salt);
                        Account account = new Account() {
                            Id = Guid.NewGuid(),
                            Email = "admin456@gmail.com",
                            Password = hashedPassword,
                            Salt = salt,
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                            EmailActive = true,
                            DeletedDate = null,
                            RoleId = 1,
                            UserType = "SystemEmployee"
                        };
                        _vigoContext.Accounts.Add(account);
                        _vigoContext.SaveChanges();
                    }

                    using (HttpClient client = new HttpClient())
                    {
                        string urlP = "https://esgoo.net/api-tinhthanh/1/0.htm";
                        HttpResponseMessage responseP = await client.GetAsync(urlP);
                        string responseBodyP = await responseP.Content.ReadAsStringAsync();
                        DataTemp<Province> dataP = JsonConvert.DeserializeObject<DataTemp<Province>>(responseBodyP);
                        _vigoContext.Provinces.AddRange(dataP.Data);

                        foreach (Province province in dataP.Data)
                        {
                            string urlD = $"https://esgoo.net/api-tinhthanh/2/{province.Id}.htm";
                            HttpResponseMessage responseD = await client.GetAsync(urlD);
                            string responseBodyD = await responseD.Content.ReadAsStringAsync();
                            DataTemp<District> dataD = JsonConvert.DeserializeObject<DataTemp<District>>(responseBodyD);
                            foreach (var district in dataD.Data)
                            {
                                district.ProvinceId = province.Id;
                            }
                            _vigoContext.Districts.AddRange(dataD.Data);

                            foreach (District district in dataD.Data)
                            {
                                string urlS = $"https://esgoo.net/api-tinhthanh/3/{district.Id}.htm";
                                HttpResponseMessage responseS = await client.GetAsync(urlS);
                                string responseBodyS = await responseS.Content.ReadAsStringAsync();
                                DataTemp<Street> dataS = JsonConvert.DeserializeObject<DataTemp<Street>>(responseBodyS);
                                foreach(var street in dataS.Data)
                                {
                                    street.DistrictId = district.Id;
                                }
                                _vigoContext.Streets.AddRange(dataS.Data);
                            }
                        }
                        _vigoContext.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
