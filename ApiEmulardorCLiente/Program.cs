
using ApiEmulardorCLiente.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//

var mapperConfig = new MapperConfiguration(map =>
{
    map.AddProfile<MappingProfile>();
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddAuthentication();
builder.Services.AddSingleton<ApiEmulardorCLiente.Services.ServiceTransicion>();


builder.Services.Configure<ApiEmulardorCLiente.Core.Model.SettingsMongo>(Options =>
{
    Options.ConnectionString = builder.Configuration.GetSection("MongoConnection:ConnectionString").Value;
    Options.Database = builder.Configuration.GetSection("MongoConnection:Database").Value;
});

var jwtConfig = builder.Configuration.GetSection("jwtConfig");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["secret"]))
    };
});

//

var app = builder.Build();
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
