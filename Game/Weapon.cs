namespace Game;

public class Weapon
{
    public string Name { get; }
    public string Type { get; }
    public double Damage { get; }

    public Weapon(string name, string type, double damage)
    {
        Name = name;
        Type = type;
        Damage = damage;
    }

    public static Weapon WeaponPool()
    {
        string[] names = { "Rusty Sword", "Iron Sword", "Basic Staff", "Forest Staff", "Wool Shear", "Iron Dagger" };
        string[] types = { "Melee", "Melee", "Magic", "Magic", "Deceit", "Deceit" };
        int[] powers = { 2, 4, 2, 4, 2, 4 };
        var random = new Random().Next(0, names.Length);

        return new Weapon(names[random], types[random], powers[random]);
    }
    public static Weapon RareWeaponPool()
    {
        string[] names = { "Steel Sword", "Ebony Sword", "Dragon Staff", "Staff of Duality", "Steel Dagger", "Ebony Dagger" };
        string[] types = { "Melee", "Melee", "Magic", "Magic", "Deceit", "Deceit" };
        int[] powers = { 7, 10, 7, 10, 7, 10 };
        var random = new Random().Next(0, names.Length);

        return new Weapon(names[random], types[random], powers[random]);
    }
}