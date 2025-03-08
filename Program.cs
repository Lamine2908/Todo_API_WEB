using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;
using TodoApi;
using TodoApi.Dtos;
using TodoApi.Endpoints;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<StudentDb>(opt => opt.UseInMemoryDatabase("StudentList"));
builder.Services.AddDbContext<TeacherDb>(opt => opt.UseInMemoryDatabase("TeacherList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(
    config =>{
        config.DocumentName = "Application Programming Interface";
        config.Title = "Application Programming Interface(API)";
    }
);

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>{
        config.DocumentTitle = "APIs";
        config.Path = "/api";
        config.DocExpansion = "List";
    });
}
app.MapGameEnpoints();
app.MapStudentsEndpoints();
app.MapTeacherEndpoints();
app.Run();
