using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Autofac Baðýmlýlýk Enjeksiyonu Yapýlandýrmasý
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

// 2. Servisler ve Baðýmlýlýklar
builder.Services.AddControllers();
builder.Services.AddCors();

// 3. IHttpContextAccessor Kaydý
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// 4. Token Options ve JWT Yapýlandýrmasý
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

// 5. Core Modüller ve Diðer Baðýmlýlýk Çözümleyicileri
builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });

// 6. Swagger / OpenAPI Yapýlandýrmasý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// 7. Yetkilendirme
builder.Services.AddAuthorization();
var app = builder.Build();

// 8. Uygulama Ortamýna Göre Swagger Yapýlandýrmasý
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

// 9. Static Files Middleware (Statik Dosyalar Ýçin)


// 10. CORS Yapýlandýrmasý
app.UseCors(options =>
    options.WithOrigins("http://localhost:4200")  // Angular uygulamanýzýn adresi
           .AllowAnyHeader()
           .AllowAnyMethod());

// 11. HTTPS Yönlendirme ve Diðer Middleware'ler
app.UseHttpsRedirection();

app.UseStaticFiles(); // Statik dosyalar için middleware burada olmalý.

// 12. Kimlik Doðrulama ve Yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();  // Kontrolcüleri haritalama

app.Run();
