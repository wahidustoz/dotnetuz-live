using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", settings =>
    {
        settings.Authority = "https://localhost:7179/";
        settings.Audience = "Api_1";
        settings.RequireHttpsMetadata = false;
        settings.TokenValidationParameters = new 
         TokenValidationParameters()
         {
            ValidateAudience = false
         };
    });

// builder.Services.AddAuthentication("Bearer")
//     .AddIdentityServerAuthentication("Bearer", options =>
//     {
//         options.ApiName = "Api_1";
//         options.Authority = "https://localhost:7179";
//     });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
