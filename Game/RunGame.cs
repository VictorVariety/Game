namespace Game;

public class RunGame
{
    private static bool _running;

    public static void Start()
    {
        _running = true;
        var you = Character.CreateCharacter();

        while (_running)
        {
            if (you.Hp < 1)
            {
                _running = Menu.GameOver();
                continue;
            }
            Menu.MainMenu(you);
        }
    }

    public static void End()
    {
        _running = false;
    }
    public static void Adventure(Character you)
    {
        Console.Clear();
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
                FightAndLoot(you, encounter);
                break;
            case 2:
                encounter = Encounters.OldBattlefield(you.Level);
                ShowEncounterText(encounter);
                FightAndLoot(you, encounter);
                break;
        }
    }

    private static void ShowEncounterText(Encounters encounters)
    {
        Console.WriteLine($"You arrive in {encounters.Name}\n\n" +
                          $"{encounters.EncounterText}");
        Character.AnyButtonToContinue();
    }

    static void FightAndLoot(Character you, Encounters encounters)
    {
        if (encounters.Enemy != null) Fight.Combat(you, encounters.Enemy);

        if (encounters.Enemy != null && encounters.Enemy.Hp > 0) return;

        if (encounters.Item != null || encounters.Weapon != null) Loot(you, encounters);
    }


    private static void Loot(Character you, Encounters encounter)
    {
        if (encounter.Weapon != null)
        {
            DecisionForWeapon(you, encounter.Weapon);
        }
        if (encounter.Item != null)
        {
            DecisionForItem(you, encounter.Item);
        }

    }

    private static void DecisionForWeapon(Character you, Weapon weapon)
    {
        var weaponExists = true;
        while (weaponExists)
        {
            Console.WriteLine($"You find a {weapon.Name}\n" +
                              $"1. {Items.FindVerb(weapon)} it.\n" +
                              $"2. Put it in your inventory.\n" +
                              $"3. Leave it.");
            var i = Character.GetNumFromUser(3);                                //
            switch (i)                                                                      //New method?
            {                                                                               //
                case 1:
                    weaponExists = Inventory.AttemptPickupAndEquipWeapon(you, weapon);
                    break;
                case 2:
                    weaponExists = Inventory.AttemptPickUp(you, weapon);
                    break;
                case 3:
                    weaponExists = false;
                    break;
            }
        }

    }

    private static void DecisionForItem(Character you, Items items)
    {
        var itemExists = true;
        while (itemExists)
        {
            Console.WriteLine($"You find a {items.Name}\n" +
                              $"1. {Items.FindVerb(items)} it.\n" +
                              $"2. Put it in your inventory.\n" +
                              $"3. Leave it.");
            var i = Character.GetNumFromUser(3);
            switch (i)
            {
                case 1:
                    Items.UseItem(you, items);
                    itemExists = false;
                    break;
                case 2:
                    itemExists = Inventory.AttemptPickUp(you, items);
                    break;
                case 3:
                    itemExists = false;
                    break;
            }
        }
    }
}

