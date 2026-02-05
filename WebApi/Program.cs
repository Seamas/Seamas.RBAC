using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Json;
using Wang.Seamas.RBAC.Controllers;
using SqlSugar;
using Wang.Seamas.RBAC.Configure;
using Wang.Seamas.Web.Filters;
using Wang.Seamas.Web.Common.Extensions;
using Wang.Seamas.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllers(opt =>
    {
        opt.Filters.Add<AsyncModelActionFilter>();
    })
    .ConfigureApiBehaviorOptions(opt =>
    {
        // 禁用框架自带的验证过滤器, 以便使用自定义的 AsyncModelActionFilter
        opt.SuppressModelStateInvalidFilter = true;
    })
    .AddApplicationPart(typeof(AuthController).Assembly);

builder.Services.Configure<JsonOptions>(options =>
{
    // 设置json格式
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddCustomAuthentication(opt =>
{
    opt.TokenHeaderName = "Authorization";
    opt.TokenPrefix = "Bearer ";
    opt.TokenExpiry = TimeSpan.FromHours(2);
    opt.SecretKey = builder.Configuration["Jwt:Secret"];
    opt.Issuer = builder.Configuration["Jwt:Issuer"];
    opt.Audience = builder.Configuration["Jwt:Audience"];
});


var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole(); // 或 AddDebug(), AddSerilog() 等
});
var logger = loggerFactory.CreateLogger("SqlSugar");


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{

    containerBuilder.Register(ctx =>
        {
            var config = new ConnectionConfig
            {
                ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection"),
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    EntityService = (property, entityType) =>
                    {
                        entityType.DbColumnName = property.Name.ToSnakeCase();
                        if (property.Name == "Id")
                        {
                            entityType.IsPrimarykey = true;
                            entityType.IsIdentity = true;
                        }
                    },
                    EntityNameService = (type, entityInfo) =>
                    {
                        entityInfo.DbTableName = type.Name.ToSnakeCase() + 's';
                    }
                }
            };
            
            var db = new SqlSugarClient(config);
            db.Aop.OnLogExecuting = (sql, parameters) =>
            {
                var paramStr = string.Empty;
                if (parameters != null)
                {
                    paramStr = string.Join(", ", parameters
                        .Select(p => $"{p.ParameterName}={ (!p.ParameterName.ToLowerInvariant().Contains("password") ? p.Value : "******")}")
                    );
                }

                logger.LogInformation("【SQL】\n{Sql}\nParameters: {ParamStr}", sql, paramStr);
            };
            return db;
        }).As<ISqlSugarClient>()
        .InstancePerLifetimeScope();
    
    var types = SqlSugarRegisterTypes.Types;
    foreach (var type in types)
    {
        containerBuilder.RegisterType(type)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
    }
});



var app = builder.Build();

app.UseGlobalErrorHandlerMiddleware();
app.UseJsonResultWrapperMiddleware();

app.UseRouting();

app.UseCustomAuthentication();
app.UsePermissionMiddleware();

app.MapControllers();

app.Run();
