using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Interfaces;
using Services.Repository;
using Services.DAL;

namespace WebApi
{
    public class Startup
    {
        private IConfigurationRoot _confstring;
        public Startup(IWebHostEnvironment hostEnv)
        {
            _confstring = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json")
                .Build();
        }

     

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(options =>
               options.UseSqlServer(_confstring.GetConnectionString("DefaultConnection")));
            services.AddTransient<IPayments, PaymentsRepository>();
            services.AddTransient<IExpenses, ExpensesRepository>();
            services.AddTransient<IIncomes, IncomesRepository>();
            services.AddTransient<IAdmissions, AdmissionsRepository>();



           
            services.AddTransient<IPaymentsService, PaymentsService>();
            services.AddTransient<IExpensesService, ExpensesService>();
            services.AddTransient<IIncomesService, IncomesService>();
            services.AddTransient<IAdmissionsService, AdmissionsService>();
            services.AddTransient<UnitOfWork>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContext content = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                SeedData.Initial(content);  
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
