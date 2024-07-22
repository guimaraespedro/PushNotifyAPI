using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using PushNotifyAPI;
using PushNotifyAPI.Database;
using PushNotifyAPI.Repositories;
using PushNotifyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var domain = builder.Configuration.GetSection("DomainName").Value.ToString();
// Add services to the container.
builder.Services.AddCors(options => options.AddPolicy("corsPolicy", policy =>
        policy.WithOrigins(domain).AllowAnyHeader().AllowAnyMethod()
));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Scheme = "Bearer",

        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                },
                In = ParameterLocation.Header,
                Name = "Authorization",
                Scheme = "Bearer",
            },
            new string[] {}
        }
    });


    }
 );

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<DatabaseCore>();
builder.Services.AddScoped<IPushService, PushService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IUserDeviceInfoRepository, UserDeviceInfoRepository>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
        options =>
        {
            options.Authority = "https://securetoken.google.com/pushnotify-cd22a";
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = "https://securetoken.google.com/pushnotify-cd22a",
                ValidateAudience = true,
                ValidAudience = "pushnotify-cd22a",
                ValidateLifetime = true
            };
        }
    );

FirebaseApp.Create(
    new AppOptions()
    {
        Credential = GoogleCredential.FromFile("./firebase-config.json"),
        ProjectId = "pushnotify-cd22a",
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();



app.UseCors("corsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
