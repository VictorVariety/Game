using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace Game;

public class Menu
{
    public static void MainMenu(Character you)
    {
        Console.Clear();
        Console.WriteLine($"      What next?" +
                          $"\n \n" +
                          $"1. Look for adventures\n" +
                          $"2. Open inventory\n" +
                          $"3. Show stats\n" +
                          $"4. Retire");
        var num = Character.GetNumFromUser(4);
        switch (num)
        {
            case 1:
                RunGame.Adventure(you);
                break;
            case 2:
                Inventory.OpenInventory(you);
                break;
            case 3:
                ShowCharacter(you);
                break;
            case 4:
                RunGame.End();
                break;
        }
    }
    public static bool GameOver()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine(@"
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⠀⠀⢀⣤⣤⣤⣶⣶⣷⣤⣀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣶⣶⣶⠀⠀⠀⠀⣠⣾⣿⣿⡇⠀⣿⣿⣿⣿⠿⠛⠉⠉⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣀⣀⠀⠀⠀⠀⠀⢀⣿⣿⣶⡀⠀⠀⠀⠀⠀⣾⣿⣿⣿⡄⠀⢀⣴⣿⣿⣿⣿⠁⢸⣿⣿⣿⣀⣤⡀⠀⠀⠀
⠀⠀⠀⠀⠀⣠⣴⣶⣿⣿⣿⣿⣿⣷⠀⠀⠀⠀⣼⣿⣿⣿⣧⠀⠀⠀⠀⢰⣿⣿⣿⣿⣇⣠⣿⣿⣿⣿⣿⡏⢠⣿⣿⣿⣿⣿⡿⠗⠂⠀⠀
⠀⠀⠀⣰⣾⣿⣿⠟⠛⠉⠉⠉⠉⠋⠀⠀⠀⣰⣿⣿⣿⣿⣿⣇⣠⣤⣤⣿⣿⣿⢿⣿⣿⣿⣿⢿⣿⣿⡿⠀⣼⣿⣿⡟⠉⠁⢀⣀⡄⠀⠀
⠀⢀⣾⣿⡿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣴⣿⣿⣿⣿⡿⣿⣿⣿⡏⠈⢿⣿⣿⠏⣾⣿⣿⠃⢠⣿⣿⣿⣶⣶⣿⣿⣿⡷⠦⠀
⢠⣾⣿⡿⠀⠀⠀⣀⣠⣴⣶⣿⣿⡷⠀⣠⣿⣿⣿⣿⡿⠟⣿⣿⣿⣠⣿⣿⣿⠀⠀⠀⠀⠁⢸⣿⣿⡏⠀⣼⣿⣿⣿⠿⠛⠛⠉⠀⠀⠀⠀
⢸⣿⣿⠣⣴⣾⣿⣿⣿⣿⣿⣿⡿⠃⣰⣿⣿⣿⠋⠁⠀⠀⠸⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠸⠿⠿⠀⠀⠛⠛⠟⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠸⣿⣿⣆⣉⣉⣭⣿⣿⣿⡿⠋⠀⠀⢿⣿⡿⠁⠀⠀⠀⠀⠀⠹⠟⠛⠛⠛⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠙⠿⣿⣿⣿⣿⡿⠟⠋⠀⠀⠀⠀⠀  ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣤⣶⣶⣶⣶⣦⣄⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣷⠄⣤⣤⣤⣤⣶⣾⣷⣴⣿⣿⣿⣿⠿⠿⠛⣻⣿⣿⣷⡄
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣠⣤⣄⠀⣶⣶⣤⡀⠀⠀⠀⠀⠀⠀⢀⣴⣿⠋⢠⣿⣿⣿⠿⠛⠋⠉⠛⣿⣿⣿⠏⢀⣤⣾⣿⣿⡿⠋⠀
⠀⠀⠀⠀⠀⠀⠀⠀⣠⣴⣾⣿⣿⣿⣿⠓⢹⣿⣿⣷⠀⠀⠀⠀⢀⣶⣿⡿⠁⠀⣾⣿⣿⣟⣠⣤⠀⠀⢸⣿⣿⣿⣾⣿⣿⣿⡟⠋⠀⠀⠀
⠀⠀⠀⠀⠀⠀⣠⣾⣿⣿⡿⠛⠉⠸⣿⣦⡈⣿⣿⣿⡇⠀⠀⣰⣿⣿⡿⠁⠀⢸⣿⣿⣿⣿⣿⠿⠷⢀⣿⣿⣿⣿⡿⠛⣿⣿⣿⡀⠀⠀⠀
⠀⠀⠀⠀⢀⣼⣿⣿⡿⠋⠀⠀⠀⠀⣿⣿⣧⠘⣿⣿⣿⡀⣼⣿⣿⡟⠀⠀⢀⣿⣿⣿⠋⠁⠀⣀⣀⣼⣿⣿⡟⠁⠀⠀⠘⣿⣿⣧⠀⠀⠀
⠀⠀⠀⠀⣼⣿⣿⡟⠀⠀⠀⠀⠀⣠⣿⣿⣿⠀⢹⣿⣿⣿⣿⣿⡟⠀⠀⠀⣼⣿⣿⣷⣶⣿⣿⣿⣿⣿⣿⡟⠀⠀⠀⠀⠀⠸⣿⣿⡆⠀⠀
⠀⠀⠀⠀⢹⣿⣿⣇⠀⠀⢀⣠⣴⣿⣿⣿⡿⠀⠈⣿⣿⣿⣿⡟⠀⠀⠀⢰⣿⣿⣿⠿⠟⠛⠉⠁⠸⢿⡟⠀⠀⠀⠀⠀⠀⠀⠘⠋⠁⠀⠀
⠀⠀⠀⠀⠈⢻⣿⣿⣿⣾⣿⣿⣿⣿⣿⠟⠁⠀⠀⠸⣿⣿⡿⠁⠀⠀⠀⠈⠙⠛⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠉⠛⠿⠿⠿⠿⠟⠋⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀");
        Character.AnyButtonToContinue();
        return false;
    }
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
        const char verticalLines = '│';
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
        var horizontalLines = new string(horizontalLine, boxWidth);

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
        Console.WriteLine($"{cornerTopLeft}{horizontalLines}{mergingCornerTop}{horizontalLines}{cornerTopRight}");
        Console.WriteLine($"{verticalLines}1.{slot1}{verticalLines}2.{slot2}{verticalLines}");
        Console.WriteLine($"{mergingCornerLeft}{horizontalLines}{mergingCornerMiddle}{horizontalLines}{mergingCornerRight}");
        Console.WriteLine($"{verticalLines}3.{slot3}{verticalLines}4.{slot4}{verticalLines}");
        Console.WriteLine($"{cornerBottomLeft}{horizontalLines}{mergingCornerBottom}{horizontalLines}{cornerBottomRight}");
        Console.WriteLine($"5. Exit");
        return count;
    }

    private static string CenterText(string text, int width)
    {
        return text.PadLeft((width - text.Length) / 2 + text.Length).PadRight(width);
    }


    private static string GetProgressBar(int current, int max)
    {
        var percentage = (int)((double)current / max * 100);
        var filledBlocks = percentage / 5;
        var emptyBlocks = 20 - filledBlocks;

        var progressBar = new string('█', filledBlocks);
        progressBar += new string(' ', emptyBlocks);

        return progressBar;
    }
}