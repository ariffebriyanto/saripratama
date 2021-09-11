using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.Utils
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {

            services.AddTransient<FactoryClass, FactoryClass>();

            return services;
        }
    }
}
