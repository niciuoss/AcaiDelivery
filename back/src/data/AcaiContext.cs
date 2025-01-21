using Microsoft.EntityFrameworkCore;
using AcaiDeliveryAPI.Models;

namespace AcaiDeliveryAPI.Data {
    public class AcaiContext : DbContext {
        public AcaiContext(DbContextOptions<AcaiContext> options) : base(options) { }

        public DbSet<Acai> Acais { get; set; }
    }
}
