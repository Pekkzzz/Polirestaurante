using PoliRestaurante.Models.Entity;

public static class UserMenu
{
    public static void UsersMenu(IUserRepository userRepository)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("   ConsoleApp - User   ");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. Read User");
            Console.WriteLine("3. Update User");
            Console.WriteLine("4. Delete User");
            Console.WriteLine("0. Volver");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");

            string? option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    UsersMenu_CreateUser(userRepository);
                    break;

                case "2":
                    UsersMenu_GetUser(userRepository);
                    break;

                case "3":
                    UsersMenu_UpdateUser(userRepository);
                    break;

                case "4":
                    UsersMenu_DeleteUser(userRepository);
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

    public static void UsersMenu_CreateUser(IUserRepository userRepository)
    {
        Console.WriteLine("========== CREATE USER ROLE ==========");
        Console.WriteLine();

        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";
        
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("PasswordHash: ");
        string passwordhash = Console.ReadLine() ?? "";

        Console.Write("UserRoleID: ");
        int userRoleID = int.Parse(Console.ReadLine()!);


        User user = new User
        {
            Username = username,
            Name = name,
            Email = email,
            PasswordHash = passwordhash,
            CreationDate = DateTime.Now,
            IsDeleted = false,
            DeletedDate = null,
            UserRoleId = userRoleID

        };

        var response = userRepository.CreateUser(user);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ User Creado");
        }
        else
        {
            Console.WriteLine("❌ No se pudo crear el User.");
            
        }
        ConsoleApp.Pause();
    }

    public static void UsersMenu_GetUser(IUserRepository userRepository)
    {
        Console.WriteLine("========== GET USER ==========");
        Console.WriteLine();

        Console.WriteLine("1. Buscar por ID ");
        Console.WriteLine("2. Buscar por Username ");
        int numero = int.Parse(Console.ReadLine()!);
        if (numero == 1)
        {
            Console.Write("ID del User Role: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID inválido.");
                ConsoleApp.Pause();
                return;
            }

            var getUserResponse = userRepository.GetUser(id);

            Console.WriteLine();

            if (getUserResponse != null)
            {
                Console.WriteLine("✅ User Role encontrado.");
                Console.WriteLine($"ID: {getUserResponse.Id}");
                Console.WriteLine($"Username: {getUserResponse.Username}");
                Console.WriteLine($"Name: {getUserResponse.Name}");
                Console.WriteLine($"Email: {getUserResponse.Email}");
                Console.WriteLine($"PasswordHash: {getUserResponse.PasswordHash}");
                Console.WriteLine($"CreationDate: {getUserResponse.CreationDate}");
                Console.WriteLine($"IsDeleted: {getUserResponse.IsDeleted}");
                Console.WriteLine($"DeletedDate: {getUserResponse.DeletedDate}");
                Console.WriteLine($"UserRoleID: {getUserResponse.UserRoleId}");
            }
            else
            {
                Console.WriteLine("❌ User no encontrado.");
            }
            ConsoleApp.Pause();
        }
        else if (numero == 2)
        {
            Console.Write("Username del User: ");
            string username = Console.ReadLine() ?? "";

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("❌ Username invalido.");
                ConsoleApp.Pause();
                return;
            }

            var getUserResponse = userRepository.GetUser(username);

            Console.WriteLine();

            if (getUserResponse != null)
            {
                Console.WriteLine("✅ User Role encontrado.");
                Console.WriteLine($"ID: {getUserResponse.Id}");
                Console.WriteLine($"Username: {getUserResponse.Username}");
                Console.WriteLine($"Name: {getUserResponse.Name}");
                Console.WriteLine($"Email: {getUserResponse.Email}");
                Console.WriteLine($"PasswordHash: {getUserResponse.PasswordHash}");
                Console.WriteLine($"CreationDate: {getUserResponse.CreationDate}");
                Console.WriteLine($"IsDeleted: {getUserResponse.IsDeleted}");
                Console.WriteLine($"DeletedDate: {getUserResponse.DeletedDate}");
                Console.WriteLine($"UserRoleID: {getUserResponse.UserRoleId}");
            }
            else
            {
                Console.WriteLine("❌ User no encontrado.");
            }
            ConsoleApp.Pause();
        }
        else
        {
            Console.WriteLine("Opción no válida.");
            ConsoleApp.Pause();
        }
    }

    public static void UsersMenu_UpdateUser(IUserRepository userRepository)
    {
        Console.WriteLine("========== UPDATE USER ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID de User a modificar: ");
        int numero = int.Parse(Console.ReadLine()!);
        
        int id = numero;

        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";
        
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("PasswordHash: ");
        string passwordhash = Console.ReadLine() ?? "";

        Console.Write("UserRoleID: ");
        int userRoleID = int.Parse(Console.ReadLine()!);

        User user = new User
        {
            Username = username,
            Name = name,
            Email = email,
            PasswordHash = passwordhash,
            CreationDate = DateTime.Now,
            IsDeleted = false,
            DeletedDate = null,
            UserRoleId = userRoleID

        };

        var response = userRepository.UpdateUser(user);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ User Actualizado");
        }
        else
        {
            Console.WriteLine("❌ No se pudo actualizar el User.");
            
        }
        ConsoleApp.Pause();
    }

    public static void UsersMenu_DeleteUser(IUserRepository userRepository)
    {
        Console.WriteLine("========== DELETE USER ROLE ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID de User Role a eliminar: ");
        int numero = int.Parse(Console.ReadLine()!);
        
        int id = numero;

        User userToEliminate = userRepository.GetUser(id);
        if (userToEliminate == null)
        {
            Console.WriteLine("❌ User role cant be finded");
        }
        else
        {
            var response = userRepository.DeleteUser(userToEliminate);

            Console.WriteLine();

            if (response)
            {
                Console.WriteLine("✅ User Eliminado");
            }
            else
            {
                Console.WriteLine("❌ No se pudo actualizar el User.");
                
            }
        }
        
        ConsoleApp.Pause();
    }
}