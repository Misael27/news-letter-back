using NewsLetter.Infrastructure;
using MediatR;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NewsLetter.Application.ValidationHandle.Behaviours;
using NewsLetter.Application.ValidationHandle.Filters;
using NewsLetter.Infrastructure.Repositories;
using NewsLetter.Core.IRepositories;
using NewsLetter.Application.NewsLetter.Commands.UpsertNewsLetter;
using NewsLetter.Web.AuthConfigure;
using Microsoft.Extensions.Configuration;
using NewsLetter.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);
builder.Services.AddValidatorsFromAssembly(typeof(UpsertNewsLetterCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Newsletter API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var assembly = AppDomain.CurrentDomain.Load("NewsLetter.Application");
builder.Services.AddMediatR(assembly);

builder.Services.Configure<NewsLetterDatabaseSettings>(builder.Configuration.GetSection("NewsLetterDatabase"));
builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<INewsLetterRepository, NewsLetterRepository>();

AuthConfigurer.Configure(builder.Services, builder.Configuration);

var newsLetterDatabaseSettings = builder.Configuration.GetSection("NewsLetterDatabase");

builder.Services.AddIdentity<User, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;
})
    .AddMongoDbStores<User, ApplicationRole, Guid>
    (
        newsLetterDatabaseSettings[nameof(NewsLetterDatabaseSettings.ConnectionString)], newsLetterDatabaseSettings[nameof(NewsLetterDatabaseSettings.DatabaseName)]
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
