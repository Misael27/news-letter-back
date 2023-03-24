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
builder.Services.AddSwaggerGen();
var assembly = AppDomain.CurrentDomain.Load("NewsLetter.Application");
builder.Services.AddMediatR(assembly);

builder.Services.Configure<NewsLetterDatabaseSettings>(builder.Configuration.GetSection("NewsLetterDatabase"));
builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<INewsLetterRepository, NewsLetterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
