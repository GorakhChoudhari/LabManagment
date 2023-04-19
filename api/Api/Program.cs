using Api.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var MyAllowSpeceficOrigins = "_myAllowSpeceficOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//cors
builder.Services.AddCors(Options =>
{
    Options.AddPolicy(name: MyAllowSpeceficOrigins, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
//JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
       ValidateIssuer= true,
       ValidateAudience= true,
       ValidateLifetime= true, 
       ValidateIssuerSigningKey= true,
       ValidIssuer = "localhost",
       ValidAudience = "localhost",
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
       ClockSkew=TimeSpan.Zero
    };

});



//DI
builder.Services.AddScoped<IDataAccess,DataAccess>();
builder.Services.AddScoped<IEmailService, EmailServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpeceficOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
