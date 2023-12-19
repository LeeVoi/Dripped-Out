using System.Text;
using infrastructure.DatabaseManager;
using infrastructure.DatabaseManager.Interface;
using infrastructure.Entities;
using infrastructure.Repositories;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;
using service.Helpers;
using service.Services;

var builder = WebApplication.CreateBuilder(args);


string secret = Environment.GetEnvironmentVariable("jwttokensecret");
Byte[] secretBytes = Encoding.UTF8.GetBytes(secret);


builder.Services.AddSingleton<IDBConnection, DBConnection>();
builder.Services.AddSingleton<CRUDFactory>();

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<LoginRepository>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<ProductImageRepository>();
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<UserProdRepository>();
builder.Services.AddSingleton<IColorMapper ,ColorMapperRepository>();
builder.Services.AddSingleton<ISizeMapper ,SizeMapperRepository>();
builder.Services.AddSingleton<ColorTypeRepository>();
builder.Services.AddSingleton<SizeTypeRepository>();

builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<ProductImageService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ColorService>();
builder.Services.AddSingleton<SizeService>();
builder.Services.AddSingleton<BlobService>();

builder.Services.AddSingleton<AuthenticationHelper>(new AuthenticationHelper(secretBytes));
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});


app.UseHttpsRedirection();

app.MapControllers();

app.Run();