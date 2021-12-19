var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(identity.Config.GetApis())
    .AddInMemoryClients(identity.Config.GetClients())
    .AddInMemoryApiScopes(identity.Config.GetScopes())
    .AddDeveloperSigningCredential();

// DEFAULT CORS WITH ALL ORIGINS
builder.Services.AddCors(options
    => options.AddDefaultPolicy(
        builder => 
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();   
        }
    ));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();

app.UseAuthorization();

app.UseIdentityServer();

app.MapDefaultControllerRoute();
app.Run();
