using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public static class UserRoleMenu
{
    public static void UserRolesMenu(IUserRoleRepository userRoleRepository)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("   ConsoleApp - User Role   ");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("1. Create User Role");
            Console.WriteLine("2. Read User Role");
            Console.WriteLine("3. Update User Role");
            Console.WriteLine("4. Delete User Role");
            Console.WriteLine("0. Volver");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");

            string? option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    UserRolesMenu_CreateUser(userRoleRepository);
                    break;

                case "2":
                    UserRolesMenu_GetUser(userRoleRepository);
                    break;

                case "3":
                    UserRolesMenu_UpdateUser(userRoleRepository);
                    break;

                case "4":
                    UserRolesMenu_DeleteUser(userRoleRepository);
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

    public static void UserRolesMenu_CreateUser(IUserRoleRepository userRoleRepository)
    {
        Console.WriteLine("========== CREATE USER ROLE ==========");
        Console.WriteLine();

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("isOwner: ");
        bool isOwner = bool.Parse(Console.ReadLine()!);                
        Console.Write("canEditSystems: ");
        bool canEditSystems = bool.Parse(Console.ReadLine()!);
        Console.Write("canEditOrders: ");
        bool canEditOrders = bool.Parse(Console.ReadLine()!);
        Console.Write("canSetOrdersStatus: ");
        bool canSetOrdersStatus = bool.Parse(Console.ReadLine()!);
        Console.Write("canSetToPickUpStatus: ");
        bool canSetToPickUpStatus = bool.Parse(Console.ReadLine()!);
        Console.Write("canSetProductStock: ");
        bool canSetProductStock = bool.Parse(Console.ReadLine()!);

        UserRole userRole = new UserRole
        {
            
            Name = name,
            Description = description,
            IsOwner = isOwner,

            CanEditSystems = canEditSystems,

            CanEditOrders = canEditOrders,

            CanSetOrdersStatus = canSetOrdersStatus,

            CanSetToPickUpStatus = canSetToPickUpStatus,

            CanSetProductStock = canSetProductStock

        };

        var response = userRoleRepository.CreateUserRole(userRole);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ User Role Creado");
        }
        else
        {
            Console.WriteLine("❌ No se pudo crear el User Role.");
            
        }
        ConsoleApp.Pause();
    }

    public static void UserRolesMenu_GetUser(IUserRoleRepository userRoleRepository)
    {
        Console.WriteLine("========== GET USER ROLE ==========");
        Console.WriteLine();

        Console.WriteLine("1. Buscar por ID ");
        Console.WriteLine("2. Buscar por Nombre ");
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

            var getUserRoleResponse = userRoleRepository.GetUserRole(id);

            Console.WriteLine();

            if (getUserRoleResponse != null)
            {
                Console.WriteLine("✅ User Role encontrado.");
                Console.WriteLine($"ID: {getUserRoleResponse.Id}");
                Console.WriteLine($"Name: {getUserRoleResponse.Name}");
                Console.WriteLine($"Description: {getUserRoleResponse.Description}");
                Console.WriteLine($"IsOwner: {getUserRoleResponse.IsOwner}");
                Console.WriteLine($"CanEditSystems: {getUserRoleResponse.CanEditSystems}");
                Console.WriteLine($"CanEditOrders: {getUserRoleResponse.CanEditOrders}");
                Console.WriteLine($"CanSetOrdersStatus: {getUserRoleResponse.CanSetOrdersStatus}");
                Console.WriteLine($"CanSetToPickUpStatus: {getUserRoleResponse.CanSetToPickUpStatus}");
                Console.WriteLine($"CanSetToPickUpStatus: {getUserRoleResponse.CanSetToPickUpStatus}");
            }
            else
            {
                Console.WriteLine("❌ User role no encontrado.");
            }
            ConsoleApp.Pause();
        }
        else if (numero == 2)
        {
            Console.Write("Nombre del User Role: ");
            string userRoleName = Console.ReadLine() ?? "";

            if (string.IsNullOrEmpty(userRoleName))
            {
                Console.WriteLine("❌ Nombre invalido.");
                ConsoleApp.Pause();
                return;
            }

            var getUserRoleResponse = userRoleRepository.GetUserRole(userRoleName);

            Console.WriteLine();

            if (getUserRoleResponse != null)
            {
                Console.WriteLine("✅ User Role encontrado.");
                Console.WriteLine($"ID: {getUserRoleResponse.Id}");
                Console.WriteLine($"Name: {getUserRoleResponse.Name}");
                Console.WriteLine($"Description: {getUserRoleResponse.Description}");
                Console.WriteLine($"IsOwner: {getUserRoleResponse.IsOwner}");
                Console.WriteLine($"CanEditSystems: {getUserRoleResponse.CanEditSystems}");
                Console.WriteLine($"CanEditOrders: {getUserRoleResponse.CanEditOrders}");
                Console.WriteLine($"CanSetOrdersStatus: {getUserRoleResponse.CanSetOrdersStatus}");
                Console.WriteLine($"CanSetToPickUpStatus: {getUserRoleResponse.CanSetToPickUpStatus}");
                Console.WriteLine($"CanSetToPickUpStatus: {getUserRoleResponse.CanSetToPickUpStatus}");
            }
            else
            {
                Console.WriteLine("❌ User role no encontrado.");
            }
            ConsoleApp.Pause();
        }
        else
        {
            Console.WriteLine("Opción no válida.");
            ConsoleApp.Pause();
        }
    }

    public static void UserRolesMenu_UpdateUser(IUserRoleRepository userRoleRepository)
    {
        Console.WriteLine("========== UPDATE USER ROLE ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID de User Role a modificar: ");
        int numero = int.Parse(Console.ReadLine()!);
        
        int id = numero;

        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("isOwner: ");
        bool isOwner = bool.Parse(Console.ReadLine()!);                
        Console.Write("canEditSystems: ");
        bool canEditSystems = bool.Parse(Console.ReadLine()!);
        Console.Write("canEditOrders: ");
        bool canEditOrders = bool.Parse(Console.ReadLine()!);
        Console.Write("canSetOrdersStatus: ");
        bool canSetOrdersStatus = bool.Parse(Console.ReadLine()!);
        Console.Write("canSetToPickUpStatus: ");
        bool canSetToPickUpStatus = bool.Parse(Console.ReadLine()!);
        Console.Write("canSetProductStock: ");
        bool canSetProductStock = bool.Parse(Console.ReadLine()!);

        UserRole userRole = new UserRole
        {
            Id = id,
            Name = name,
            Description = description,
            IsOwner = isOwner,

            CanEditSystems = canEditSystems,

            CanEditOrders = canEditOrders,

            CanSetOrdersStatus = canSetOrdersStatus,

            CanSetToPickUpStatus = canSetToPickUpStatus,

            CanSetProductStock = canSetProductStock

        };

        var response = userRoleRepository.UpdateUserRole(userRole);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ User Role Actualizado");
        }
        else
        {
            Console.WriteLine("❌ No se pudo actualizar el User Role.");
            
        }
        ConsoleApp.Pause();
    }

    public static void UserRolesMenu_DeleteUser(IUserRoleRepository userRoleRepository)
    {
        Console.WriteLine("========== DELETE USER ROLE ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID de User Role a eliminar: ");
        int numero = int.Parse(Console.ReadLine()!);
        
        int id = numero;

        UserRole userRoleToEliminate = userRoleRepository.GetUserRole(id);
        if (userRoleToEliminate == null)
        {
            Console.WriteLine("❌ User role cant be finded");
        }
        else
        {
            var response = userRoleRepository.DeleteUserRole(userRoleToEliminate);

            Console.WriteLine();

            if (response)
            {
                Console.WriteLine("✅ User Role Eliminado");
            }
            else
            {
                Console.WriteLine("❌ No se pudo actualizar el User Role.");
                
            }
        }
        
        ConsoleApp.Pause();
    }

}