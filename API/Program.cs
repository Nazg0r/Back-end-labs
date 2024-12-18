using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(opt =>
{
	opt.UseLazyLoadingProxies()
	.UseNpgsql(conf.GetConnectionString("DbConnection"));
});

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
	string tokenKey = conf["TokenKey"] ?? throw new Exception("TokenKey not found");
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
		ValidateIssuer = false,
		ValidateAudience = false
	};
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
