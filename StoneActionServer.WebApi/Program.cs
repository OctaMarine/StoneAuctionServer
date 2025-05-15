using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using StoneActionServer.BusinessLogic;
using StoneActionServer.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddBusinessLogic(builder.Configuration);

var authSettings = builder.Configuration.GetSection("AuthSettings").Get<AuthSettings>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.SecretKey)),
    };
});
builder.Services.AddAuthorization();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseDefaultFiles();

// WebGL
StaticFileOptions option = new StaticFileOptions();
FileExtensionContentTypeProvider contentTypeProvider = (FileExtensionContentTypeProvider)option.ContentTypeProvider ?? new FileExtensionContentTypeProvider();

contentTypeProvider.Mappings.Add(".unityweb", "application/octet-stream");
contentTypeProvider.Mappings.Add(".br", "application/brotli");
contentTypeProvider.Mappings.Add(".data.br", "application/brotli");
contentTypeProvider.Mappings.Add(".wasm.br", "application/wasm");
contentTypeProvider.Mappings.Add(".js.br", "application/javascript");
contentTypeProvider.Mappings.Add(".symbols.js.br", "application/octet-stream");

option.ContentTypeProvider = contentTypeProvider;

app.UseStaticFiles(option);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

