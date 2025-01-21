using Clinic.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Clinic.DataGateway
{
    public class AppDbContext : DbContext
    {
        private static AppDbContext _instance;
        public static AppDbContext getInstance() { 
            if( _instance == null )
                _instance = new AppDbContext();
            return _instance;
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            using (StreamReader r = new StreamReader("Config.json")) {
                string connectionString = JObject.Parse(r.ReadToEnd())["ConnectionStrings"]["PostgresConnection"].Value<string>();
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}
