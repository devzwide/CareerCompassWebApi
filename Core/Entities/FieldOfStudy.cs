using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class FieldOfStudy
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;
}