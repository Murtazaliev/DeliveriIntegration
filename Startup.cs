using Delivery.SelfServiceKioskApi.DbModel;
using Delivery.SelfServiceKioskApi.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Delivery.SelfServiceKioskApi.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Sentry;
using Delivery.SelfServiceKioskApi.Infrastructure.Logging;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using Delivery.SelfServiceKioskApi.Infrastructure.Middlewares;

namespace Delivery.SelfServiceKioskApi
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
            services.AddDbContext<DeliveryKioskApiContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<INomenclatureService, NomenclatureService>();

            var loggerOptions = new FileLoggerOptions();
            Configuration.GetSection("Logging").GetSection("RoundTheCodeFile").GetSection("Options").Bind(loggerOptions);
            services.AddSingleton(typeof(FileLoggerOptions), loggerOptions);

            services.AddControllers()
            .ConfigureApiBehaviorOptions(opt =>
            {
                var defaultFactory = opt.InvalidModelStateResponseFactory;
                opt.InvalidModelStateResponseFactory = context =>
                {
                    AllowSynchronousIO(context.HttpContext);

                    var result = defaultFactory(context);

                    var bad = result as BadRequestObjectResult;
                    if (bad?.Value is ValidationProblemDetails problem)
                        LogInvalidModelState(context, problem);

                    return result;

                    static void AllowSynchronousIO(HttpContext httpContext)
                    {
                        IHttpBodyControlFeature? maybeSyncIoFeature = httpContext.Features.Get<IHttpBodyControlFeature>();
                        if (maybeSyncIoFeature is IHttpBodyControlFeature syncIoFeature)
                            syncIoFeature.AllowSynchronousIO = true;
                    }

                    static void LogInvalidModelState(ActionContext actionContext, ValidationProblemDetails error)
                    {
                        var errorJson = System.Text.Json.JsonSerializer.Serialize(error);

                        var reqBody = actionContext.HttpContext.Request.Body;
                        if (reqBody.CanSeek) reqBody.Position = 0;
                        var sr = new System.IO.StreamReader(reqBody);
                        string body = sr.ReadToEnd();

                        actionContext.HttpContext
                            .RequestServices.GetRequiredService<ILogger>()
                            .LogWarning($"Invalid model state. Responded: {errorJson}. Received: {body}");
                    }
                };
            })
            .AddJsonOptions(options =>
            {

                //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                
            });


            services.AddSwaggerGen();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,

                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,

                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Delivery v1"));

            app.UseHttpsRedirection();
            app.UseSentryTracing();
            app.UseRouting();

            app.UseAuthorization();
            app.EnableRequestBodyRewind();

            app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "text/html";
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                SentrySdk.CaptureException(ex.Error);
                            }
                        });
                }
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
