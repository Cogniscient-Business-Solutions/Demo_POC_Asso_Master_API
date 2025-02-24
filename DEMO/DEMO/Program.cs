using DEMO.Models.BusinessDL;
using DEMO.Models.DataDL.Classes;
using DEMO.Models.DataDL.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]); // Ensure key is read correctly

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key) // Key must be 256 bits or longer
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSingleton<TokenService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<OrgChartInterface, SQLData>();
builder.Services.AddScoped<OrgChartServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IData, SQLData>();
builder.Services.AddScoped<GetEmployeeByIdService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Enable authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
