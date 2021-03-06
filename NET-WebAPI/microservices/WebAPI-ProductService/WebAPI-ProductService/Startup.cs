using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using WebAPI_ProductService.Consumer;

namespace WebAPI_ProductService
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
            services.AddControllers();

            //Cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                                                             .AllowAnyMethod()
                                                             .AllowAnyHeader());
            });

            // Register the Swagger services
            services.AddSwaggerDocument();

            // Configure RabbitMQ service 
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductConsumer>();
                x.AddConsumer<IssueConsumer>();
                
                x.UsingRabbitMq((context, cfg) =>
                {                    
                    cfg.Host("host.docker.internal", "/", h => {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("product-queue", e =>
                    {
                        e.ConfigureConsumer<ProductConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("issue-queue", e =>
                    {
                        e.ConfigureConsumer<IssueConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            //JSON serialization
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            

            //Add authorization service
            var key = "ThisIsMySimpleJwtKey"; //temporary key
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowOrigin");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Swagger UI
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
