using System;
using csharp_asp_net_core_mvc_housing_queue.Data;
using csharp_asp_net_core_mvc_housing_queue.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(csharp_asp_net_core_mvc_housing_queue.Areas.Identity.IdentityHostingStartup))]
namespace csharp_asp_net_core_mvc_housing_queue.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}