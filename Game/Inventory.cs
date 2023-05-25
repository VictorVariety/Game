namespace Game;

public class Inventory
{
    public static void OpenInventory(Character you)
    {
        var count = 0;
        Console.Clear();
        for (var index = 0; index < you.Inventory.Length; index++)
        {
            var item = you.Inventory[index];
            if (item == null) continue;
            count++;
            Console.WriteLine($"{count}. {item}");
        }

        var choice = Character.GetNumFromUser(count);
        var theItem = ConvertObjectToItemsOrWeapon(you.Inventory[choice - 1]);

        Console.WriteLine($"1. {Items.FindVerb(theItem)}." +
                          $"2. Drop.");
        var decision = Character.GetNumFromUser(2);
        Console.Clear();
        if (decision == 1)
        {
            if (theItem is Weapon weapon)
            {
                var currentWeapon = Character.Hand;
                Character.Hand = weapon;
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
}