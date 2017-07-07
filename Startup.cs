using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using app.Data;
using app.Models;


namespace app
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			// Database connection string.
			// Make sure to update the Password value below from "your_password" to your actual password.
			var connection = @"Server=db;Database=FabsEvals;User=sa;Password=P@ssword1!;";

			// This line uses 'UseSqlServer' in the 'options' parameter
			// with the connection string defined above.
			services.AddDbContext<ApplicationDbContext>(
				options => options.UseSqlServer(connection));

            services.AddScoped<iSpeakerEvalsRepository, SpeakerEvalsRepository>();


			services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
                             ApplicationDbContext evalDataContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			//uses to seed the SQLCore Database in the Docker Container
			evalDataContext.EnsureSeedDataForContext();

            app.UseMvc();
        }
    }
}
