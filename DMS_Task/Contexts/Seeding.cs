﻿using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

using JsonNet.PrivateSettersContractResolvers;

namespace DMS_Task.Contexts
{
    public class Seeding
    {
        public static void Seed<T>(string jsonData, IServiceProvider serviceProvider) where T : class
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };
            List<T> entities = JsonConvert.DeserializeObject<List<T>>(jsonData, settings);
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope
                    .ServiceProvider.GetService<ApplicationDbContext>();
                if (!context.Set<T>().Any())
                {
                    context.Set<T>().AddRange(entities);
                    context.SaveChanges();
                }
            }
        }
    }
}
