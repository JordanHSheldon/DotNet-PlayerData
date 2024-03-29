using EsportsProfileWebApi.INFRASTRUCTURE;
using EsportsProfileWebApi.Web.Extensions;
using EsportsProfileWebApi.Web.Orchestrators;
using EsportsProfileWebApi.Web.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuthentication(builder);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerJwtBearer();
builder.Services.AddSingleton<IDataOrchestrator, DataOrchestrator>();
builder.Services.AddSingleton<IDataRepository, DataRepository>();
builder.Services.AddSingleton<IUserOrchestrator, UserOrchestrator>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();