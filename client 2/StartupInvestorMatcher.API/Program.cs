using StartupInvestorMatcher.Model.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories with the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("sim_database");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'sim_database' is not configured.");
}
builder.Services.AddScoped<MemberRepository>(_ => new MemberRepository(connectionString));
builder.Services.AddScoped<InvestorRepository>(_ => new InvestorRepository(connectionString));
builder.Services.AddScoped<StartupRepository>(_ => new StartupRepository(connectionString));
builder.Services.AddScoped<CountryRepository>(_ => new CountryRepository(connectionString));
builder.Services.AddScoped<IndustryRepository>(_ => new IndustryRepository(connectionString));
builder.Services.AddScoped<InvestmentSizeRepository>(_ => new InvestmentSizeRepository(connectionString));

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
    app.UseCors("DevelopmentCorsPolicy");
}

app.UseAuthorization();
app.MapControllers();
app.Run();
