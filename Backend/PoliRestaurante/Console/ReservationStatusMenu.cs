using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public static class ReservationStatusMenu
{
    public static void ReservationStatusesMenu(IReservationStatusRepository reservationStatusRepository)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("=======================================");
            Console.WriteLine("   ConsoleApp - Reservation Status   ");
            Console.WriteLine("=======================================");
            Console.WriteLine();
            Console.WriteLine("1. Create Reservation Status");
            Console.WriteLine("2. Read Reservation Status");
            Console.WriteLine("3. Update Reservation Status");
            Console.WriteLine("4. Delete Reservation Status");
            Console.WriteLine("0. Volver");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");

            string? option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    ReservationStatusesMenu_CreateReservationStatus(reservationStatusRepository);
                    break;

                case "2":
                    ReservationStatusesMenu_GetReservationStatus(reservationStatusRepository);
                    break;

                case "3":
                    ReservationStatusesMenu_UpdateReservationStatus(reservationStatusRepository);
                    break;

                case "4":
                    ReservationStatusesMenu_DeleteReservationStatus(reservationStatusRepository);
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

    public static void ReservationStatusesMenu_CreateReservationStatus(IReservationStatusRepository reservationStatusRepository)
    {
        Console.WriteLine("========== CREATE RESERVATION STATUS ==========");
        Console.WriteLine();

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("❌ Nombre inválido.");
            ConsoleApp.Pause();
            return;
        }

        if (reservationStatusRepository.ReservationStatusExists(name))
        {
            Console.WriteLine("❌ Ya existe un Reservation Status con ese nombre.");
            ConsoleApp.Pause();
            return;
        }

        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        ReservationStatus reservationStatus = new ReservationStatus
        {
            Name = name,
            Description = description
        };

        var response = reservationStatusRepository.CreateReservationStatus(reservationStatus);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ Reservation Status Creado");
        }
        else
        {
            Console.WriteLine("❌ No se pudo crear el Reservation Status.");
        }
        ConsoleApp.Pause();
    }

    public static void ReservationStatusesMenu_GetReservationStatus(IReservationStatusRepository reservationStatusRepository)
    {
        Console.WriteLine("========== GET RESERVATION STATUS ==========");
        Console.WriteLine();

        Console.Write("ID del Reservation Status: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("❌ ID inválido.");
            ConsoleApp.Pause();
            return;
        }

        var getReservationStatusResponse = reservationStatusRepository.GetReservationStatus(id);

        Console.WriteLine();

        if (getReservationStatusResponse != null)
        {
            Console.WriteLine("✅ Reservation Status encontrado.");
            Console.WriteLine($"ID: {getReservationStatusResponse.Id}");
            Console.WriteLine($"Name: {getReservationStatusResponse.Name}");
            Console.WriteLine($"Description: {getReservationStatusResponse.Description}");
        }
        else
        {
            Console.WriteLine("❌ Reservation Status no encontrado.");
        }
        ConsoleApp.Pause();
    }

    public static void ReservationStatusesMenu_UpdateReservationStatus(IReservationStatusRepository reservationStatusRepository)
    {
        Console.WriteLine("========== UPDATE RESERVATION STATUS ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID del Reservation Status a modificar: ");
        int id = int.Parse(Console.ReadLine()!);

        ReservationStatus? reservationStatus = reservationStatusRepository.GetReservationStatus(id);
        if (reservationStatus == null)
        {
            Console.WriteLine("❌ Reservation Status no encontrado.");
            ConsoleApp.Pause();
            return;
        }

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("❌ Nombre inválido.");
            ConsoleApp.Pause();
            return;
        }

        // Solo validamos duplicado si el nombre realmente cambió
        if (name != reservationStatus.Name && reservationStatusRepository.ReservationStatusExists(name))
        {
            Console.WriteLine("❌ Ya existe otro Reservation Status con ese nombre.");
            ConsoleApp.Pause();
            return;
        }

        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        reservationStatus.Name = name;
        reservationStatus.Description = description;

        var response = reservationStatusRepository.UpdateReservationStatus(reservationStatus);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ Reservation Status Actualizado");
        }
        else
        {
            Console.WriteLine("❌ No se pudo actualizar el Reservation Status.");
        }
        ConsoleApp.Pause();
    }

    public static void ReservationStatusesMenu_DeleteReservationStatus(IReservationStatusRepository reservationStatusRepository)
    {
        Console.WriteLine("========== DELETE RESERVATION STATUS ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID del Reservation Status a eliminar: ");
        int id = int.Parse(Console.ReadLine()!);

        ReservationStatus? reservationStatusToEliminate = reservationStatusRepository.GetReservationStatus(id);
        if (reservationStatusToEliminate == null)
        {
            Console.WriteLine("❌ Reservation Status no encontrado.");
        }
        else
        {
            var response = reservationStatusRepository.DeleteReservationStatus(reservationStatusToEliminate);

            Console.WriteLine();

            if (response)
            {
                Console.WriteLine("✅ Reservation Status Eliminado");
            }
            else
            {
                Console.WriteLine("❌ No se pudo eliminar el Reservation Status.");
            }
        }

        ConsoleApp.Pause();
    }
}