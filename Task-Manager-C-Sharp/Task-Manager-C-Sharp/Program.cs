using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Task_Manager_C_Sharp;
using Task_Manager_C_Sharp.Models;
using Task_Manager_C_Sharp.Repository;
using Task_Manager_C_Sharp.Repository.Impl;
using dotenv.net;


var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(options: new DotEnvOptions(ignoreExceptions: false));
var envVars = DotEnv.Read();

builder.Services.AddDbContext<TaskManagerCSharpContext>(options =>
{
    options.UseSqlServer(envVars["DEFAULT_CONNECTION"]);
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT
var encryptionKeyBytes = Encoding.ASCII.GetBytes(envVars["SECRET_KEY"]);
builder.Services.AddAuthentication(authentication =>
{
    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(authentication =>
{
    authentication.RequireHttpsMetadata = false;
    authentication.SaveToken = true;
    authentication.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(encryptionKeyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//Adding Cors
builder.Services.AddCors();
//Adding Scoped
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();

//------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors =>
    {
        cors.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader(); 
    }
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
