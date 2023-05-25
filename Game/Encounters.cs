namespace Game;

public class Encounters
{
    public string Name { get; private set; }
    public string EncounterText { get; private set; }
    public Enemy Enemy { get; private set; }
    public Items Item { get; private set; }
    public Weapon Weapon { get; private set; }

    public Encounters(string name, string encounter, Enemy enemy, Items item, Weapon weapon)
    {
        Name = name;
        EncounterText = encounter;
        Enemy = enemy;
        Item = item;
        Weapon = weapon;
    }

    public static Encounters Forest(int level)
    {
        var encounter = string.Empty;
        Enemy enemy = null;
        Items? item = null;
        Weapon weapon = null;
        var rnd = new Random().Next(0, 3);
        switch (rnd)
        {
            case 0:
                encounter = "It's beautiful here, would be a perfect place to pick berries, but there is a noise.";
                enemy = new Random().Next(0, 2) == 1 
                    ? Enemy.GenerateGiantSpider(level) 
                    : Enemy.GenerateBear(level);
                weapon = Weapon.WeaponPool();
                item = Items.GenerateEpicLoot();
                break;
            case 1:
                encounter = "It's getting dark, and something is watching you..";
                enemy = new Random().Next(0, 2) == 1
                    ? Enemy.GenerateGhost(level)
                    : Enemy.GenerateShade(level);

                break;
            case 2:
                encounter = "You find an empty camp, and of course you loot it.";
                item = Game.Items.GenerateLoot();
                if (new Random().Next(0, 2) == 1) weapon = Weapon.WeaponPool();
                break;
        }

        return new Encounters("the forest", encounter, enemy, item, weapon);
    }

    public static Encounters OldBattlefield(int level)
    {
        var encounter = string.Empty;
        Enemy enemy = null;
        Items? item = null;
        Weapon weapon = null;
        var rnd = new Random().Next(0, 3);
        switch (rnd)
        {
            case 0:
                encounter = "All the dead bodies here probably attracted some ghouls, but there are weapons laying around so you risk it.";
                enemy = Enemy.GenerateGhoul(level);
                weapon = Weapon.WeaponPool();
                break;
            case 1:
                encounter = "It's getting dark, and something is watching you..";
                enemy = Enemy.GenerateGhost(level);
                item = Game.Items.GenerateLoot();
                break;
            case 2:
                encounter = "You find what looks like the remnants of a commander tent, there might be something valuable here.";
                item = Game.Items.GenerateLoot();
                weapon = Weapon.RareWeaponPool();
                break;
        }

        return new Encounters("the forest", encounter, enemy, item, weapon);
    }
}