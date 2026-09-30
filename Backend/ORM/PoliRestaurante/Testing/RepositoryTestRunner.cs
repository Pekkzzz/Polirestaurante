using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polirestaurante.Repository;
using PoliRestaurante.Data;
using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

namespace Polirestaurante.Tests;

public class RepositoryTestRunner
{
    public static void Run()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            ApplicationDbContext db = CreateDbContext(configuration);

            //User Role Repo Testing
            UserRoleRepositoryTest(db);
            //User Repo Testing
            
            UserRepositoryTest(db);

            //Reservation Status Repo Testing
            ReservationStatusRepositoryTest(db);

        
    }

    public static ApplicationDbContext CreateDbContext(IConfiguration configuration)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            )
            .Options;

        return new ApplicationDbContext(options);
    }

    public static void UserRoleRepositoryTest(ApplicationDbContext db)
    {
        UserRoleRepository repository = new UserRoleRepository(db);
        Console.WriteLine("===== PRUEBA USER ROLE REPOSITORY =====");

        Console.WriteLine("= Get Test: =");

        bool exists = repository.UserRoleExists(1);

        Console.WriteLine($"¿Existe user role 1?: {exists}");

        UserRole userRole = repository.GetUserRole(1);

        if (userRole != null)
        {
            Console.WriteLine($"Name: {userRole.Name}");
            Console.WriteLine($"Description: {userRole.Description}");
            Console.WriteLine($"IsOwner: {userRole.IsOwner}");
            Console.WriteLine($"CanEditSystems: {userRole.CanEditSystems}");
            Console.WriteLine($"CanEditOrders: {userRole.CanEditOrders}");
            Console.WriteLine($"CanSetOrdersStatus: {userRole.CanSetOrdersStatus}");
            Console.WriteLine($"CanSetToPickUpStatus: {userRole.CanSetToPickUpStatus}");
            Console.WriteLine($"CanSetProductStock: {userRole.CanSetProductStock}");
            Console.WriteLine($"Skiping creation test, reset database for test creation test");
        }
        else
        {
            Console.WriteLine("UserRole dont exists. Proceding with creation test");
            Console.WriteLine("= Create Test: =");
            UserRole createUserRole = new UserRole();
            createUserRole.Name = "Default";
            createUserRole.Description = "The Default Role in System";
            createUserRole.IsOwner = false;
            createUserRole.CanEditSystems = false;
            createUserRole.CanEditOrders = false;
            createUserRole.CanSetOrdersStatus = false;
            createUserRole.CanSetToPickUpStatus = false;
            createUserRole.CanSetProductStock = false;
            if (repository.CreateUserRole(createUserRole))
            {
                Console.WriteLine($"user role created correctly");
            }
            else
            {
                Console.WriteLine($"user role cant be created correctly, test failed");
            }
        }

        
    }

    public static void UserRepositoryTest(ApplicationDbContext db)
    {
        UserRepository repository = new UserRepository(db);

        Console.WriteLine("===== PRUEBA USER REPOSITORY =====");

        Console.WriteLine("= Get Test: =");

        bool exists = repository.UserExists(1);

        Console.WriteLine($"¿Existe usuario 1?: {exists}");

        var user = repository.GetUser(1);

        if (user != null)
        {
            Console.WriteLine($"Usuario: {user.Username}");
            Console.WriteLine($"Nombre: {user.Name}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Skiping creation test, reset database for test creation test");
        }
        else
        {
            Console.WriteLine("User cant be founded.");
            Console.WriteLine("User dont exists. Proceding with creation test");
            Console.WriteLine("= Create Test: =");
            User createUser = new User();
            createUser.Username = "tester";
            createUser.Name = "tester man";
            createUser.Email = "email@teste.com";
            createUser.PasswordHash = "1234567890";
            createUser.CreationDate = DateTime.Now;
            createUser.IsDeleted = false;
            createUser.DeletedDate = null;
            createUser.UserRoleId = 1;
            if (repository.CreateUser(createUser))
            {
                Console.WriteLine($"user created correctly");
            }
            else
            {
                Console.WriteLine($"user cant be created correctly, test failed");
            }

        }

        
    }

    public static void ReservationStatusRepositoryTest(ApplicationDbContext db)
    {
        ReservationStatusRepository repository = new ReservationStatusRepository(db);

        Console.WriteLine("===== PRUEBA RESERVATION STATUS REPOSITORY =====");

        Console.WriteLine("= Get Test: =");

        bool exists = repository.ReservationStatusExists(1);

        Console.WriteLine($"¿Existe reservation status 1?: {exists}");

        var reservationStatus = repository.GetReservationStatus(1);

        if (reservationStatus != null)
        {
            Console.WriteLine($"Reservation Status: {reservationStatus.Name}");
            Console.WriteLine($"Description: {reservationStatus.Description}");
            Console.WriteLine($"Skiping creation test, reset database for test creation test");
        }
        else
        {
            Console.WriteLine("Reservation Status cant be founded.");
            Console.WriteLine("Reservation Status dont exists. Proceding with creation test");
            Console.WriteLine("= Create Test: =");
            ReservationStatus createReservationStatus = new ReservationStatus();
            createReservationStatus.Name = "Pending";
            createReservationStatus.Description = "The reservation is pending";
            if (repository.CreateReservationStatus(createReservationStatus))
            {
                Console.WriteLine($"reservation status created correctly");
            }
            else
            {
                Console.WriteLine($"reservation status cant be created correctly, test failed");
            }

        }

        
    }

    

}