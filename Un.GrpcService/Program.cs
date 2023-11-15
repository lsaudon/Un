using Un.GrpcService.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();

WebApplication app = builder.Build();
app.MapGrpcService<UnService>();
app.MapGet("/", () => string.Empty);
app.Run();