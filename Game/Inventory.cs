namespace Game;

public class Inventory
{
    public static void OpenInventory(Character you)
    {
        var viewInventory = true;
        while (viewInventory)
        {
            Menu.ShowInventory(you);
            var choice = Character.GetNumFromUser(5);

            if (ChoseExitInventory(choice, ref viewInventory)) continue;
            if (ItemDecision(you, choice)) continue;

            Character.AnyButtonToContinue();
        }
        
        Console.Clear();

    }

    private static bool ItemDecision(Character you, int choice)
    {
        var theItem = ConvertObjectToItemsOrWeapon(you.Inventory[choice - 1]);

        Console.WriteLine($"1. {Items.FindVerb(theItem)}.\n" +
                          $"2. Drop.\n" +
                          $"3. back.");
        var decision = Character.GetNumFromUser(3);
        Console.Clear();
        switch (decision)
        {
            case 1 when theItem is Weapon weapon:
            {
                var currentWeapon = EquipWeaponFromInventory(you, weapon);
                Console.WriteLine($"You equipped {weapon.Name} and put {currentWeapon.Name} in your inventory");
                break;
            }
            case 1 when theItem is Items item:
            {
                Items.UseItem(you, item);
                you.Inventory.Remove(item);

                break;
            }
            case 2:
                Console.Clear();
                Console.WriteLine($"You dropped it");
                you.Inventory.Remove(theItem!);
                break;
            case 3:
                return true;
        }

        return false;
    }

    private static bool ChoseExitInventory(int choice, ref bool viewInventory)
    {
        if (choice != 5) return false;
        viewInventory = false;
        return true;

    }

    private static int ShowAllInventory(Character you)
    {
        Console.Clear();
        var count = 0;
        foreach (var item in you.Inventory)
        {
            count++;
            Console.WriteLine($"{count}. [ {FindNameOfItemsOrWeapon(item)} ]");
        }

        count += 1;
        Console.WriteLine($"{count}. Back");
        return count;
    }

    public static bool AttemptPickupAndEquipWeapon(Character you, Weapon weapon)
    {
        if (you.Inventory.Count < 3)
        {
            var currentWeapon = you.Hand;
            you.Hand = weapon;
            you.Inventory.Remove(weapon);
            you.Inventory.Add(currentWeapon);
            return false;
        }
        else return true;
    }
    public static Weapon EquipWeaponFromInventory(Character you, Weapon weapon)
    {
        var currentWeapon = you.Hand;
        you.Hand = weapon;
        you.Inventory.Remove(weapon);
        you.Inventory.Add(currentWeapon);
        return weapon;
    }

    public static bool AttemptPickUp(Character you, object x)
    {
        if (you.Inventory.Count < 3)
        {
                you.Inventory.Add(x);
                Console.WriteLine();
                Console.WriteLine($"You put it in your inventory.");
                Character.AnyButtonToContinue();
                return false;
        }
        else
        {
                Console.WriteLine();
                Console.Write("No room left in your inventory.");
                Character.AnyButtonToContinue();
                return true;
        }

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

    public static string? FindNameOfItemsOrWeapon(object? theItem)
    {
        if (theItem is Items items) //Hvis theItem er av Items klassen, cast den til en av Items som "items"
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