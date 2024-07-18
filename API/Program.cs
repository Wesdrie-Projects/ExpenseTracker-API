using API.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.RegisterPostgres();
builder.RegisterIdentity();
builder.RegisterJsonWebToken();
builder.AddBearerTokenForSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await app.SetUpDevelopmentDatabaseAsync();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
