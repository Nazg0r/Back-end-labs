using API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<DataContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
