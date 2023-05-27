namespace Game;

public class Character
{
    public string Name { get; set; }
    public string Class { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Level { get; set; }
    public int Xp { get; set; }
    public int MaxXp { get; set; }
    public Weapon Hand { get; set; }
    public List<object> Inventory { get; set; }

    public Character(string name, string classs)
    {
        Name = name;
        Class = classs;
        Hp = 100;
        MaxHp = 100;
        Level = 1;
        Xp = 0;
        MaxXp = 100;
        Hand = StartingWeapon(classs);
        Inventory = new List<object>();
    }

    private static Weapon StartingWeapon(string classs)
    {
        return classs switch
        {
            "Warrior" => new Weapon("Training Stick", "Melee", 1),
            "Mage" => new Weapon("Makeshift Wand", "Magic", 1),
            "Rogue" => new Weapon("Chopstick", "Deceit", 1),
            _ => throw new ArgumentOutOfRangeException(nameof(classs), classs, null)
        };
    }

    public static void LevelUp(Character you)
    {
        Console.WriteLine($"You leveled up! Max HP increased by 10");
        you.Level += 1;
        you.MaxHp += 10;
        you.Hp = you.MaxHp;
        you.Xp = 0 + (you.MaxXp - you.Xp);
    }
    public static int GetNumFromUser(int maxChoice)
    {
        var topCursorPosition = Console.CursorTop; 
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.WriteLine(); // Move to the next line
                Console.SetCursorPosition(0, topCursorPosition); // Move the cursor back to the top
                Console.Write("Invalid choice. Please try again: "); // Overwrite the line
            }
            else
            {
                if (int.TryParse(keyInfo.KeyChar.ToString(), out var choice) && choice >= 1 && choice <= maxChoice)
                {
                    Console.Clear();
                    Console.WriteLine(); // Move to the next line
                    return choice;
                }
            }
        } while (true);
        
    }

    public static void AnyButtonToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("Press any button to continue..");
        Console.ReadKey();
        Console.Clear();
    }
}