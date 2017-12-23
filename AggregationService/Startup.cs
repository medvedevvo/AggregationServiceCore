using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AggregationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AggregationService
{
    public class Startup
    {
        private ConnectionSettings connectionSettings = ConnectionSettings.getInstance();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionSettings.ConnecnionString = Configuration.GetConnectionString("DriversConnection");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<DriversDbContext>(options => options.UseSqlServer(connectionSettings.ConnecnionString));
            services.BuildServiceProvider().GetRequiredService<DriversDbContext>().Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
