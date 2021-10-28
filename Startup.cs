using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //public static IServiceCollection AddSwaggerGen(this IServiceCollection services, Action<SwaggerGenOptions> setupAction = null)
        //{
        //    // Add Mvc convention to ensure ApiExplorer is enabled for all actions
        //    services.Configure<MvcOptions>(c =>
        //        c.Conventions.Add(new SwaggerApplicationConvention()));

        //    // Register generator and it's dependencies
        //    services.AddTransient<ISwaggerProvider, SwaggerGenerator>();
        //    services.AddTransient<ISchemaGenerator, SchemaGenerator>();
        //    services.AddTransient<IApiModelResolver, JsonApiModelResolver>();

        //    // Register custom configurators that assign values from SwaggerGenOptions (i.e. high level config)
        //    // to the service-specific options (i.e. lower-level config)
        //    services.AddTransient<IConfigureOptions<SwaggerGeneratorOptions>, ConfigureSwaggerGeneratorOptions>();
        //    services.AddTransient<IConfigureOptions<SchemaGeneratorOptions>, ConfigureSchemaGeneratorOptions>();

        //    // Used by the <c>dotnet-getdocument</c> tool from the Microsoft.Extensions.ApiDescription.Server package.
        //    services.TryAddSingleton<IDocumentProvider, DocumentProvider>();

        //    if (setupAction != null) services.ConfigureSwaggerGen(setupAction);

        //    return services;
        //}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "NOBEDS API - V1",
                        Version = "v1",
                        Description = "We proudly present a first version of NOBEDS API.<br>" +
                        "You will be able to connect our channel manager to any third party solution.<br>" +
                        "View, Create, Update or Delete your data.<br>" +
                        "<br>" +
                        "<b>DEMO API TOKEN: demotoken</b><br>" +
                        "<br>" +
                        "Please contact us if you need more functions.",
                        TermsOfService = new Uri("http://nobeds.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Nobeds Support",
                            Email = "support@nobeds.com",
                            Url = new Uri("https://nobeds.com")
                        },
                        //License = new OpenApiLicense
                        //{
                        //    Name = "Apache 2.0",
                        //    Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                        //}
                    }
                );
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle ="NOBEDS API: Booking API, Expedia API, Airbnb API, Agoda API, HostelWorld API ";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NOBEDS API V1");
                c.DefaultModelsExpandDepth(-1); //do not show schemas
            });

           app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
