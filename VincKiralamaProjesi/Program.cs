using Microsoft.EntityFrameworkCore;
using VincKiralamaProjesi.Data;
using VincKiralamaProjesi.Models;
using VincKiralamaProjesi.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Connection string'i appsettings.json içindeki "DefaultConnection" ile eşleştiriyoruz
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<EmailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

// Varsayılan route: Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Veritabanı boşsa örnek verileri doldur
        if (!context.Categories.Any() && !context.Firms.Any() && !context.Cranes.Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Sepetli Vinç", Description = "Yüksekte çalışma platformu olan sepetli vinçler." },
                new Category { Name = "Mobil Vinç", Description = "Tekerlekli, mobil kullanım için vinçler." },
                new Category { Name = "Kule Vinç", Description = "Şantiye ve yüksek yapılarda kullanılan kule vinçler." }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            var firms = new List<Firm>
            {
                new Firm { Name = "İstanbul Vinç", City = "İstanbul", Phone = "+90 212 111 11 11", Email = "info@istanbulvinc.com" },
                new Firm { Name = "Ankara Vinç", City = "Ankara", Phone = "+90 312 222 22 22", Email = "info@ankaravinc.com" },
                new Firm { Name = "İzmir Vinç", City = "İzmir", Phone = "+90 232 333 33 33", Email = "info@izmirvinc.com" }
            };
            context.Firms.AddRange(firms);
            context.SaveChanges();

            var sepetli = context.Categories.First(c => c.Name == "Sepetli Vinç");
            var mobil   = context.Categories.First(c => c.Name == "Mobil Vinç");
            var kule    = context.Categories.First(c => c.Name == "Kule Vinç");

            var istanbulFirm = context.Firms.First(f => f.Name == "İstanbul Vinç");
            var ankaraFirm   = context.Firms.First(f => f.Name == "Ankara Vinç");
            var izmirFirm    = context.Firms.First(f => f.Name == "İzmir Vinç");

            var cranes = new List<Crane>
            {
                new Crane
                {
                    Name = "Sepetli Vinç 20m",
                    CapacityTon = 5,
                    DailyPrice = 2500,
                    Description = "20 metre çalışma yüksekliğine sahip sepetli vinç.",
                    ImageUrl = "/images/sepetli20m.jpg",
                    CategoryId = sepetli.Id,
                    FirmId = istanbulFirm.Id,
                    City = "İstanbul",
                    District = "Kağıthane"
                },
                new Crane
                {
                    Name = "Mobil Vinç 40 Ton",
                    CapacityTon = 40,
                    DailyPrice = 5000,
                    Description = "Şehir içi ve şantiye kullanımı için 40 ton mobil vinç.",
                    ImageUrl = "/images/mobil40ton.jpg",
                    CategoryId = mobil.Id,
                    FirmId = ankaraFirm.Id,
                    City = "Ankara",
                    District = "Çankaya"
                },
                new Crane
                {
                    Name = "Kule Vinç 60m",
                    CapacityTon = 60,
                    DailyPrice = 8000,
                    Description = "Yüksek katlı inşaatlar için 60 metre kule vinç.",
                    ImageUrl = "/images/kule60m.jpg",
                    CategoryId = kule.Id,
                    FirmId = izmirFirm.Id,
                    City = "İzmir",
                    District = "Bornova"
                }
            };

            context.Cranes.AddRange(cranes);
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Seed sırasında hata: " + ex.Message);
    }
}




app.Run();
