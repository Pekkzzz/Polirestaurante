using PoliRestaurante.Models.Entity;

public static class UserMenu
{
    public static void UsersMenu(IUserService userService)
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
                    UsersMenu_CreateUser(userService);
                    break;

                case "2":
                    UsersMenu_GetUser(userService);
                    break;

                case "3":
                    UsersMenu_UpdateUser(userService);
                    break;

                case "4":
                    UsersMenu_DeleteUser(userService);
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

    public static void UsersMenu_CreateUser(IUserService userService)
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


        User_CreateDto user_createDto = new User_CreateDto
        {
            Username = username,
            Name = name,
            Email = email,
            Password = passwordhash

        };

        UserService_Response response = userService.Create(user_createDto);

        Console.WriteLine();

        if (response.Response)
        {
            Console.WriteLine("✅ User Creado");
        }
        else
        {
            Console.WriteLine("❌ No se pudo crear el User.");
            
        }
        ConsoleApp.Pause();
    }

    public static void UsersMenu_GetUser(IUserService userService)
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

            var getUserResponse = userService.GetByID(id);

            Console.WriteLine();

            if (getUserResponse != null)
            {
                Console.WriteLine("✅ User Role encontrado.");
                Console.WriteLine($"ID: {getUserResponse.user}");
                Console.WriteLine($"Username: {getUserResponse.user.Username}");
                Console.WriteLine($"Name: {getUserResponse.user.Name}");
                Console.WriteLine($"Email: {getUserResponse.user.Email}");
                Console.WriteLine($"PasswordHash: {getUserResponse.user.PasswordHash}");
                Console.WriteLine($"CreationDate: {getUserResponse.user.CreationDate}");
                Console.WriteLine($"IsDeleted: {getUserResponse.user.IsDeleted}");
                Console.WriteLine($"DeletedDate: {getUserResponse.user.DeletedDate}");
                Console.WriteLine($"UserRoleID: {getUserResponse.user.UserRoleId}");
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

            UserService_Response getUserResponse = userService.GetByName(username);

            Console.WriteLine();

            if (getUserResponse != null)
            {
                Console.WriteLine("✅ User Role encontrado.");
                Console.WriteLine($"ID: {getUserResponse.user.Id}");
                Console.WriteLine($"Username: {getUserResponse.user.Username}");
                Console.WriteLine($"Name: {getUserResponse.user.Name}");
                Console.WriteLine($"Email: {getUserResponse.user.Email}");
                Console.WriteLine($"PasswordHash: {getUserResponse.user.PasswordHash}");
                Console.WriteLine($"CreationDate: {getUserResponse.user.CreationDate}");
                Console.WriteLine($"IsDeleted: {getUserResponse.user.IsDeleted}");
                Console.WriteLine($"DeletedDate: {getUserResponse.user.DeletedDate}");
                Console.WriteLine($"UserRoleID: {getUserResponse.user.UserRoleId}");
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

    public static void UsersMenu_UpdateUser(IUserService userService)
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

        User_UpdateDto user = new User_UpdateDto
        {
            Username = username,
            Name = name,
            Email = email,
            Password = passwordhash

        };

        UserService_Response response = userService.Update(user);

        Console.WriteLine();

        if (response.Response)
        {
            Console.WriteLine("✅ User Actualizado");
        }
        else
        {
            Console.WriteLine("❌ No se pudo actualizar el User.");
            
        }
        ConsoleApp.Pause();
    }

    public static void UsersMenu_DeleteUser(IUserService userService)
    {
        Console.WriteLine("========== DELETE USER ROLE ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID de User Role a eliminar: ");
        int numero = int.Parse(Console.ReadLine()!);
        
        int id = numero;

        UserService_Response userToEliminate = userService.GetByID(id);
        if (!userToEliminate.Response)
        {
            Console.WriteLine("❌ User role cant be finded");
        }
        else
        {
            Console.WriteLine("✅ User Eliminado");

        }
        
        ConsoleApp.Pause();
    }
}