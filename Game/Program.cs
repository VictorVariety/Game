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
                      $"3. Retire");
    var num = Character.GetNumFromUser(3);
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
        DecisionForWeapon(weapon);
    }

    if (item != null)
    {
        DecisionForItem(you, item);
    }

}

void DecisionForWeapon(Weapon weapon)
{
    var weaponExists = true;
    while (weaponExists)
    {
        if (weapon != null)
        {
            Console.WriteLine($"You find a {weapon.Name}\n" +
                              $"1. {Items.FindVerb(weapon)} it.\n" +
                              $"2. Put it in your inventory.\n" +
                              $"3. Leave it.");
            var i = Character.GetNumFromUser(3);
            switch (i)
            {
                case 1:
                    weaponExists = Inventory.AttemptPickupAndEquipWeapon(you, weapon);
                    break;
                case 2:
                    weaponExists = Inventory.AttemptPickUp(you, weapon);
                    break;
                case 3:
                    weapon = null;
                    break;
            }
        }
    }

}

void DecisionForItem(Character character, Items? items)
{
    while (items != null)
    {
        Console.WriteLine($"You find a {items.Name}\n" +
                          $"1. {Items.FindVerb(items)} it.\n" +
                          $"2. Put it in your inventory.\n" +
                          $"3. Leave it.");
        var i = Character.GetNumFromUser(3);
        switch (i)
        {
            case 1:
                Items.UseItem(character, items);
                items = null;
                break;
            case 2:
                Inventory.AttemptPickUp(character, items);

                break;
            case 3:
                items = null;
                break;
        }
    }
}

void ClearScreen()
{
    Console.Clear();
}
