using Amazon.CloudWatchLogs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Mediator.Commands;
using SmartBusinessAPI.Repositories;
using SmartBusinessAPI.Services.Auth0;
using SmartBusinessAPI.Services.Interfaces;
using SmartBusinessAPI.Services.Metamap;
using System;
using System.Text;

namespace SmartBusinessAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            //var client = new AmazonCloudWatchLogsClient();

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Verbose()
            //    .WriteTo.AmazonCloudWatch(
            //    logGroup: "/smartbusiness/api/logs",
            //    logStreamPrefix: DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
            //    cloudWatchClient: client)
            //    .CreateLogger();

            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<ISociosRepository, SociosRepository>();
            services.AddTransient<ISociosCommand, SociosCommand>();
            services.AddTransient<IProductosCommand, ProductosCommand>();
            services.AddTransient<IProductosRepository, ProductosRepository>();
            services.AddTransient<IProspectosCommand, ProspectosCommand>();
            services.AddTransient<IPaisesCommand, PaisesCommand>();
            services.AddTransient<ICatalogsRepository, CatalogsRepository>();
            services.AddTransient<IEnumsCommand, EnumsCommand>();
            services.AddTransient<IMetamapRepository, MetamapRepository>();
            services.AddTransient<IAuth0Repository, Auth0Repository>();
            services.AddTransient<IMembresiasCommand, MembresiasCommand>();
            services.AddTransient<IMembresiasRepository, MembresiasRepository>();
            services.AddSingleton<IMetamapAPI, MetamapAPI>();
            services.AddSingleton<IAuthAPI, AuthAPI>();
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                };
            });
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartBusiness API", Version = "v1" });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "SmartBusiness API");
            });

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}
