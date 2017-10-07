using ConsoleApplication.Infrastructure;
using ConsoleApplication.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApplication
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder() // Collection of sources for read/write key/value pairs
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables(); // Overrides environment variables with valiues from config files/etc
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddJsonFormatters();

            // DB INITIALIZATION: 
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ArticlesContext>(options => options
                    .UseNpgsql("User ID=postgres;Password=password;Server=postgres;Port=5432;Database=MyTestDb;Integrated Security=true;Pooling=true;"));
                    
            services.AddScoped<IArticlesRepository, ArticlesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            var startupLogger = loggerFactory.CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            try 
            {
                // DB INITIALIZATION: Create DB on startup
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<ArticlesContext>().Database.Migrate();
                }
            }
            catch 
            {
                // no database 
            }            
            
            startupLogger.LogTrace("Trace test output!");
            startupLogger.LogDebug("Debug test output!");
            startupLogger.LogInformation("Info test output!");
            startupLogger.LogError("Error test output!");
            startupLogger.LogCritical("Trace test output!");
        }
    }
}