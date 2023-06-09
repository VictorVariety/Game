﻿namespace Game;

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
        Hp = 50;
        MaxHp = 50;
        Level = 1;
        Xp = 0;
        MaxXp = 50;
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

    public static Character CreateCharacter()
    {
        string? name;
        do
        {
            Console.Clear();
            Console.WriteLine("What's your name?");
            name = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(name));

        var classCheck = true;
        var type = "";
        while (classCheck)
        {
            Console.Clear();
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

            Console.Clear();
        }

        Console.WriteLine($"You are {name} the {type}\n");
        Console.WriteLine($"Your adventure starts");
        Character.AnyButtonToContinue();
        return new Character(name, type);
    }

    public static void LevelUp(Character you)
    {
        you.Level += 1;
        you.MaxHp += 10;
        you.Hp = you.MaxHp;
        you.Xp = 0 + (you.MaxXp - you.Xp);
        you.MaxXp += 10;
        Console.Clear();
        Console.WriteLine($"You reached level {you.Level}! Max HP increased by 10, to {you.MaxHp}");
        AnyButtonToContinue();
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