using System.ComponentModel.DataAnnotations;

namespace ComputerLaboratoryUsageMonitoringSystem.Models;

public class LaboratorySession
{
    public int Id { get; set; }

    [Display(Name = "Session Number")]
    public string? SessionNumber { get; set; }

    [Required]
    [Display(Name = "Student Number")]
    public string StudentNumber { get; set; } = "";

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = "";

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = "";

    [Required]
    public string Course { get; set; } = "";

    [Required]
    [Range(1, 5)]
    [Display(Name = "Year Level")]
    public int YearLevel { get; set; }

    [Required]
    [Display(Name = "Computer Number")]
    public string ComputerNumber { get; set; } = "";

    [Required]
    public string Purpose { get; set; } = "";

    [Display(Name = "Time In")]
    [DataType(DataType.DateTime)]
    public DateTime TimeIn { get; set; }

    [Display(Name = "Time Out")]
    [DataType(DataType.DateTime)]
    public DateTime? TimeOut { get; set; }

    public string Status { get; set; } = "Using";

    [DataType(DataType.MultilineText)]
    public string? Notes { get; set; }
}
