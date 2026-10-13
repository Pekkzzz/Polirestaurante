
using PoliRestaurante.Models;

public static class TableMenu
{
    public static void TablesMenu(ITableRepository TableRepository)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("   ConsoleApp - Table   ");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("1. Create Table");
            Console.WriteLine("2. Read Table");
            Console.WriteLine("3. Update Table");
            Console.WriteLine("4. Delete Table");
            Console.WriteLine("0. Volver");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");

            string? option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    TablesMenu_CreateTable(TableRepository);
                    break;

                case "2":
                    TablesMenu_GetTable(TableRepository);
                    break;

                case "3":
                    TablesMenu_UpdateTable(TableRepository);
                    break;

                case "4":
                    TablesMenu_DeleteTable(TableRepository);
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

    public static void TablesMenu_CreateTable(ITableRepository TableRepository)
    {
        Console.WriteLine("================================");
        Console.WriteLine("   Create Table   ");
        Console.WriteLine("================================");
        Console.WriteLine();

        Console.Write("Numero de mesa: ");
        int number = int.Parse(Console.ReadLine()!);

        Console.Write("Capacidad de la mesa: ");
        int capacity = int.Parse(Console.ReadLine()!);


        Table newTable = new Table
        {
            TableNum = number,
            Capacity = capacity
        };

       var response = TableRepository.CreateTable(newTable);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ Mesa Creada exitosamente.");
        }
        else
        {
            Console.WriteLine("❌ No se pudo crear la Mesa.");
            
        }
        ConsoleApp.Pause();
    }

    public static void TablesMenu_GetTable(ITableRepository TableRepository)
    {
        Console.WriteLine("================================");
        Console.WriteLine("   Get Table   ");
        Console.WriteLine();

        Console.Write("Ingrese el ID de la mesa: ");
        int id = int.Parse(Console.ReadLine()!);

        var table = TableRepository.GetTable(id);

        if (table != null)
        {
            Console.WriteLine($"ID: {table.Id}");
            Console.WriteLine($"Numero de mesa: {table.TableNum}");
            Console.WriteLine($"Capacidad: {table.Capacity}");
        }
        else
        {
            Console.WriteLine("❌ Mesa no encontrada.");
        }
        ConsoleApp.Pause();
    }

    public static void TablesMenu_UpdateTable(ITableRepository TableRepository)
    {
        Console.WriteLine("================================");
        Console.WriteLine("   Update Table   ");
        Console.WriteLine();

        Console.Write("Ingrese el ID de la mesa a actualizar: ");
        int numero = int.Parse(Console.ReadLine()!);
        int id = numero;

        Console.Write("Ingrese el nuevo numero de mesa: ");
        int number = int.Parse(Console.ReadLine()!);

        Console.Write("ingrese la capacidad de la mesa: ");
        int capacity = int.Parse(Console.ReadLine()!);

        Table table = new Table
        {
            Id = id,
            TableNum = number,
            Capacity = capacity
        };

       var response = TableRepository.UpdateTable(table);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ Mesa actualizada exitosamente.");
        }
        else
        {
            Console.WriteLine("❌ No se pudo actualizar la Mesa.");
        }
        ConsoleApp.Pause();
    }

    public static void TablesMenu_DeleteTable(ITableRepository TableRepository)
    {
       Console.WriteLine("========== DELETE TABLE ==========");
        Console.WriteLine();

        Console.Write("Ingrese el ID de la mesa a eliminar: ");
        int numero = int.Parse(Console.ReadLine()!);

        int id = numero;

        Table tableToEliminate = TableRepository.GetTable(id);
        if (tableToEliminate == null)
        {
            Console.WriteLine("Tabla no se pudo encontrar");
        }

        else
        {
            var response = TableRepository.DeleteTable(tableToEliminate);
            Console.WriteLine();

            if (response)
            {
                Console.WriteLine("Tabla eliminada");
            }
            else
            {
                Console.WriteLine("no se pudo actualizar");
            }
        }

        ConsoleApp.Pause();
    }
}
    