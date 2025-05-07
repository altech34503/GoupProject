using StartupInvestorMatcher.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<MemberRepository, MemberRepository>();
builder.Services.AddScoped<InvestorRepository, InvestorRepository>();
builder.Services.AddScoped<StartupRepository, StartupRepository>();
builder.Services.AddScoped<CountryRepository, CountryRepository>();
builder.Services.AddScoped<IndustryRepository, IndustryRepository>();
builder.Services.AddScoped<InvestmentSizeRepository, InvestmentSizeRepository>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentCorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Use the CORS policy during development
    app.UseCors("DevelopmentCorsPolicy");
}

app.UseAuthorization();

app.MapControllers();

app.Run();
