using Blogs.Api.Configurations;
using Blogs.Api.Services;
using HttpBuildR.RunTime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
RegisterDependencies(builder);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterDependencies(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IBlogService, BlogService>();
    
    var blogConfig = builder.Configuration.GetSection(nameof(BlogConfig)).Get<BlogConfig>();
    builder.Services.AddSingleton(typeof(BlogConfig), blogConfig);
    builder.Services.RegisterHttpRunTime(); //.WithHttpClient(blogConfig.Name);

}
