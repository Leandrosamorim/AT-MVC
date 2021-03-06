﻿using System;
using System.Text;
using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _20GRPED.MVC2.Crosscutting.Identity
{
    public static class IdentityRegistration
    {
        public static void RegisterIdentityForMvc(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDbContext(services, configuration);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<LoginContext>();
        }

        public static void RegisterIdentityForWebApi(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            AddDbContext(services, configuration);

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<LoginContext>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LoginContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LoginContextConnection")));
        }
    }
}
