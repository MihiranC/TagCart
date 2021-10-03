using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TagTeam.Admin.Service;
using TagTeam.Admin.Service.Interfaces;

namespace TagTeam.Admin.API
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        private static string _adminConnectionString { get; set; }
        private static string _sCConnectionString { get; set; }
        private static string _DocumentUploadPath { get; set; }

        //email settings
        private static string _Mail { get; set; }
        private static string _DisplayName { get; set; }
        private static string _Password { get; set; }
        private static string _Host { get; set; }
        private static int _Port { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile("AppSettings.json");

            _configuration = builder.Build();
            _adminConnectionString = _configuration.GetConnectionString("AdminConnection");
            _sCConnectionString = _configuration.GetConnectionString("SCConnection");

            //emails
            _Mail = _configuration.GetConnectionString("Mail");
            _DisplayName = _configuration.GetConnectionString("DisplayName");
            _Password = _configuration.GetConnectionString("Password");
            _Host = _configuration.GetConnectionString("Host");
            _Port = int.Parse(_configuration.GetConnectionString("Port"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<ITest_Interface>(c => new TestService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<IPages_Interface>(c => new PagesService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<IRef_Districts_interface>(c => new Ref_DistrictsService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<IRef_Cities_interface>(c => new Ref_CitiesService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<IUsers_interface>(c => new UsersService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<ILogin_Interface>(c => new LoginService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<ISettings_interface>(c => new SettingsService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<INextCode_interface>(c => new NextCodeService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<IImageResize_interface>(c => new ImageResizeService(_adminConnectionString, _sCConnectionString));
            services.AddTransient<emailService>(c => new emailService(_Mail, _DisplayName, _Password, _Host, _Port));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE"));
            app.UseMvc();

        }
    }
}
