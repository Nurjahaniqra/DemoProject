using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		public DbSet<Department> Departments { get; set; }

		//public override int SaveChanges()
		//{
		//	foreach (var item in ChangeTracker.Entries())
		//	{
		//		var items = item.Entity;

		//		if (item.State == EntityState.Deleted && items is ISoftDelete)
		//		{
		//			item.State = EntityState.Modified;
		//			item.GetType().GetProperty("IsDelete").SetValue(items, 1);


		//		}
		//	}
		//	return base.SaveChanges();
		//}
		public DbSet<Course> Courses { get; set; }

        public DbSet<Semester> Semesters { get; set; }
		
    }
}
