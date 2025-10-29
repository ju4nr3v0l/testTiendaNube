using SistePay.TiendaNube.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("TiendaNube", client =>
{
    client.DefaultRequestHeaders.Add("User-Agent", "SistePayApp (sistecredito.com/contacto)");
});

builder.Services.AddScoped<TiendaNubeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

// Servir archivos estáticos (checkout.js)
app.UseStaticFiles();

app.MapControllers();

app.Run();
