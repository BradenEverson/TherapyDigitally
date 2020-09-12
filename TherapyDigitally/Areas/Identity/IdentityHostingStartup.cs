using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TherapyDigitally;
using TherapyDigitally.Data;

[assembly: HostingStartup(typeof(TherapyDigitally.Areas.Identity.IdentityHostingStartup))]
namespace TherapyDigitally.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TherapyDigitallyContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TherapyDigitallyContextConnection")));

                services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<TherapyDigitallyContext>();
            });
        }
    }
}