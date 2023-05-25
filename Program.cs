

using ConsoleApp4;
using System;

namespace VendingMachineApp
{
    class Program
    {
        // The Main method: This is the entry point of the application. It initializes a VendingMachine object, creates a User object with an initial balance of 100, and enters a loop that allows the user to interact with the vending machine.
        public static void Main()
        {
            VendingMachine vendingMachine = StartVendingMachine();
            User user = new User(100);

            while (true)
            {
                DisplayMenu(vendingMachine);
                string userInput = GetUserInput();

                if (userInput.ToLower() == "x")
                {
                    break;
                }
                else
                {
                    if (int.TryParse(userInput, out int itemIndex))
                    {
                        int quantity = GetQuantity();
                        if (quantity > 0)
                        {
                            HandleUserTransaction(vendingMachine, user, itemIndex - 1, quantity);
                        }
                        else
                        {
                            Console.WriteLine("Transaction cancelled.");
                        }
                    }
                }
            }
        }
        // The InitializeVendingMachine method: This method creates a list of VendingMachineItem objects and starting a VendingMachine object with these items. Each item has a name, price, and initial stock quantity.
        static VendingMachine StartVendingMachine()
        {
            List<VendingMachineItem> items = new List<VendingMachineItem>
        {
            new VendingMachineItem("** Cola ************", 1.70m, 5),
            new VendingMachineItem("** Pepsi ***********", 1.65m, 4),
            new VendingMachineItem("** Fanta ***********", 1.80m, 4),
            new VendingMachineItem("** Bavaria *********", 1.80m, 4),
            new VendingMachineItem("** Heineken ********", 2.75m, 6),
            new VendingMachineItem("** Wine ************", 3.75m, 7),
            new VendingMachineItem("** Red bull ********", 2.50m, 9),
            new VendingMachineItem("** Monster *********", 2.40m, 6),
            new VendingMachineItem("** Something magic *", 9.99m, 3)
        };
            return new VendingMachine(items);
        }
        // The DisplayMenu method: This method displays the vending machine menu by iterating over the items in the vending machine and printing their index, name, price, and stock quantity.
        static void DisplayMenu(VendingMachine vendingMachine)
        {
            Console.WriteLine("\n\tVending Machine Menu:\n\n");
            for (int i = 0; i < vendingMachine.Items.Count; i++)
            {
                VendingMachineItem item = vendingMachine.Items[i];
                Console.WriteLine($"{i + 1}.\t {item.Name}\t  ${item.Price}\t in stock: {item.Stock}");


            }
        }
        // The GetUserInput method: This method prompts the user to enter the item number they want to purchase or to type "exit" to quit. It reads the user's input from the console and returns it as a string.
        static string GetUserInput()
        {
            Console.WriteLine("\n\nEnter the item number to purchase or type 'x' to quit:");
            return Console.ReadLine();
        }
        // The DisplaySelectedItem method: This method displays the selected item, the chosen quantity, and the total price to the user.
        static void DisplaySelectedItem(VendingMachineItem selectedItem, int quantity, decimal totalPrice)
        {
            Console.WriteLine($"You have selected {quantity} units of {selectedItem.Name}, Price: ${totalPrice}");
        }

        // The GetQuantity method: This method prompts the user to enter the desired quantity of the selected item. The user can press Enter for a default quantity of 1 or press Escape to cancel the transaction. The method reads the user's input from the console and returns the selected quantity as an integer.
        static int GetQuantity()
        {
            Console.WriteLine("Enter the quantity  (1-9):\nPress ENTER for default(1), or ESC to cancel.\n");  

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    return 0;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return 1;
                }
                else if (char.IsDigit(keyInfo.KeyChar))
                {
                    int quantity = int.Parse(keyInfo.KeyChar.ToString()); // quantity 
                    if (quantity >= 1 && quantity <= 9)
                    {
                        return quantity;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }
        // The ConfirmPurchase method: This method confirms the user's purchase by displaying the selected item, quantity, total cost, and the user's current balance. The user is prompted to press Y or Enter to confirm the purchase or N or Escape to cancel. The method returns a boolean value indicating the user's decision.
        static bool ConfirmPurchase(VendingMachineItem selectedItem, User user, int quantity)
        {
            decimal totalCost = selectedItem.Price * quantity;
            Console.WriteLine($"\n\tThank you ! for choosing our Machine :");
            Console.WriteLine($"\nItem: {selectedItem.Name}");
            Console.WriteLine($"Quantity: {quantity}");
            Console.WriteLine($"\nTotal cost: ${totalCost}");
            Console.WriteLine($"\n\tCurrent balance: ${user.Balance}");
            Console.WriteLine("\nPress (Y) or ENTER to confirm, (N) or ESC to cancel:");

            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                if (keyInfo.Key == ConsoleKey.Y || keyInfo.Key == ConsoleKey.Enter)
                {
                    return true;
                }
                else if (keyInfo.Key == ConsoleKey.N || keyInfo.Key == ConsoleKey.Escape)
                {
                    return false;
                }
            }
        }

        // The HandleUserTransaction method: This method handles the user's transaction by checking if the selected item and quantity are valid. It calculates the total cost, checks if the user has enough balance, and verifies if the item is in stock. If all conditions are met, it prompts the user to confirm the purchase. If confirmed, it updates the user's balance, reduces the stock of the selected item in the vending machine, and displays the selected item's details.
        static void HandleUserTransaction(VendingMachine vendingMachine, User user, int itemIndex, int quantity)
        {
            VendingMachineItem selectedItem = vendingMachine.GetItemByIndex(itemIndex);
            if (selectedItem != null)
            {
                if (quantity >= 0)
                {
                    decimal totalCost = selectedItem.Price * quantity;
                    if (user.HasEnoughBalance(totalCost))
                    {
                        if (vendingMachine.IsItemInStock(selectedItem, quantity))
                        {
                            if (ConfirmPurchase(selectedItem, user, quantity))
                            {
                                user.UpdateBalance(--totalCost);
                                vendingMachine.ReduceStock(selectedItem, -quantity);
                                DisplaySelectedItem(selectedItem, quantity, totalCost);
                                Console.WriteLine($"Purchased: {quantity}");
                                Console.WriteLine($"New balance: ${user.Balance}");
                                Console.ReadLine();
                                Console.Clear();
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Canceled.");
                                Console.ReadLine();
                                Console.Clear();
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, out of Stock.");
                            Console.ReadLine();
                            Console.Clear();
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance.");
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            } 
        }
    }
}