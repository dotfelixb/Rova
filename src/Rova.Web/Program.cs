using System;
using System.Net;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rova.Core;
using Rova.Core.Services;
using Rova.Data.Repository;
using Rova.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMediatR(typeof(IFeatureService));
builder.Services.AddAutoMapper(typeof(ModelProfiles));
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
}).AddFluentValidation(f => f.RegisterValidatorsFromAssemblyContaining<IFeatureService>(includeInternalTypes: true));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient("Default", c =>
{
    IHttpContextAccessor context = new HttpContextAccessor();
    var request = context?.HttpContext?.Request;
    c.BaseAddress = new Uri($"{request?.Scheme}://{request?.Host}");
    c.Timeout = TimeSpan.FromSeconds(120);

    c.DefaultRequestHeaders.Add("User-Agent", "Rova-Web");
    //c.DefaultRequestHeaders.Add("Accept", "application/json");
    //c.DefaultRequestHeaders.Add("Content-Type", "application/json");
});
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();

// Register Rova Services
builder.Services.AddSingleton<CustomerService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(ex =>
    {
        ex.Run(async ctx =>
        {
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ctx.Response.ContentType = "application/json";

            var ctxFeature = ctx.Features.Get<IExceptionHandlerFeature>();
            var exceptionMsg = "";

            if (ctxFeature is { })
            {
                exceptionMsg = ctxFeature.Error.Message;
            }

            var error = new ErrorResult
            {
                Errors = new[] { "Internal Server Error", exceptionMsg }
            };

            await ctx.Response.WriteAsJsonAsync(error);
        });
    });
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();
