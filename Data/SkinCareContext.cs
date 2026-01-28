using Microsoft.EntityFrameworkCore;
using SkinCareForUniversity.Models;

namespace SkinCareForUniversity.Data;

public class SkinCareContext : DbContext
{
    public SkinCareContext(DbContextOptions<SkinCareContext> options) : base(options)
    {
    }

    public DbSet<Appointment> Appointments => Set<Appointment>();
}
