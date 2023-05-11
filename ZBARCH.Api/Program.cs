using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using ZBARCH.Api.Mappings;
using ZBARCH.DataAccess.Abstract;
using ZBARCH.DataAccess.Concrete.EFCore;

var builder = WebApplication.CreateBuilder(args);


#region DataProtection
builder.Services.AddDataProtection();
builder.Services.AddCors();

var serviceProvider = builder.Services.BuildServiceProvider();
var _provider = serviceProvider.GetService<IDataProtectionProvider>();
var protector = _provider.CreateProtector(builder.Configuration["Protector_Key"]);
#endregion

#region AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new TestMapper(protector));
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);
#endregion

#region Service Resolves
builder.Services.AddScoped<ITestDal, TestDal>();
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FishMarket Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,

      },
      new List<string>()
    }});

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
#endregion
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(p =>
{
    p.AllowAnyOrigin();
    p.WithMethods("GET");
    p.AllowAnyHeader();
});

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();
