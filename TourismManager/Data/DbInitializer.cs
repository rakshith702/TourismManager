using TourismManager.Web.Models;

namespace TourismManager.Web.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(TourismDbContext db)
        {
            if (!db.Packages.Any())
            {
                db.Packages.AddRange(new[]
                {
                    new Package { Title = "Goa Weekend", Description = "3D/2N, beachside resort", Location = "Goa", Days = 3, Nights = 2, Price = 12999, SeatsAvailable = 20, ImageFileName = "goa.jpg" },
                    new Package { Title = "Hampi Heritage", Description = "UNESCO ruins tour", Location = "Hampi", Days = 2, Nights = 1, Price = 7999, SeatsAvailable = 15, ImageFileName = "hampi.jpg" },
                });
                await db.SaveChangesAsync();
            }
        }
    }
}
