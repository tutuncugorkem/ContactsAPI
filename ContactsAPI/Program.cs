
using Contacts.Core.Repositories;
using Contacts.Core.Services;
using Contacts.Core.UnitOfWorks;
using Contacts.Repository;
using Contacts.Repository.Repositories;
using Contacts.Repository.UnitOfWorks;
using Contacts.Service.Mapping;
using Contacts.Service.Services;
using Contacts.Service.Validations;
using ContactsAPI.Filters;
using ContactsAPI.Middlewares;
using ContactsAPI.Settings;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//tüm controllerlara tek tek koyacaðýmýza buraya koyacaðýz filter'ýmýzý!!!
builder.Services.AddControllers(options => options.Filters.Add(new ValidatorFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<BaseDtoValidator>());

//apiye kendi döndüðü modeli kapat diyeceðiz, bizim filtre çalýþsýn diye! altta
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// MVC tarafýnda supress etmene gerek yok!

//appsettingsteki configuration kýsmýna eriþmek için ********25.10
var teacher = builder.Configuration.GetSection("Teacher").Get<MyOptions>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EÐER BÝR FÝLTER, CONSTRACTORUNDA BÝR SERVÝSÝ VEYA CLASSI DI OLARAK GEÇÝYORSA BUNU PROGRAM CS'DE TANITMAMIZ LAZIM;
builder.Services.AddScoped(typeof(NotFoundFilter<>));

//alt satýrda yaptýðýmýz interfaceleri buraya tanýmlýyoruz
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.Configure<MyOptions>(
    builder.Configuration.GetSection("MyOptions"));

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //appdbcontext'im API içerisinde deðil, repository içerisinde olduðu için haber vermemiz gerekiyordu:
        //projenin ismi yerine tip güvenli dinamik verdik devamýnda..
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

    });
});


//eski uygulama notlarý:
//builder.Services.AddDbContext<ContactsAPIDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));
//üstte depend inj yaptýk, dbcontext lazýmdý, tipi ise class'a verdiðimiz isim, options kýsmýnýn en içindeki de random isim
//builder.Services.AddDbContext<ContactsAPIDbContext>(options => 
//options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiCollectionString")));

/*manuel loglama bu þekilde
builder.Services.AddLogging(x =>
{
    x.ClearProviders(); //diðer log saðlayýcýlarýný salla, benim dediðime logla..
    x.SetMinimumLevel(LogLevel.Trace);
    x.AddDebug();
}); */

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app ile baþlayýp use ile devam eden hepsi MiddleWare'dir!!!
app.UseHttpsRedirection();

app.UseCustomException();    //eklememiz gerekiyor MW imizi

app.UseAuthorization();

app.MapControllers();

app.Run();
