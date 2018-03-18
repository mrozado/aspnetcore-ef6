using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BCP.Data;
using Microsoft.EntityFrameworkCore;
using BCP.Bussines;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BCP.Api.Core
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
            services.AddMvc();

            services.AddDbContext<BCPContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Example")));

            //Descomentar para validación de JWT, cambiar el String por el correspondiente
            //byte[] symmetricKey = Convert.FromBase64String("856FECBA3B06519C8DDDBC80BB080553");
            //SymmetricSecurityKey secret = new SymmetricSecurityKey(symmetricKey);
            //TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //    ValidateActor = false,
            //    ValidateLifetime = true,
            //    IssuerSigningKey = secret
            //};
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //         .AddJwtBearer(options =>
            //         {
            //             options.TokenValidationParameters = tokenValidationParameters;
            //             ///options.RequireHttpsMetadata = false;
            //         });
            //services.AddAuthorization(options => { options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build(); });

            services.AddTransient<PersonaService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
