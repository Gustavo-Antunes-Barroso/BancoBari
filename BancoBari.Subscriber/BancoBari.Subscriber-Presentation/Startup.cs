using BancoBari.Subscriber_Application.Implementation;
using BancoBari.Subscriber_Application.Interfaces;
using BancoBari.Subscriber_Crosscutting.Context;
using BancoBari.Subscriber_Domain.Intefaces;
using BancoBari.Subscriber_Repository.Repository.Queued;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BancoBari.Subscriber_Application.BackgroudService;

namespace BancoBari.Subscriber_Presentation
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
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IQueuedAppService, QueuedAppService>();
            services.AddTransient<IQueuedRepository, QueuedRepository>();
            services.AddHostedService<TimedBackgroundService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
