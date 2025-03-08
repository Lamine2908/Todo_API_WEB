using System;
using TodoApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Endpoints;

public static class TeacherEndpoints
{
    public static RouteGroupBuilder MapTeacherEndpoints (this WebApplication app)
    {
        var group = app.MapGroup("teachers");

        group.MapPost("/", async(Teacher teacher, TeacherDb db) =>
        {
            await db.Teachers.AddRangeAsync(teacher);
            await db.SaveChangesAsync();
            return Results.Ok($"Le professeur {teacher.Name} cree avec succes.");

        }).WithParameterValidation();

        group.MapGet("/", async(TeacherDb db) => await db.Teachers.ToListAsync());

        return group;
    }
}
