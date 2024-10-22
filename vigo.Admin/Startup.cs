using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using vigo.Domain.AccountFolder;
using vigo.Infrastructure.DBContext;

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

        public void Init()
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
