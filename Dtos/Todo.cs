using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApi;

public class Todo
{
    public int Id { get; set; }
    [Required] [StringLength(15)] public string? Name { get; set; }
    [Required] public bool IsComplete { get; set; }
}
