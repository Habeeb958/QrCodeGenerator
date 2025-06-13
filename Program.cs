var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();


app.UseCors();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    if (path.StartsWith("/api") || path.StartsWith("/movies") || path.StartsWith("/qrcode"))
    {
        await next();
    }
    else if (path == "/" || !Path.HasExtension(path)) 
    {
        context.Request.Path = "/index.html";
        await next();
    }
    else
    {
        await next();
    }
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
