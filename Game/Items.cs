namespace Game;

public class Items
{
    public string Name { get; }
    public string Effect { get; }
    public int Potency { get; }

    public Items(string name, string effect, int potency)
    {
        Name = name;
        Effect = effect;
        Potency = potency;
    }

    public static Items GenerateLoot()
    {
        var loot = new List<Items>
        {
            new Items("Holy Water", "Healing", 25),
            new Items("Health Potion", "Healing", 50),
            new Items("Scroll", "Experience", 25),
            new Items("Book", "Experience", 50),
        };
        return loot[new Random().Next(0, loot.Count)];
    }
    public static Items GenerateEpicLoot()
    {
        var loot = new List<Items>
        {
            new Items("Potion of Youth", "Healing", 75),
            new Items("Great Book", "Experience", 75),
        };
        return loot[new Random().Next(0, loot.Count)];
    }
    public static void UseItem(Character you, Items item)
    {
        switch (item.Effect)
        {
            case "Healing":
            {
                HealingEffect(you, item);
                break;
            }
            case "Experience":
            {
                ExperienceEffect(you, item);
                break;
            }
        }
    }

    private static void ExperienceEffect(Character you, Items item)
    {
        you.Xp += item.Potency;
        Console.Clear();
        Console.WriteLine($"You {Items.FindVerb(item)} it and gained {item.Potency}XP");
        if (you.Xp >= you.MaxXp)
        {
            Character.LevelUp(you);
        }
    }

    private static void HealingEffect(Character you, Items item)
    {
        Console.Clear();
        var healthGained = you.MaxHp - you.Hp < item.Potency ? you.MaxHp - you.Hp : item.Potency;
        Console.WriteLine(
            $"You {Items.FindVerb(item)} it and gain {healthGained}HP");
        you.Hp += item.Potency;
        if (you.Hp > you.MaxHp) you.Hp = you.MaxHp;
        Character.AnyButtonToContinue();
    }

    public static string FindVerb(object? items)
    {
        var s = items switch
        {
            Items item => item.Effect == "Healing" ? "Drink" : "Read",
            Weapon => "Equip",
            _ => throw new ArgumentOutOfRangeException(nameof(items), items, null)
        };
        return s;
    }
}