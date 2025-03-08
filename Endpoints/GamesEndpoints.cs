using System;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;

namespace TodoApi.Endpoints;

public static class GamesEndpoints
{
    public static RouteGroupBuilder MapGameEnpoints(this WebApplication app)
    {
        var group = app.MapGroup("todoitems");

        group.MapGet("/", async(TodoDb db) => 

            await db.Todos.ToListAsync());

        group.MapGet("/complete", async(TodoDb db) =>
            await db.Todos.Where(t => t.IsComplete).ToListAsync()
        );

        group.MapGet("/{id}", async (int id, TodoDb db) =>
            await db.Todos.FindAsync(id)
                is Todo todo
                    ? Results.Ok(todo):Results.NotFound()
        );

        group.MapPost("/", async(Todo todo, TodoDb db) =>
            {
                db.Todos.Add(todo);
                await db.SaveChangesAsync();

                return Results.Created($"/todoitems/{todo.Id}", todo);
            }
        ).WithParameterValidation();

        app.MapPut("/{id}", async (int id, Todo inputTodo, TodoDb db) =>
        {
            var todo = await db.Todos.FindAsync(id);

            if(todo is null) return Results.NotFound();

            todo.Name=inputTodo.Name;
            todo.IsComplete=inputTodo.IsComplete;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }).WithParameterValidation();

        group.MapPut("/{name}", async (string name, Todo inputTodo, TodoDb db) =>
        {
            var todo = await db.Todos.FirstOrDefaultAsync(t => t.Name==name);

            if(todo is null) return Results.NotFound();

            todo.Name=inputTodo.Name;
            todo.IsComplete=inputTodo.IsComplete;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }).WithParameterValidation();

        group.MapDelete("/{id}", async (int id, TodoDb db) =>
        {
            var todo = await db.Todos.FindAsync(id);

            if (todo is null) return Results.NotFound();

            db.Todos.Remove(todo);

            await db.SaveChangesAsync();
            return Results.NoContent();
        });
        
        return group;
    }
}
