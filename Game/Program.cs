using Game;

Console.SetWindowSize(463, 738);

var you = CreateCharacter();

var running = true;
while (running)
{
    MainMenu();
}
void MainMenu()
{
    ClearScreen();
    Console.WriteLine($"1. Look for adventures" +
                      $"2. Open inventory" +
                      $"3. Retire");
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
    Console.WriteLine("You travel towards adventuress directions and end up in" +
                      "1. The forest" +
                      "2. The old battlefield");
    var numChoice = Character.GetNumFromUser(2);
    Encounters encounter;
    switch (numChoice)
    {
        case 1:
            encounter = Encounters.Forest(you.Level);
            Console.WriteLine($"You arrive in {encounter.Name}" +
                              $"{encounter.EncounterText}");
            Console.ReadKey();
            if(encounter.Enemy != null ) Fight.Combat(you, encounter.Enemy);

            Loot(encounter);
            break;
        case 2:
            encounter = Encounters.OldBattlefield(you.Level);
            Console.WriteLine($"You arrive in {encounter.Name}" +
                              $"{encounter.EncounterText}");
            break;
    }
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
        Console.WriteLine($"Are you a" +
                          $"1. Warrior," +
                          $"2. Mage or" +
                          $"3. Rogue?"
        );

        switch (Character.GetNumFromUser(3))
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

    Character you = new Character(name, type);
    Console.WriteLine($"You are {name} the {type}");
    Console.WriteLine($"Your adventure starts");
    return you;
}

void Loot(Encounters encounter)
{
    var item = encounter.Item;
    Console.WriteLine($"You find a {item.Name}" +
                      $"1. {Items.FindVerb(item)} it." +
                      $"2. Put it in your inventory");
    var choice = Character.GetNumFromUser(2);
    switch (choice)
    {
        case 1:
            Items.UseItem(you, item);
            break;
        case 2:
            AttemptPickUp(item);
            break;
    }
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
