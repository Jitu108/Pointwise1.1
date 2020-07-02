using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.ServiceInterfaces;
using Pointwise.Domain.Services;
using Pointwise.SqlDataAccess.SQLContext;
using Pointwise.SqlDataAccess.SqlRepositories;
using AutoMapper;
using Pointwise.API.Admin.Mapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Pointwise.API.Admin
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
            #region Add Cors
            services.AddCors(); 
            #endregion

            #region DB Context
            services.AddDbContext<PointwiseSqlContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PointwiseDBConnection")));
            #endregion

            #region Repository DI
            services.AddScoped<IArticleRepository, SqlArticleRepository>();
            services.AddScoped<IAuthRepository, SqlAuthRepository>();
            services.AddScoped<ICategoryRepository, SqlCategoryRepository>();
            services.AddScoped<ISourceRepository, SqlSourceRepository>();
            services.AddScoped<IImageRepository, SqlImageRepository>();
            services.AddScoped<ITagRepository, SqlTagRepository>();
            services.AddScoped<IUserRepository, SqlUserRepository>();
            services.AddScoped<IUserRoleRepository, SqlUserRoleRepository>();
            services.AddScoped<IUserTypeRepository, SqlUserTypeRepository>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            #endregion

            #region Services DI
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ISourceService, SourceService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Swagger Gen
            services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("PointwiseAPISpec",
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "Pointwise Admin API",
                            Version = "1",
                            Description = "The Admin API is meant for providing back-end service to front-end. The front-end is to be provided to super-admin, authors or editors who are responsible to create news content.",
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                            {
                                Email = "gupta.jitendra108@gmail.com",
                                Name = "Jitendra Gupta"
                            }
                        });

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                    });

                    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                    options.IncludeXmlComments(cmlCommentsFullPath);
                }); 
            #endregion

            #region Authentication
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            var appSetings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSetings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });
            #endregion

            #region Add AutoMapper
            services.AddAutoMapper(typeof(Mappings));
            services.AddAutoMapper(typeof(ArticleMapping));
            services.AddAutoMapper(typeof(UserMapping));
            #endregion

            #region Add Controllers
            services.AddControllers();
            #endregion

            #region Add Routing
            services.AddRouting(options => options.LowercaseUrls = true); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/PointwiseAPISpec/swagger.json", "Pointwise API");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
