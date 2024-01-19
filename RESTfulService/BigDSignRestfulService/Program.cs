using BigDSignRestfulService.BusinesslogicLayer;
using Microsoft.OpenApi.Models;
using SignData.DatabaseLayer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "BigDSign API",
		Description = "An ASP.NET Core Web API for a distributed digital-sign booking and management system",
	});

	// using System.Reflection;
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add dependency injections for business logic layer
builder.Services.AddSingleton<IUserLogic, UserLogic>();
builder.Services.AddSingleton<IBookingLogic, BookingLogic>();
builder.Services.AddSingleton<IBookingLineLogic, BookingLineLogic>();
builder.Services.AddSingleton<IEventLogic, EventLogic>();
builder.Services.AddSingleton<IStadiumLogic, StadiumLogic>();
builder.Services.AddSingleton<ISignLogic, SignLogic>();

// Add dependency injections for data access layer
builder.Services.AddSingleton<IUserAccess, UserAccess>();
builder.Services.AddSingleton<IBookingAccess, BookingAccess>();
builder.Services.AddSingleton<IBookingLineAccess, BookingLineAccess>();
builder.Services.AddSingleton<IEventAccess, EventAccess>();
builder.Services.AddSingleton<IStadiumAccess, StadiumAccess>();
builder.Services.AddSingleton<ISignAccess, SignAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
