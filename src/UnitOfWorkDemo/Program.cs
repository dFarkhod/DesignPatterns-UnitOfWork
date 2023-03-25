using Microsoft.EntityFrameworkCore;
using UnitOfWorkDemo;
using UnitOfWorkDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseInMemoryDatabase("StaffDb"));

builder.Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddTransient<IDateTimeService, SystemDateTimeService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
