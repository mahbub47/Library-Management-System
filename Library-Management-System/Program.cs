using Library_Management_System;
using Library_Management_System.Data;
using Library_Management_System.Entities;
using Library_Management_System.Services;
using Library_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
builder.Services.AddScoped<ILibraryManagementService, LibraryManagementService>();


builder.Services.AddDbContext<LibraryManagementDBContext>((serviceProvider, optionBuilder) =>
{
    optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Database"));

    optionBuilder.AddInterceptors(new AuditInterceptor());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
