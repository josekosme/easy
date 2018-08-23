using Bus;
using EasyWeb.Domain.Core.Bus;
using EasyWeb.Domain.Core.Events;
using EasyWeb.Domain.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Visitante.API.Application.Queries;
using Visitante.Data.Context;
using Visitante.Data.EventSourcing;
using Visitante.Data.Repository;
using Visitante.Data.Repository.EventSourcing;
using Visitante.Data.UoW;
using Visitante.Domain.Interfaces;
using Visitante.Domain.Models;

namespace Visitante.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddMvc();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));
            services.AddMediatR();

            // Domain - Commands and handles
            //services.AddScoped<IRequestHandler<RegisterUser, bool>, RegisterUserHandler>();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddMediatR(AppDomain.CurrentDomain.Load("Visitante.Domain"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("EasyWeb.Domain.Core"));


            services.AddDbContext<EasyVisitanteContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<EasyVisitanteQuerieContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Infra - Data
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEventStore, SqlEventStore>();

            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();
            services.AddScoped<EasyVisitanteQuerieContext>();

            services.AddScoped<IAccountQueries, AccountQueries>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AuthAppBuilderExtensions.UseAuthentication(app);
            app.UseMvc();
        }
    }
}
