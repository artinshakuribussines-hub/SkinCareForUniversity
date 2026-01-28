using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCareForUniversity.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "نام و نام خانوادگی الزامی است.")]
    [StringLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "شماره دانشجویی الزامی است.")]
    [StringLength(20)]
    public string StudentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "شماره تماس الزامی است.")]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "انتخاب خدمت الزامی است.")]
    [StringLength(100)]
    public string ServiceType { get; set; } = string.Empty;

    [Required(ErrorMessage = "تاریخ نوبت الزامی است.")]
    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }

    [Required(ErrorMessage = "ساعت نوبت الزامی است.")]
    [DataType(DataType.Time)]
    public TimeSpan AppointmentTime { get; set; }

    [StringLength(500)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [NotMapped]
    [Range(typeof(bool), "true", "true", ErrorMessage = "پذیرش قوانین الزامی است.")]
    public bool AcceptTerms { get; set; }
}
