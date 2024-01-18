using Auctionieer.Data;
using Auctionieer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServices(builder.Configuration);//Register the extension of the ServiceRegistration class


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
try
{
    DbInitializer.InitDb(app);
    Console.WriteLine("Executed");
}
catch (Exception e)
{

    Console.WriteLine(e.Message);
}

app.Run();
