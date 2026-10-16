using Microsoft.Identity.Client;
using Polirestaurante.Repository;
using PoliRestaurante.Models;

public static class ReservationStatusMenu
{
    public static void ReservationStatussMenu(IReservationStatusRepository ReservationStatusRepository)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("   ConsoleApp - ReservationStatus   ");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("1. Create ReservationStatus");
            Console.WriteLine("2. Read ReservationStatus");
            Console.WriteLine("3. Update ReservationStatus");
            Console.WriteLine("4. Delete ReservationStatus");
            Console.WriteLine("0. Volver");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");

            string? option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    ReservationStatussMenu_CreateReservationStatus(ReservationStatusRepository);
                    break;

                case "2":
                    ReservationStatussMenu_GetReservationStatus(ReservationStatusRepository);
                    break;

                case "3":
                    ReservationStatussMenu_UpdateReservationStatus(ReservationStatusRepository);
                    break;

                case "4":
                   ReservationStatussMenu_DeleteReservationStatus(ReservationStatusRepository);
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    ConsoleApp.Pause();
                    break;
            }
        }
    }

    public static void ReservationStatussMenu_CreateReservationStatus(IReservationStatusRepository ReservationStatusRepository)
    {
        Console.WriteLine("================================");
        Console.WriteLine("   Create ReservationStatus   ");
        Console.WriteLine();

        Console.Write("Ingrese el nombre de su reserva: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Ingrese la descripción de la reserva: ");
        string description = Console.ReadLine() ?? "";

        ReservationStatus reservationStatus = new ReservationStatus
        {
            Name = name,
            Description = description
        };

        var response = ReservationStatusRepository.CreateReservationStatus(reservationStatus);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("ReservationStatus creado exitosamente.");
        }
        else
        {
            Console.WriteLine("Error al crear el ReservationStatus.");
        }
        ConsoleApp.Pause();
    }

    public static void ReservationStatussMenu_GetReservationStatus(IReservationStatusRepository ReservationStatusRepository)
    {
        Console.WriteLine("================================");
        Console.WriteLine("   Get ReservationStatus   ");
        Console.WriteLine();

             Console.Write("ID del ReservationStatus: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID inválido.");
                ConsoleApp.Pause();
                return;
            }

            var getReservationStatusResponse = ReservationStatusRepository.GetReservationStatus(id);
            Console.WriteLine();

            if (getReservationStatusResponse != null)
            {
                Console.WriteLine("✅ ReservationStatus encontrado.");
                Console.WriteLine($"ID: {getReservationStatusResponse.Id}");
                Console.WriteLine($"Nombre: {getReservationStatusResponse.Name}");
                Console.WriteLine($"Descripción: {getReservationStatusResponse.Description}");
            }
            else
            {
                Console.WriteLine("❌ Reservation no encontrado.");
            }
            ConsoleApp.Pause();
        
        
    }

    public static void ReservationStatussMenu_UpdateReservationStatus(IReservationStatusRepository ReservationStatusRepository)
    {
        Console.WriteLine("================================");
        Console.WriteLine("   Update ReservationStatus   ");
        Console.WriteLine();

        Console.Write("Ingrese el ID de la reserva a actualizar: ");
        int numero = int.Parse(Console.ReadLine()!);
        int id = numero;

        Console.WriteLine("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.WriteLine("Description: ");
        string description = Console.ReadLine() ?? "";

        ReservationStatus reservationStatus = new ReservationStatus
        {
            Id = id,
            Name = name,
            Description = description
        };

        var response = ReservationStatusRepository.UpdateReservationStatus(reservationStatus);
        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("Reserva actualizada exitosamente.");
        }
        else
        {
            Console.WriteLine("Error al actualizar la reserva.");
        }
        ConsoleApp.Pause();
    } 

    public static void ReservationStatussMenu_DeleteReservationStatus(IReservationStatusRepository ReservationStatusRepository)
    {
        Console.WriteLine("================================");
        Console.WriteLine("   Delete ReservationStatus   ");
        Console.WriteLine();

        Console.Write("Ingrese el ID de la reserva a eliminar: ");
        int numero = int.Parse(Console.ReadLine()!);
        int id = numero;

        ReservationStatus ReservaToEliminate = ReservationStatusRepository.GetReservationStatus(id);
        if (ReservaToEliminate == null)
        {
            Console.WriteLine("❌ Reserva no encontrada.");
        }
        else
        {
            var response = ReservationStatusRepository.DeleteReservationStatus(ReservaToEliminate);
            Console.WriteLine();

            if (response)
            {
                Console.WriteLine("Reserva eliminada exitosamente.");
            }
            else
            {
                Console.WriteLine("Error al eliminar la reserva.");
            }
        }
        ConsoleApp.Pause();
    }    
}