using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gezifoni.ModalLogin.Models;
using Gezifoni.Models;

namespace Gezifoni.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<LoginUser> Uyeler { get; set; }
        public DbSet<Medya> Medyalar { get; set; }
        public DbSet<Mekan> Mekanlar { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<YolTarifBirimi> YolTarifBirimleri { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new SampleDatabaseContextInitializer());
        }
    }

    public class SampleDatabaseContextInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            context.Uyeler.Add(new LoginUser()
            {
                Email = "gokaysunum@gmail.com@gmail.com",
                Name = "Gökay",
                Surname = "Sözer",
                Username = "gokaysozer",
                Password = "123",
                ProfileImageFileName="man.png",
                RoleName = "admin"
            });

            for (int i = 0; i < 5; i++)
            {
                context.Uyeler.Add(new LoginUser()
                {
                    Email = $"uye{i}@gmail.com",
                    Name = $"Uye-{i}",
                    Surname = "Soyad",
                    Username = $"uye{i}",
                    Password = "123",
                    ProfileImageFileName = "user_boy.png",
                    RoleName = "member"
                });
            }

            context.SaveChanges();

            List<LoginUser> uyeler = context.Uyeler.ToList();

            List<string> sehirler = new List<string>()
            {
                "Ankara","Antalya","Bursa","Kars","Edirne",
                "Adana","Trabzon","Burdur","Urfa",
                "Gaziantep","Bayburt","Giresun","Rize"
            };

            List<string> sehirResimleri = new List<string>()
            {
                "sehir1.jpg","sehir2.jpg","sehir3.jpg","sehir4.jpg"
            };

            // þehir ekleme..
            foreach (string sehirAdi in sehirler)
            {
                Sehir sehir = new Sehir()
                {
                    Adi = sehirAdi,
                    Resmi = sehirResimleri[FakeData.NumberData.GetNumber(0, sehirResimleri.Count - 1)],
                    Slogan = FakeData.TextData.GetSentence(),
                    DigerBilgiler = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5, 10)),
                    GezilecekYer = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5, 10)),
                    Tarihi = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(5,10)),
                    Yemekler = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(3, 5)),
                    EkleyenUyeAdi = uyeler[FakeData.NumberData.GetNumber(0, uyeler.Count - 1)].Username,
                    OlusturmaTarihi = FakeData.DateTimeData.GetDatetime(),
                    GuncellenmeTarihi = FakeData.DateTimeData.GetDatetime()
                };

                context.Sehirler.Add(sehir);
            }

            context.SaveChanges();

            foreach (Sehir sehir in context.Sehirler.ToList())
            {
                // mekan ekleme..
                for (int i = 0; i < FakeData.NumberData.GetNumber(5, 10); i++)
                {
                    Mekan mekan = new Mekan()
                    {
                        Adi = $"Mekan - {i}",
                        Resim = sehirResimleri[FakeData.NumberData.GetNumber(0, sehirResimleri.Count - 1)],
                        Adres = FakeData.PlaceData.GetAddress(),
                        Tel = FakeData.PhoneNumberData.GetPhoneNumber(),
                        Aciklama = FakeData.TextData.GetSentence(),
                        YolTarifi = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3))
                    };

                    sehir.Mekanlar.Add(mekan);
                }

                // yorum ekleme..
                for (int i = 0; i < FakeData.NumberData.GetNumber(5, 10); i++)
                {
                    Yorum yorum = new Yorum()
                    {
                        Tarih = FakeData.DateTimeData.GetDatetime(),
                        YorumMetni = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        LoginUser = uyeler[FakeData.NumberData.GetNumber(0, uyeler.Count - 1)]
                    };

                    sehir.Yorumlar.Add(yorum);
                }

                // medya ekleme..
                // TODO : fake medyalar eklenebilir.
            }

            context.SaveChanges();

            foreach (Mekan mekan in context.Mekanlar.ToList())
            {
                // mekan yol tarif birimleri..
                for (int k = 0; k < FakeData.NumberData.GetNumber(2, 5); k++)
                {
                    mekan.YolTarifBirimleri.Add(new YolTarifBirimi()
                    {
                        Id = Guid.NewGuid(),
                        Detay = FakeData.PlaceData.GetStreetName(),
                        Lokasyon = new Lokasyon()
                        {
                            Latitude = FakeData.PlaceData.GetPostCode(),
                            Longitude = FakeData.PlaceData.GetZipCode()
                        }
                    });
                }
            }

            context.SaveChanges();
        }
    }
}
