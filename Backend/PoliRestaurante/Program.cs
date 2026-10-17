using Microsoft.EntityFrameworkCore;
using Polirestaurante.Repository;
using PoliRestaurante.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection"); //name of database connection
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString)); //connection to database

//Repositories
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservationStatusRepository, ReservationStatusRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
builder.Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
builder.Services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();



//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IReservationStatusService, ReservationStatusService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Testing
//RepositoryTestRunner.Run();
// close testing




using (var scope = app.Services.CreateScope())
{   
    var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
    var userRoleRepository = scope.ServiceProvider.GetRequiredService<IUserRoleRepository>();
    var reservationStatusRepository = scope.ServiceProvider.GetRequiredService<IReservationStatusRepository>();
    var tableRepository = scope.ServiceProvider.GetRequiredService<ITableRepository>();
    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
    var orderTypeRepository = scope.ServiceProvider.GetRequiredService<IOrderTypeRepository>();
    var orderStatusRepository = scope.ServiceProvider.GetRequiredService<IOrderStatusRepository>();
    var paymentStatusRepository = scope.ServiceProvider.GetRequiredService<IPaymentStatusRepository>();

    //Default Config
    DefaultConfiguration.Run(
        userRepository, userRoleRepository,
        reservationStatusRepository,
        tableRepository,
        orderRepository, orderTypeRepository, orderStatusRepository, paymentStatusRepository);
    //Menu Console
    ConsoleApp.Run(userRoleRepository, userRepository, reservationStatusRepository, tableRepository);
}

app.Run();



//----------To start api----------
//to this steps, first complete the database start up and step by step new install
//------previus installations------
//  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
//  dotnet add package Microsoft.EntityFrameworkCore.Tools
//  dotnet add package Microsoft.EntityFrameworkCore.Design
//  dotnet add package BCrypt.Net-Next

//---------------------------------
// 1. set up the database with "docker-compose up-d" or another already created database
// 2. set up the api with command "dotnet run"
// 3. To see the HTTP requests in Swagger, go to the link when the dotner run command is executed on localhost or any ip and
//      after that, in the final of the ip put "swagger" example = http://localhost:5112/swagger

//----------set up database docker----------
//  docker-compose up -d 

//----------step by step new install .NET API----------
//  docker-compose up -d
//  dotnet add package Microsoft.EntityFrameworkCore.Design
//---For database migrations---
//initial migration
//  dotnet ef migrations add InitialMigration
//make migration 
//  dotnet ef database update

//----------generate models from database to code----------
// dotnet ef dbcontext scaffold "Server=localhost;Database=PoliRestaurante;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models/Entity --context-dir Data --context ApplicationDbContext --force
