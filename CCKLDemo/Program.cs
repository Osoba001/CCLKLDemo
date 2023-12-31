using CCKLDemo.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSqlServer<AccountDbContext>("Data Source=.;Initial Catalog=AccountDb;Integrated Security=True; Encrypt=False");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMiniProfiler(opt =>
{
    opt.RouteBasePath = "/profiler";
    opt.PopupRenderPosition = StackExchange.Profiling.RenderPosition.Left;
}).AddEntityFramework();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiniProfiler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
