using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class Interest
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
}