using API.Mapping;
using Microsoft.EntityFrameworkCore;
using PateintTaskTracker.Infrastructure.Repositories;
using PatientTaskTracker.Core.Interfaces;
using PatientTaskTracker.Core.Managers;
using PatientTaskTracker.Infrastructure.Data;
using PatientTaskTracker.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 4, 0))
    )
);

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IPatientRepositoryAsync, DbPatientRepository>();
builder.Services.AddScoped<ITaskRepositoryAsync, DbTaskItemRepository>();

builder.Services.AddScoped<PatientManagerAsync>();
builder.Services.AddScoped<TaskManagerAsync>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

});



var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ReactPolicy");
app.UseAuthorization();
app.MapControllers();


app.Run();


