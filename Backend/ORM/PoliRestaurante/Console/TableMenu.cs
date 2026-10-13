using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public static class TableMenu
{
    public static void TablesMenu(ITableRepository tableRepository)
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
                    TablesMenu_CreateTable(tableRepository);
                    break;

                case "2":
                    TablesMenu_GetTable(tableRepository);
                    break;

                case "3":
                    TablesMenu_UpdateTable(tableRepository);
                    break;

                case "4":
                    TablesMenu_DeleteTable(tableRepository);
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

    public static void TablesMenu_CreateTable(ITableRepository tableRepository)
    {
        Console.WriteLine("========== CREATE TABLE ==========");
        Console.WriteLine();

        Console.Write("TableNum: ");
        int tableNum = int.Parse(Console.ReadLine()!);

        if (tableRepository.TableNumberExists(tableNum))
        {
            Console.WriteLine("❌ Ya existe una Table con ese número.");
            ConsoleApp.Pause();
            return;
        }

        Console.Write("Capacity: ");
        int capacity = int.Parse(Console.ReadLine()!);

        Table table = new Table
        {
            TableNum = tableNum,
            Capacity = capacity
        };

        var response = tableRepository.CreateTable(table);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ Table Creada");
        }
        else
        {
            Console.WriteLine("❌ No se pudo crear la Table.");
        }
        ConsoleApp.Pause();
    }

    public static void TablesMenu_GetTable(ITableRepository tableRepository)
    {
        Console.WriteLine("========== GET TABLE ==========");
        Console.WriteLine();

        Console.Write("ID de la Table: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("❌ ID inválido.");
            ConsoleApp.Pause();
            return;
        }

        var getTableResponse = tableRepository.GetTable(id);

        Console.WriteLine();

        if (getTableResponse != null)
        {
            Console.WriteLine("✅ Table encontrada.");
            Console.WriteLine($"ID: {getTableResponse.Id}");
            Console.WriteLine($"TableNum: {getTableResponse.TableNum}");
            Console.WriteLine($"Capacity: {getTableResponse.Capacity}");
        }
        else
        {
            Console.WriteLine("❌ Table no encontrada.");
        }
        ConsoleApp.Pause();
    }

    public static void TablesMenu_UpdateTable(ITableRepository tableRepository)
    {
        Console.WriteLine("========== UPDATE TABLE ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID de la Table a modificar: ");
        int id = int.Parse(Console.ReadLine()!);

        Table? table = tableRepository.GetTable(id);
        if (table == null)
        {
            Console.WriteLine("❌ Table no encontrada.");
            ConsoleApp.Pause();
            return;
        }

        Console.Write("TableNum: ");
        int tableNum = int.Parse(Console.ReadLine()!);

        // Solo validamos duplicado si el número realmente cambió
        if (tableNum != table.TableNum && tableRepository.TableNumberExists(tableNum))
        {
            Console.WriteLine("❌ Ya existe otra Table con ese número.");
            ConsoleApp.Pause();
            return;
        }

        Console.Write("Capacity: ");
        int capacity = int.Parse(Console.ReadLine()!);

        table.TableNum = tableNum;
        table.Capacity = capacity;

        var response = tableRepository.UpdateTable(table);

        Console.WriteLine();

        if (response)
        {
            Console.WriteLine("✅ Table Actualizada");
        }
        else
        {
            Console.WriteLine("❌ No se pudo actualizar la Table.");
        }
        ConsoleApp.Pause();
    }

    public static void TablesMenu_DeleteTable(ITableRepository tableRepository)
    {
        Console.WriteLine("========== DELETE TABLE ==========");
        Console.WriteLine();

        Console.Write("Introduzca el ID de la Table a eliminar: ");
        int id = int.Parse(Console.ReadLine()!);

        Table? tableToEliminate = tableRepository.GetTable(id);
        if (tableToEliminate == null)
        {
            Console.WriteLine("❌ Table no encontrada.");
        }
        else
        {
            var response = tableRepository.DeleteTable(tableToEliminate);

            Console.WriteLine();

            if (response)
            {
                Console.WriteLine("✅ Table Eliminada");
            }
            else
            {
                Console.WriteLine("❌ No se pudo eliminar la Table.");
            }
        }

        ConsoleApp.Pause();
    }
}