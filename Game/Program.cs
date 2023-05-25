using System.Runtime.InteropServices.ComTypes;
using Game;

Character you = null!;
while (you == null)
{
    you = CreateCharacter();
}

var running = true;
while (running)
{
    MainMenu();
}
void MainMenu()
{
    ClearScreen();
    Console.WriteLine($"What next?\n" +
                      $"1. Look for adventures\n" +
                      $"2. Open inventory\n" +
                      $"3. Retire\n");
    int num = Character.GetNumFromUser(3);
    switch (num)
    {
        case 1: 
            Adventure();
            break;
        case 2:
            Inventory.OpenInventory(you);
            break;
        case 3:
            running = false;
            break;
    }
}
void Adventure()
{
    ClearScreen();
    Console.WriteLine("You travel towards adventuress directions and end up in\n" +
                      "1. The forest\n" +
                      "2. The old battlefield");
    var numChoice = Character.GetNumFromUser(2);
    Encounters encounter;
    switch (numChoice)
    {
        case 1:
            encounter = Encounters.Forest(you.Level);

            ShowEncounterText(encounter);
            FightAndLoot(encounter);

            break;
        case 2:
            encounter = Encounters.OldBattlefield(you.Level);
            ShowEncounterText(encounter);
            FightAndLoot(encounter);

            break;
    }
}

void ShowEncounterText(Encounters encounters)
{
    Console.WriteLine($"You arrive in {encounters.Name}\n\n" +
                      $"{encounters.EncounterText}");
    Character.AnyButtonToContinue();
}

void FightAndLoot(Encounters encounters)
{
    if (encounters.Enemy != null) Fight.Combat(you, encounters.Enemy);
    if (encounters.Item != null || encounters.Weapon != null) Loot(encounters);
}

Character CreateCharacter()
{
    string? name;
    do
    {
        ClearScreen();
        Console.WriteLine("What's your name?");
        name = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(name));

    var classCheck = true;
    var type = "";
    while (classCheck)
    {
        ClearScreen();
        Console.WriteLine($"{name} the\n" +
                          $"1. Warrior.\n" +
                          $"2. Mage.\n" +
                          $"3. Rogue."
        );
        var choice = Character.GetNumFromUser(3);
        switch (choice)
        {
            case 1:
                type = "Warrior";
                classCheck = false;
                break;
            case 2:
                type = "Mage";
                classCheck = false;
                break;
            case 3:
                type = "Rogue";
                classCheck = false;
                break;
            default:
                Console.WriteLine("Invalid input");
                break;
        }

        ClearScreen();
    }

    Console.WriteLine($"You are {name} the {type}\n");
    Console.WriteLine($"Your adventure starts");
    Character.AnyButtonToContinue();
    return new Character(name, type);
}

void Loot(Encounters encounter)
{
    var weapon = encounter.Weapon;
    var item = encounter.Item;
    if (weapon != null)
    {
        DecisionForWeapon(weapon, item);
    }

    if (item != null)
    {
        DecisionForItem(item, you);
    }

}

void DecisionForWeapon(Weapon weapon1, Items items)
{
    Console.WriteLine($"You find a {weapon1.Name}\n" +
                      $"1. {Items.FindVerb(weapon1)} it.\n" +
                      $"2. Put it in your inventory");
    var i = Character.GetNumFromUser(2);
    switch (i)
    {
        case 1:
            Items.UseItem(you, items);
            break;
        case 2:
            AttemptPickUp(items);
            break;
    }

    Character.AnyButtonToContinue();
}

void DecisionForItem(Items items, Character character)
{
    Console.WriteLine($"You find a {items.Name}\n" +
                      $"1. {Items.FindVerb(items)} it.\n" +
                      $"2. Put it in your inventory");
    var i = Character.GetNumFromUser(2);
    switch (i)
    {
        case 1:
            Items.UseItem(character, items);
            break;
        case 2:
            AttemptPickUp(items);
            break;
    }

    Character.AnyButtonToContinue();
}

void ClearScreen()
{
    Console.Clear();
}

void AttemptPickUp(object? x)
{
    var attempt = true;
    var firstAvailableIndex = Array.IndexOf(you.Inventory, null);
    var topCursorPosition = Console.CursorTop;
    do
    {
        var keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.Enter)
        {
            Console.WriteLine(); // Move to the next line
            Console.SetCursorPosition(0, topCursorPosition); // Move the cursor back to the top
            Console.Write("No room left in your inventory."); // Overwrite the line
        }
        else if (firstAvailableIndex != -1)
        {
            you.Inventory[firstAvailableIndex] = x;
            x = null;
            attempt = false;
            Console.WriteLine(); // Move to the next line
        }
    } while (attempt);
}
