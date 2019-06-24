using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using InfiniteGrid.Simulation.Application;
using InfiniteGrid.Simulation.DomainModel;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace InfiniteGrid.Api
{
    public class Startup
    {
        private const string API_NAME = "InfiniteSpaceship API";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;  
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = API_NAME, Version = "v1" });
            });

            // Create the container builder.
            var builder = new ContainerBuilder();

            services.AddOptions();
            
            services.AddAutofac();
            services.AddMediatR(
                typeof(Machine).Assembly,
                typeof(TextRenderer).Assembly
            );
            //services.Configure<MachineSettings>(Configuration);

            //var machineSettings = new MachineSettings();
            //Configuration.GetSection("MachineSettings").Bind(machineSettings);

            builder.Populate(services);
            builder.RegisterModule<ApplicationModule>();
            

            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            var serviceProvider = new AutofacServiceProvider(this.ApplicationContainer);


            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            //loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", API_NAME);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
