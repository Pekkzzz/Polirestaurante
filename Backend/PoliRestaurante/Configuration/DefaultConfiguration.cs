using PoliRestaurante.Models.Entity;

public static class DefaultConfiguration
{  
    public static string DefaulUserRoleName = "Default";
    private static string[] DefaultOrderTypesNames = {"InRestaurant", "ToPickUp"};
    public static string DefaultOrderTypeName = DefaultOrderTypesNames[0];
    private static string[] DefaultOrderStatusNames = {"InQueue", "InPreparation", "Ready", "Delivered", "Finished"};
    public static string DefaultOrderStatusName = DefaultOrderStatusNames[0];
    private static string[] DefaultPaymentStatusNames = {"Pending", "Paid"};
    public static string DefaultPaymentStatusName = DefaultPaymentStatusNames[0];
    private static string[] DefaultProductTypeNames = {"General"};
    public static string DefaultProductTypeName = DefaultPaymentStatusNames[0];
    private static string[] DefaultOrderDetailStatusNames = {"InQueue", "Accepted", "Refused", "InPreparation", "Ready"};
    public static string DefaultOrderDetailStatusName = DefaultPaymentStatusNames[0];

    public static void Run(
        IUserRepository userRepository, IUserRoleRepository userRoleRepository, 
        IReservationStatusRepository reservationStatusRepository,
        ITableRepository tableRepository, 
        IOrderRepository orderRepository, IOrderTypeRepository orderTypeRepository, IOrderStatusRepository orderStatusRepository, IPaymentStatusRepository paymentStatusRepository
        
        )
    {
        bool running = (
                DefaultUserConfigs(userRoleRepository, userRepository) &&
                DefaultOrderConfigs(orderTypeRepository, orderStatusRepository, paymentStatusRepository)
                );
            
        if (!running)
        {
            Console.WriteLine("================================");
            Console.WriteLine(" Polirestaurante - Config ERROR ");
            Console.WriteLine("================================");
            Console.WriteLine("ERROR: AN ERROR OCURRED DURING CONFIGURATION");
            Console.WriteLine("1. Continue With The Error (Not Recomended)");
            Console.WriteLine("2. Stop Program");
            Console.WriteLine();

            string? option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    UserRoleMenu.UserRolesMenu(userRoleRepository);
                    running = false;
                    break;

                case "2":
                    UserMenu.UsersMenu(userRepository);
                    Console.WriteLine("Error crítico. Cerrando programa...");
                    Environment.Exit(1); // 1 indica que terminó con error
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    Console.WriteLine("Error crítico. Cerrando programa...");
                    Environment.Exit(1); // 1 indica que terminó con error
                    break;
            }
        }
        


    }

    public static bool DefaultUserConfigs(IUserRoleRepository userRoleRepository, IUserRepository userRepository)
    {
        // User Roles
        if (!userRoleRepository.UserRoleExists(DefaulUserRoleName))
        {
            UserRole defaultUserRole = new UserRole
            {
                Name = DefaulUserRoleName,
                Description = "Default User Role",
                IsOwner = false,
                CanEditSystems = false,

                CanEditOrders = false,

                CanSetOrdersStatus = false,

                CanSetToPickUpStatus = false,

                CanSetProductStock = false,
            };

            if (!userRoleRepository.CreateUserRole(defaultUserRole))
            {

                Console.WriteLine("Configuration - UserRole: Default User Role Not Working");
                return false;
            }    
        }
        Console.WriteLine("Configuration - UserRole: Config Finished");
        return true;

        // Users


        
    }

    public static bool DefaultOrderConfigs(
        IOrderTypeRepository orderTypeRepository, 
        IOrderStatusRepository orderStatusRepository, 
        IPaymentStatusRepository paymentStatusRepository)
    {
        // Order Type
        foreach (string orderTypeName in DefaultOrderTypesNames)
        {
            if (!orderTypeRepository.Exists(orderTypeName))
            {
                OrderType orderType = new OrderType
                {
                    Name = orderTypeName,
                    Description = ""
                };

                if (!orderTypeRepository.Create(orderType))
                {
                    Console.WriteLine($"Configuration - OrderTypes: Cant create '{orderTypeName}' Order Type");
                    return false;
                }
            }
        }
        Console.WriteLine("Configuration - OrderTypes: Config Finished");

        // Order Status
        foreach (string orderStatusName in DefaultOrderStatusNames)
        {
            if (!orderStatusRepository.Exists(orderStatusName))
            {
                OrderStatus orderStatus = new OrderStatus
                {
                    Name = orderStatusName,
                    Description = ""
                };

                if (!orderStatusRepository.Create(orderStatus))
                {
                    Console.WriteLine($"Configuration - OrderStatus: Cant create '{orderStatusName}' Order Status");
                    return false;
                }
            }
        }
        Console.WriteLine("Configuration - OrderStatus: Config Finished");

        // Payment Status
        foreach (string paymentStatusName in DefaultPaymentStatusNames)
        {
            if (!paymentStatusRepository.Exists(paymentStatusName))
            {
                PaymentStatus paymentStatus = new PaymentStatus
                {
                    Name = paymentStatusName,
                    Description = ""
                };

                if (!paymentStatusRepository.Create(paymentStatus))
                {
                    Console.WriteLine($"Configuration - PaymentStatus: Cant create '{paymentStatusName}' Payment Status");
                    return false;
                }
            }
        }
        Console.WriteLine("Configuration - PaymentStatus: Config Finished");
        
        
        return true;
    }

}