using System.Reflection.Emit;
using System.Security.Claims;
using System.Xml.Linq;

namespace Game;

public class Menu
{
    public static void ShowCharacter(Character you)
    {
        var title = $"{you.Name} the {you.Class}";
        var titlePadLength = (int)Math.Round((40 - title.Length) / 2.0);
        var titleBar = title.PadLeft(titlePadLength + title.Length, ' ');

        var level = $"Level: {you.Level}";
        var levelPadLength = (int)Math.Round((40 - level.Length) / 2.0);
        var levelBar = level.PadLeft(levelPadLength + level.Length, ' ');

        var weapon = $"Equipped with a {you.Hand.Name}";
        var weaponPadLength = (int)Math.Round((40 - weapon.Length) / 2.0);
        var weaponBar = weapon.PadLeft(weaponPadLength + weapon.Length, ' ');

        var hpLine = $"HP: {you.Hp}/{you.MaxHp}".PadRight(15, ' ');
        var hpBar = $"|{GetProgressBar(you.Hp, you.MaxHp)}|".PadLeft(25, ' ');

        var xpLine = $"XP: {you.Xp}/{you.MaxXp}".PadRight(15, ' ');
        var xpBar = $"|{GetProgressBar(you.Xp, you.MaxXp)}|".PadLeft(25, ' ');

        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"{titleBar}\n" +
                          $"{levelBar}\n" +
                          $"{weaponBar}\n " +
                          $"\n" +
                          $"{hpLine + hpBar}\n" +
                          $"{xpLine + xpBar}\n");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();
        Character.AnyButtonToContinue();
    }

    public static int ShowInventory(Character you)
    {
        const int slotWidth = 15;
        const int boxWidth = 17;
        const char horizontalLine = '─';
        const char verticalLine = '│';
        const char cornerTopLeft = '┌';
        const char cornerTopRight = '┐';
        const char cornerBottomLeft = '└';
        const char cornerBottomRight = '┘';
        const char mergingCornerLeft = '├';
        const char mergingCornerRight = '┤';
        const char mergingCornerTop = '┬';
        const char mergingCornerBottom = '┴';
        const char mergingCornerMiddle  = '┼';
        const string emptySlot = "Empty";

        var object1 = you.Inventory.Count > 0 ? Inventory.FindNameOfItemsOrWeapon(you.Inventory[0]) : emptySlot;
        var object2 = you.Inventory.Count > 1 ? Inventory.FindNameOfItemsOrWeapon(you.Inventory[1]) : emptySlot;
        var object3 = you.Inventory.Count > 2 ? Inventory.FindNameOfItemsOrWeapon(you.Inventory[2]) : emptySlot;
        var object4 = you.Inventory.Count > 3 ? Inventory.FindNameOfItemsOrWeapon(you.Inventory[3]) : emptySlot;

        var slot1 = CenterText(object1!, slotWidth);
        var slot2 = CenterText(object2!, slotWidth);
        var slot3 = CenterText(object3!, slotWidth);
        var slot4 = CenterText(object4!, slotWidth);

        var count = you.Inventory.Count;

        Console.Clear();
        Console.WriteLine($"{cornerTopLeft}{new string(horizontalLine, boxWidth)}{mergingCornerTop}{new string(horizontalLine, boxWidth)}{cornerTopRight}");
        Console.WriteLine($"{verticalLine}1.{slot1}{verticalLine}2.{slot2}{verticalLine}");
        Console.WriteLine($"{mergingCornerLeft}{new string(horizontalLine, boxWidth)}{mergingCornerMiddle}{new string(horizontalLine, boxWidth)}{mergingCornerRight}");
        Console.WriteLine($"{verticalLine}3.{slot3}{verticalLine}4.{slot4}{verticalLine}");
        Console.WriteLine($"{cornerBottomLeft}{new string(horizontalLine, boxWidth)}{mergingCornerBottom}{new string(horizontalLine, boxWidth)}{cornerBottomRight}");
        Console.WriteLine($"5. Exit");
        return count;
    }

    private static string CenterText(string text, int width)
    {
        return text.PadLeft((width - text.Length) / 2 + text.Length).PadRight(width);
    }


    private static string GetProgressBar(int current, int max)
    {
        int percentage = (int)((double)current / max * 100);
        int filledBlocks = percentage / 5;
        int emptyBlocks = 20 - filledBlocks;

        string progressBar = new string('█', filledBlocks);
        progressBar += new string(' ', emptyBlocks);

        return progressBar;
    }
}