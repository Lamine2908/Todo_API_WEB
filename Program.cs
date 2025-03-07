using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;
using TodoApi;
using TodoApi.Dtos;
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

app.MapGet("/todoitems", async(TodoDb db) => 

    await db.Todos.ToListAsync());

app.MapGet("/todoitems/complete", async(TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync()
);

app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo):Results.NotFound()
);

app.MapPost("/todoitems", async(Todo todo, TodoDb db) =>
    {
        db.Todos.Add(todo);
        await db.SaveChangesAsync();

        return Results.Created($"/todoitems/{todo.Id}", todo);
    }
);

app.MapPut("/itemid/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if(todo is null) return Results.NotFound();

    todo.Name=inputTodo.Name;
    todo.IsComplete=inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/itemname/{name}", async (string name, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FirstOrDefaultAsync(t => t.Name==name);

    if(todo is null) return Results.NotFound();

    todo.Name=inputTodo.Name;
    todo.IsComplete=inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    db.Todos.Remove(todo);

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapGet("/students", async(StudentDb db) =>
    await db.Students.ToListAsync()
);

app.MapPost("/students", async (Student student, StudentDb db) =>
   {
        db.Students.Add(student);
        await db.SaveChangesAsync();

        return Results.Ok(student);
    }
);


app.MapPost("/teachers", async(Teacher teacher, TeacherDb db) =>
{
    db.Teachers.Add(teacher);
    await db.SaveChangesAsync();
    return Results.Ok(teacher);
});

app.MapGet("/student/{id}", async(int id, StudentDb db) =>

    await db.Students.FindAsync(id)

        is Student st
            ?Results.Ok(st):Results.NotFound()

);
app.Run();
