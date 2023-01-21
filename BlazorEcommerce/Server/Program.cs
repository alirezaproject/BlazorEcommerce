using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using BlazorEcommerce.Server;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


// Add services to the container.
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers().AddApplicationPart(AssemblyRefrence.Assembly);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ICartService, CartService>();

builder
    .Services
    .Scan(s =>
        s
            .FromAssemblies(AssemblyRefrence.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor Ecommerce V1"); });

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();


app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();