namespace Game;

public class Inventory
{
    public static void OpenInventory(Character you)
    {
        
        var viewInventory = true;
        while (viewInventory)
        {
            Console.Clear();
            var count = 0;
            foreach (var item in you.Inventory)
            {
                if (item == null) continue;
                count++;
                Console.WriteLine($"{count}. [ {FindNameOfItemsOrWeapon(item)} ]");
            }
            count += 1;
            Console.WriteLine($"{count}. Back");

            var choice = Character.GetNumFromUser(count);
            if (choice == count)
            {
                viewInventory = false;
                continue;
            }
            var theItem = ConvertObjectToItemsOrWeapon(you.Inventory[choice-1]);

            Console.WriteLine($"1. {Items.FindVerb(theItem)}.\n" +
                              $"2. Drop.");
            var decision = Character.GetNumFromUser(2);
            Console.Clear();
            if (decision == 1)
            {
                if (theItem is Weapon weapon)
                {
                    var currentWeapon = you.Hand;
                    you.Hand = weapon;
                    Console.WriteLine($"You equipped {weapon.Name} and put {currentWeapon.Name} in your inventory");
                }
                else if (theItem is Items item)
                {
                    Items.UseItem(you, item);
                    Console.WriteLine($"You {Items.FindVerb(item)} it and feel {(item.Effect == "Healing" ? "healthier" : "smarter")}");
                }
            }
            else if (decision == 2)
            {
                you.Inventory[choice - 1] = null;
            }

            Character.AnyButtonToContinue();
        }
            

        


        Console.Clear();

        
        
    }

    private static object? ConvertObjectToItemsOrWeapon(object? theItem)
    {
        if (theItem is Items items) //Hvis theItem er Items, cast den til en Items som items
        {
            Console.WriteLine($"{items.Name}");
            theItem = items;
        }
        else if (theItem is Weapon weapon)
        {
            Console.WriteLine($"{weapon.Name}");
            theItem = weapon;
        }

        return theItem;
    }
    private static object? FindNameOfItemsOrWeapon(object? theItem)
    {
        if (theItem is Items items) //Hvis theItem er Items, cast den til en Items som items
        {
            return items.Name;
        }
        if (theItem is Weapon weapon)
        {
            return weapon.Name;
        }

        return null;
    }
}