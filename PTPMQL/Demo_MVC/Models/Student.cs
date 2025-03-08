using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_MVC.Models;

[Table("Students")]
public class Student
{
    [Key]
    public required int Id { get; set; }
    public required string FullName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }

    public double Height { get; set; }
    public double Weight { get; set; }
}