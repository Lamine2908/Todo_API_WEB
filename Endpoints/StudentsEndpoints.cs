using System;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;

namespace TodoApi.Endpoints;

public static class StudentsEndpoints
{
    public static WebApplication MapStudentsEndpoints(this WebApplication app)
    {
        app.MapGet("/", async(StudentDb db) =>
            await db.Students.ToListAsync()
        );

        app.MapPost("/students", async (Student student, StudentDb db) =>
        {
                db.Students.Add(student);
                await db.SaveChangesAsync();

                return Results.Ok(student);
            }
        ).WithParameterValidation();

        app.MapGet("/student/{id}", async(int id, StudentDb db) =>

            await db.Students.FindAsync(id)

                is Student st
                    ?Results.Ok(st):Results.NotFound()

        );

        return app;
    }
}
