using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos;

public class Teacher
{
    public int Id {get; set;}

   [Required] [StringLength(15)] public string? Name {get; set;}

    [Required] public bool IsPresent {get; set;}
}
