using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivosCatalogApi.Data;
using INVENTARIO.Services.ActivosCatalogApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ActivosCatalogApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ActivosSettings>(Configuration);
            services.AddMvc();
            services.AddDbContext<ActivosContext>(options => options.UseSqlServer(Configuration["ConnectionString"]));
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
            options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
            {
                Title="Inventario - Activos Catalog HTTP API",
                Version="V1",
                Description="Catalogo de Activos Microservicio HTTP API",
                TermsOfService="Terms of Service"
            });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(c=>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "ActivosCatalogAPI v1");
                });
            app.UseMvc();
        }
    }
}
