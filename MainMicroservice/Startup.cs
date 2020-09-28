using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Data;
using DataAccess.Entities;
using MainMicroservice.AutoMapper;
using MainMicroservice.Implementions;
using MainMicroservice.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MainMicroservice
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
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("DataAccess")));

            services.AddIdentity<User, Role>()
               .AddRoles<Role>()
               .AddEntityFrameworkStores<DataContext>()
               .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddScoped<UserManager<User>, UserManager<User>>();
            services.AddScoped<RoleManager<Role>, RoleManager<Role>>();
            //Add automapper
            services.AddAutoMapper(typeof(AutoMapperProfiles), typeof(AutoMapperProfiles));

            //Add all repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IFlashcardRepository, FlashcardRepository>();
            services.AddScoped<IPronunciationRepository, PronunciationRepository>();
            services.AddScoped<IUserFlashcardRepository, UserFlashcardRepository>();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
               // c.SwaggerDoc("v1", new Info { Title = "Learning English API", Version = "v1" });
                //c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                //{
                //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "apiKey"
                //});
                //c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                //{
                //    {"Bearer", new string[] { }},
                //});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flashcard API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
