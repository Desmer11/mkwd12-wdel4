﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Implementations;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services.Implementations;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModelValidators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lamazon.Services.Extensions
{
    public static class InjectionExtensions
    {
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IProductCategoriesRepository, ProductCategoriesRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        }

        public static void InjectService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IProductCategoriesService, ProductCategoriesService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
        }

        public static void InjectAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void InjectFluentValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(s =>
            {
                s.DisableDataAnnotationsValidation = true;
            });
            services.AddValidatorsFromAssemblyContaining<ProductViewModelValidator>();
            services.AddValidatorsFromAssemblyContaining<ProductCategoryViewModelValidator>();
        }
    }
}
