using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using $rootnamespace$.ModalLogin.Models;

namespace $rootnamespace$.Context
{
    public class SampleDatabaseContext: DbContext
    {
        public DbSet<LoginUser> LoginUsers { get; set; }

        public SampleDatabaseContext()
        {
            Database.SetInitializer(new SampleDatabaseContextInitializer());
        }
    }

    public class SampleDatabaseContextInitializer : CreateDatabaseIfNotExists<SampleDatabaseContext>
    {
        protected override void Seed(SampleDatabaseContext context)
        {
            context.LoginUsers.Add(new LoginUser() {
                Email = "kadirmuratbaseren@gmail.com",
                Name = "Murat",
                Surname = "Baseren",
                Username = "muratbaseren",
                Password = "123"
            });

            context.SaveChanges();
        }
    }
}
