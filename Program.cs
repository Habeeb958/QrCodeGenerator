var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

// Redirect to index.html for root path and any non-API frontend routes
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;

    // Allow API routes through
    if (path.StartsWith("/api") || path.StartsWith("/movies") || path.StartsWith("/qrcode"))
    {
        await next();
    }
    else if (path == "/" || !Path.HasExtension(path)) // Redirect root and non-file paths
    {
        context.Request.Path = "/index.html";
        await next();
    }
    else
    {
        await next();
    }
});

// Enable routing
app.UseRouting();

app.UseAuthorization();

// Map API controllers
app.MapControllers();

// Run the app
app.Run();
