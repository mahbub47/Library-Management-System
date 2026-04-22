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

builder.Services.AddScoped<IBookManagementService, BookManagementService>();
builder.Services.AddScoped<IMemberManagementService, MemberManagementService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();


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
