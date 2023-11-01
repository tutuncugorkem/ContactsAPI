
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
//t�m controllerlara tek tek koyaca��m�za buraya koyaca��z filter'�m�z�!!!
builder.Services.AddControllers(options => options.Filters.Add(new ValidatorFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<BaseDtoValidator>());

//apiye kendi d�nd��� modeli kapat diyece�iz, bizim filtre �al��s�n diye! altta
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
// MVC taraf�nda supress etmene gerek yok!

//appsettingsteki configuration k�sm�na eri�mek i�in ********25.10
var teacher = builder.Configuration.GetSection("Teacher").Get<MyOptions>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//E�ER B�R F�LTER, CONSTRACTORUNDA B�R SERV�S� VEYA CLASSI DI OLARAK GE��YORSA BUNU PROGRAM CS'DE TANITMAMIZ LAZIM;
builder.Services.AddScoped(typeof(NotFoundFilter<>));

//alt sat�rda yapt���m�z interfaceleri buraya tan�ml�yoruz
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
        //appdbcontext'im API i�erisinde de�il, repository i�erisinde oldu�u i�in haber vermemiz gerekiyordu:
        //projenin ismi yerine tip g�venli dinamik verdik devam�nda..
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

    });
});


//eski uygulama notlar�:
//builder.Services.AddDbContext<ContactsAPIDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));
//�stte depend inj yapt�k, dbcontext laz�md�, tipi ise class'a verdi�imiz isim, options k�sm�n�n en i�indeki de random isim
//builder.Services.AddDbContext<ContactsAPIDbContext>(options => 
//options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiCollectionString")));

/*manuel loglama bu �ekilde
builder.Services.AddLogging(x =>
{
    x.ClearProviders(); //di�er log sa�lay�c�lar�n� salla, benim dedi�ime logla..
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

//app ile ba�lay�p use ile devam eden hepsi MiddleWare'dir!!!
app.UseHttpsRedirection();

app.UseCustomException();    //eklememiz gerekiyor MW imizi

app.UseAuthorization();

app.MapControllers();

app.Run();
