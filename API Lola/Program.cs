using API_Lola.Tools;
using Gallery_Lola_DAL.Interfaces;
using Gallery_Lola_DAL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;
using System.Text;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDbConnection, SqlConnection>( pc => new( builder.Configuration.GetConnectionString( "HomeMSSQL" ) ) );

builder.Services.AddScoped<IIndexService, IndexService>();
builder.Services.AddScoped<IFolderService, FolderService>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAccessControlService, AccessControlService>();
builder.Services.AddScoped<TokenManager>();

builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme ).AddJwtBearer(

    options => options.TokenValidationParameters = new() {
        ValidateLifetime = true,
        ValidateIssuer = true,
        //ValidIssuer = "monserverapi.com",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes( TokenManager._secretKey )
        ),
        ValidateAudience = false
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() ) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
