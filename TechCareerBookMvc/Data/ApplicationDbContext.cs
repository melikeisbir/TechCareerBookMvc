using Microsoft.EntityFrameworkCore;
using TechCareerBookMvc.Models;

namespace TechCareerBookMvc.Data
{
    //Database i oluşturmaktan hatta tabloları oluşturmaya ve o tablolara veri ekleme güncelleme silme işlemeni hatta connnection açma komut çalıştırma işlemlerinden sorumlu bir class
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        //Db Set database oluşmasını istediğimiz tablolar için kullanırız. 
        public DbSet<YeniKitap> YeniKitap { get; set; }
    }
}
