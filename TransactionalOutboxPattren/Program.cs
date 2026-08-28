using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TransactionalOutboxPattern.Application;
using TransactionalOutboxPattern.Application.Email;
using TransactionalOutboxPattern.Contract;
using TransactionalOutboxPattern.Contract.Email;
using TransactionalOutboxPattern.Contract.OutBox;
using TransactionalOutboxPattern.Controllers.OutBox;
using TransactionalOutboxPattern.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Register AppDbContext
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IOrderService, OrderService>();

///Register Email Service
var emailSettings = new EmailSettings();
builder.Configuration.Bind(EmailSettings.SectionName, emailSettings);
builder.Services.AddSingleton(Options.Create(emailSettings));
builder.Services.AddSingleton<IMailService, EmailService>();
builder.Services.AddScoped<IEmailOutbox, EmailOutboxes>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
