using System;
using System.IO;
using System.Linq;

namespace AdamsApp
{
    class Program
    {
        const string EditMode = "Edit";
        const string ViewMode = "View";
        const string LocationAction = "Location";
        const string ValueAction = "Value";
        const string DeleteAction = "DELETE";
        const string EndCommand = "End";

        static void Main(string[] args)
        {
            Console.Write("View Or Edit: ");
            string mode = Console.ReadLine();

            string filePath = Path.Combine("CS690-FinalProject", "AdamsApp", "AdamsApp", "Inventory.txt");
            string content = File.ReadAllText(filePath);

            if (mode == EditMode)
            {
                string command;
                do
                {
                    Console.Write("Enter Name: ");
                    string itemName = Console.ReadLine();

                    bool containsString = content.Contains(itemName);
                    if (containsString)
                    {
                        Console.Write("Current Item! To edit please type Location, Value or DELETE: ");
                        string action = Console.ReadLine();

                        if (action == LocationAction)
                        {
                            Console.Write("Current Location is: where is the item now? ");
                            string newLocation = Console.ReadLine();
                            content = content.Replace(itemName, $"{itemName}:{newLocation}");
                        }
                        else if (action == ValueAction)
                        {
                            Console.Write("Current Value is: what is the new value? ");
                            string newValue = Console.ReadLine();
                            content = content.Replace(itemName, $"{itemName}:{newValue}");
                        }
                        else if (action == DeleteAction)
                        {
                            Console.Write("Confirm name of Item (Must Match): ");
                            string confirmName = Console.ReadLine();

                            if (confirmName == itemName)
                            {
                                content = content.Replace($"{itemName}:", "");
                            }
                        }
                    }
                    else
                    {
                        Console.Write("New Item! Enter Location: ");
                        string itemLocation = Console.ReadLine();
                        Console.Write("Enter Value: ");
                        int itemValue;

                        while (!int.TryParse(Console.ReadLine(), out itemValue))
                        {
                            Console.Write("Please enter a valid integer for Value: ");
                        }

                        content += $"{itemName}:{itemLocation}:{itemValue}{Environment.NewLine}";
                    }

                    Console.Write("Type End to End: ");
                    command = Console.ReadLine();
                } while (command != EndCommand);

                File.WriteAllText(filePath, content);
            }
            else if (mode == ViewMode)
            {
                Console.Write("Search Item Name: ");
                string itemName = Console.ReadLine();
                string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                var item = lines.FirstOrDefault(line => line.StartsWith(itemName));

                if (item != null)
                {
                    Console.WriteLine($"Item Found: {item}");
                }
                else
                {
                    Console.WriteLine("Item not found.");
                }
            }
        }
    }
}
