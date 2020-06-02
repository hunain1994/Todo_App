using Microsoft.Extensions.DependencyInjection;
using Todo.Master.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Todo.Master.Api.Extensions
{
    public static class ServicesCollectionExtension
    {
        public static void RegisterMappingProfiles(this IServiceCollection services)
        {
            //Auto Mapper Configuration
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
