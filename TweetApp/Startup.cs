using Confluent.Kafka;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.DAL.Interfaces;
using TweetApp.DAL.Repositories;
using TweetApp.Entities;


namespace TweetApp
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
            services.Configure<TweetAppDatabaseSettings>(Configuration.GetSection(nameof(TweetAppDatabaseSettings)));
            services.AddSingleton<ITweetAppDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TweetAppDatabaseSettings>>().Value);
            services.AddSingleton<IDataContext, DataContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITweetRepository, TweetRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ITweetCommentsRepository, TweetCommentsRepository>();
            services.AddCors();
            services.AddSwaggerGen();
            
            services.AddHostedService<MyKafkaConsumer>();
            services.AddSingleton<ConsumerConfig>(option =>
            {
                ConsumerConfig config = new ConsumerConfig();
                config.BootstrapServers = Configuration.GetValue<string>("KafkaConsumer:BootstrapServers");
                config.SaslUsername = Configuration.GetValue<string>("KafkaConsumer:SaslUsername");
                config.SaslPassword = Configuration.GetValue<string>("KafkaConsumer:SaslPassword");
                config.SaslMechanism = SaslMechanism.Plain;
                config.SecurityProtocol = SecurityProtocol.SaslSsl;
                config.GroupId = Guid.NewGuid().ToString();
                config.AutoOffsetReset = AutoOffsetReset.Earliest;
                return config;
            });
            services.AddMetrics();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //// Custom Metrics to count requests for each endpoint and the method
            //var counter = Metrics.CreateCounter("tweetappapi_path_counter", "Counts requests to the Tweetapp API endpoints", new CounterConfiguration
            //{
            //    LabelNames = new[] { "method", "endpoint" }
            //});
            //app.Use((context, next) =>
            //{
            //    counter.WithLabels(context.Request.Method, context.Request.Path).Inc();
            //    return next();
            //});
            //// Use the Prometheus middleware
            //app.UseMetricServer();
            //app.UseHttpMetrics();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                  
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
        
    }
}
