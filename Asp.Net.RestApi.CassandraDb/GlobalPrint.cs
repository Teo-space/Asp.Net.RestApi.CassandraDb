global using static GlobalPrint;

internal static class GlobalPrint
{
    static DateTime start = DateTime.Now;

    public static void print(object? o = null, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("[");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"[{DateTime.Now.ToString("yyyy.MM.dd HH:mm.ss.fff")}]");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("]");
        Console.ForegroundColor = color;
        Console.Write(o ?? string.Empty);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write($"({Math.Round((DateTime.Now - start).TotalMilliseconds, 2)})");
        start = DateTime.Now;
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.White;
    }
    public static void print(params object[] args)
    {
        for (int i = 0; i < args.Length; i++) { print(args[i]); }
    }

}

