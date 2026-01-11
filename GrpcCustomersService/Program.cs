using GrpcCustomersService.Services;
using Microsoft.EntityFrameworkCore;
using Blaga_Alexia_Denisa_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Blaga_Alexia_Denisa_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Blaga_Alexia_Denisa_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Blaga_Alexia_Denisa_Lab2Context' not found.")));
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GrpcCRUDService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
