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
            new Items("Minor Health Potion", "Healing", 25),
            new Items("Health Potion", "Healing", 50),
            new Items("Minor Scroll", "Experience", 25),
            new Items("Scroll", "Experience", 50),
        };
        return loot[new Random().Next(0, loot.Count)];
    }
    public static Items GenerateEpicLoot()
    {
        var loot = new List<Items>
        {
            new Items("Major Health Potion", "Healing", 75),
            new Items("Major Scroll", "Experience", 75),
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

    private static void ExperienceEffect(Character you, Items? item)
    {
        you.Xp += item.Potency;
        Console.Clear();
        Console.WriteLine($"You gained {item.Potency}XP");
        if (you.Xp > you.MaxXp)
        {
            Character.LevelUp(you);
        }
    }

    private static void HealingEffect(Character you, Items? item)
    {
        you.Hp += item.Potency;
        if (you.Hp > you.MaxHp) you.MaxHp = you.Hp;
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