public static class ConsoleApp
{
    public static void Run(
        IUserRoleRepository userRoleRepository, IUserRepository userRepository, IReservationStatusRepository reservationStatusRepository,
        ITableRepository tableRepository
        )
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("   ConsoleApp Polirestaurante   ");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("1. User Roles");
            Console.WriteLine("2. Users");
            Console.WriteLine("3. Tables");
            Console.WriteLine("4. Reservations Status");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");

            string? option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    UserRoleMenu.UserRolesMenu(userRoleRepository);
                    break;

                case "2":
                    UserMenu.UsersMenu(userRepository);
                    break;

                case "3":
                    Console.WriteLine("Listar usuarios todavía no implementado.");
                    break;

                case "4":
                    Console.WriteLine("Eliminar usuario todavía no implementado.");
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    Pause();
                    break;
            }
        }
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }
}