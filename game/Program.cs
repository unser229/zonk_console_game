using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;

public class Die
{
    public int Value { get; private set; }
    private static Random rng = new Random();

    public Die()
    {
        Roll();
    }

    public void Roll()
    {
        Value = rng.Next(1, 7);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    // Возвращает ASCII-представление кости с определенным числом точек
    public string[] GetAsciiRepresentation()
    {
        // 3D представление кости
        string[] dieTemplate;
        
        dieTemplate = new string[]
        {
            "    ┌─────────┐    ",
            "   ╱           ╲   ",
            "  ╱             ╲  ",
            " ╱               ╲ ",
            "┌─────────────────┐",
            "│                 │",
            "│                 │",
            "│                 │",
            "│                 │",
            "│                 │",
            "└─────────────────┘"
        };

        // Копируем шаблон, чтобы не изменять исходный массив
        string[] result = (string[])dieTemplate.Clone();
        
        // Добавляем точки в зависимости от значения кости
        switch (Value)
        {
            case 1:
                result[7] = "│        ●        │";
                break;
            case 2:
                result[6] = "│    ●           │";
                result[8] = "│           ●    │";
                break;
            case 3:
                result[6] = "│    ●           │";
                result[7] = "│        ●        │";
                result[8] = "│           ●    │";
                break;
            case 4:
                result[6] = "│    ●     ●    │";
                result[8] = "│    ●     ●    │";
                break;
            case 5:
                result[6] = "│    ●     ●    │";
                result[7] = "│        ●        │";
                result[8] = "│    ●     ●    │";
                break;
            case 6:
                result[6] = "│    ●     ●    │";
                result[7] = "│    ●     ●    │";
                result[8] = "│    ●     ●    │";
                break;
        }
        
        return result;
    }
    
    public string[] GetSmallAsciiRepresentation()
    {
        // Уменьшенное представление отложенной кости
        string[] dieTemplate = new string[]
        {
            "┌───────┐",
            "│       │",
            "│       │",
            "│       │",
            "└───────┘"
        };
        
        // Копируем шаблон
        string[] result = (string[])dieTemplate.Clone();
        
        // Добавляем точки в зависимости от значения
        switch (Value)
        {
            case 1:
                result[2] = "│   ●   │";
                break;
            case 2:
                result[1] = "│ ●     │";
                result[3] = "│     ● │";
                break;
            case 3:
                result[1] = "│ ●     │";
                result[2] = "│   ●   │";
                result[3] = "│     ● │";
                break;
            case 4:
                result[1] = "│ ●   ● │";
                result[3] = "│ ●   ● │";
                break;
            case 5:
                result[1] = "│ ●   ● │";
                result[2] = "│   ●   │";
                result[3] = "│ ●   ● │";
                break;
            case 6:
                result[1] = "│ ● ● ● │";
                result[2] = "│       │";
                result[3] = "│ ● ● ● │";
                break;
        }
        
        return result;
    }

    // Упрощенное 2D представление кости
    public string[] Get2DAsciiRepresentation()
    {
        string[] dieTemplate = new string[]
        {
            "┌───────┐",
            "│       │",
            "│       │",
            "│       │",
            "│       │",
            "│       │",
            "└───────┘"
        };
        
        string[] result = (string[])dieTemplate.Clone();
        
        switch (Value)
        {
            case 1:
                result[3] = "│   ●   │";
                break;
            case 2:
                result[2] = "│ ●     │";
                result[4] = "│     ● │";
                break;
            case 3:
                result[2] = "│ ●     │";
                result[3] = "│   ●   │";
                result[4] = "│     ● │";
                break;
            case 4:
                result[2] = "│ ●   ● │";
                result[4] = "│ ●   ● │";
                break;
            case 5:
                result[2] = "│ ●   ● │";
                result[3] = "│   ●   │";
                result[4] = "│ ●   ● │";
                break;
            case 6:
                result[2] = "│ ●   ● │";
                result[3] = "│ ●   ● │";
                result[4] = "│ ●   ● │";
                break;
        }
        
        return result;
    }
}

public class Combination
{
    public string Name { get; }
    public int Points { get; }
    public List<int> DiceValues { get; }

    public Combination(string name, int points, List<int> diceValues)
    {
        Name = name;
        Points = points;
        DiceValues = diceValues;
    }

    public override string ToString()
    {
        return $"{Name}: {string.Join(", ", DiceValues)} ({Points} очков)";
    }
}

public class AsciiArt
{
    // Проверка поддержки звука
    private static bool IsSoundSupported = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    
    // Метод для безопасного воспроизведения звука (только если платформа поддерживает)
    private static void SafeBeep(int frequency, int duration)
    {
        if (IsSoundSupported)
        {
            try
            {
#pragma warning disable CA1416 // Отключаем предупреждение о платформенной совместимости
                Console.Beep(frequency, duration);
#pragma warning restore CA1416
            }
            catch
            {
                // Игнорируем любые ошибки со звуком
            }
        }
    }

    public static string GetTitle()
    {
        return @"
 ██████  ██████  ███    ██ ██   ██     ██████   █████  ███    ███ ███████ 
██       ██   ██ ████   ██ ██  ██      ██      ██   ██ ████  ████ ██      
 █████   ██████  ██ ██  ██ █████       ██  ███ ███████ ██ ████ ██ █████   
     ██  ██   ██ ██  ██ ██ ██  ██      ██   ██ ██   ██ ██  ██  ██ ██      
██████   ██   ██ ██   ████ ██   ██      ██████ ██   ██ ██      ██ ███████
 ";
    }
    
    public static string GetGameTable()
    {
        return @"
    ╔═════════════════════════════════════════════════════════════════════════╗
    ║                             ИГРОВОЙ СТОЛ                                 ║ 
    ╠═════════════════════════════════════════════════════════════════════════╣
    ║                                                                         ║
    ║                            КОМПЬЮТЕР                                    ║
    ║                         ╭─────────────╮                                 ║
    ║                         │ o           │                                 ║
    ║                         │      _      │                                 ║
    ║                         │     |_|     │                                 ║
    ║                         │    /   \    │                                 ║
    ║                         │    \ _ /    │                                 ║
    ║                         │             │                                 ║
    ║                         ╰─────────────╯                                 ║
    ║                                                                         ║
    ║                   ╭─────────────────────────╮                           ║
    ║                  /                           \                          ║
    ║                 /                             \                         ║
    ║                /                               \                        ║
    ║               /                                 \                       ║
    ║              /                                   \                      ║
    ║             ╰───────────────────────────────────╯                      ║
    ║                                                                         ║
    ║                                                                         ║
    ║                                                                         ║
    ║                              ИГРОК                                      ║
    ║                        ╭──────────────╮                                 ║
    ║                        │    _    _    │                                 ║
    ║                        │   (o)  (o)   │                                 ║
    ║                        │      \/      │                                 ║
    ║                        │     ____     │                                 ║
    ║                        │    /    \    │                                 ║
    ║                        ╰──────────────╯                                 ║
    ║                                                                         ║
    ╚═════════════════════════════════════════════════════════════════════════╝
";
    }
    
    public static void DrawScoreboard(int playerScore, int computerScore, int targetScore)
    {
        Console.WriteLine("╔════════════════════════╦════════════════════════╗");
        Console.WriteLine("║        ИГРОК          ║       КОМПЬЮТЕР        ║");
        Console.WriteLine("╠════════════════════════╬════════════════════════╣");
        Console.WriteLine($"║  Очки: {playerScore.ToString().PadRight(13)} ║  Очки: {computerScore.ToString().PadRight(13)} ║");
        Console.WriteLine("╚════════════════════════╩════════════════════════╝");
        Console.WriteLine($"          Цель: набрать {targetScore} очков             ");
    }
    
    public static void DrawZonk()
    {
        // Улучшенная анимация зонка
        string[] zonkFrames = {
            @"
  ███████╗ ██████╗ ███╗   ██╗██╗  ██╗██╗
  ╚══███╔╝██╔═══██╗████╗  ██║██║ ██╔╝██║
    ███╔╝ ██║   ██║██╔██╗ ██║█████╔╝ ██║
   ███╔╝  ██║   ██║██║╚██╗██║██╔═██╗ ╚═╝
  ███████╗╚██████╔╝██║ ╚████║██║  ██╗██╗
  ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝
",
            @"
  ██████╗ ██████╗ ███╗   ██╗██╗  ██╗██╗
  ╚═███╔╝██╔═══██╗████╗  ██║██║ ██╔╝██║
    ██╔╝ ██║   ██║██╔██╗ ██║█████╔╝ ██║
    ██╔╝  ██║   ██║██║╚██╗██║██╔═██╗   
  ██████╗╚██████╔╝██║ ╚████║██║  ██╗██╗
  ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚═╝  ╚═╝╚═╝
"
        };
        
        Console.ForegroundColor = ConsoleColor.Red;
        
        // Звуковой эффект (безопасный)
        SafeBeep(300, 200);
        SafeBeep(250, 200);
        SafeBeep(200, 300);
        
        // Анимация мигания с эффектом дрожания
        for (int i = 0; i < 6; i++)
        {
            Console.Clear();
            
            // Случайное смещение для эффекта дрожания
            int offsetX = i % 2 == 0 ? 2 : 0;
            int offsetY = i % 3 == 0 ? 1 : 0;
            
            for (int j = 0; j < offsetY; j++)
                Console.WriteLine();
                
            string prefix = new string(' ', offsetX);
            string[] lines = zonkFrames[i % 2].Split('\n');
            
            foreach (var line in lines)
                Console.WriteLine(prefix + line);
                
            Thread.Sleep(150);
        }
        
        Console.WriteLine(zonkFrames[0]);
        Console.ResetColor();
    }
    
    public static string[] GetHand()
    {
        string[] hand = {
            @"                                                  ",
            @"                                                  ",
            @"                                                  ",
            @"                                                  ",
            @"                                                  ",
            @"                                                  ",
            @"                                ╭──────╮          ",
            @"                            ╭───╯      ╰───╮     ",
            @"                            │              │     ",
            @"                     ╭──────┴───────┬──────╯     ",
            @"                     │              │            ",
            @"                     │              │            ",
            @"                     │              │            ",
            @"                  ╭──┴──╮        ╭──┴──╮         ",
            @"                  │     │        │     │         ",
            @"                  │     │        │     │         ",
            @"                  │     │        │     │         ",
            @"               ╭──┴──╮  │     ╭──┴──╮  │         ",
            @"               │     │  │     │     │  │         ",
            @"               │     │  │     │     │  │         ",
            @"               │     │  │     │     │  │         "
        };
        
        return hand;
    }
    
    public static string[] GetShakingHand(int frame)
    {
        string[] hand = GetHand();
        string[] shakingHand = new string[hand.Length];
        
        for (int i = 0; i < hand.Length; i++)
        {
            StringBuilder sb = new StringBuilder(hand[i]);
            if (frame % 2 == 0)
            {
                sb.Append("  ");
            }
            else
            {
                sb.Insert(0, "  ");
            }
            shakingHand[i] = sb.ToString();
        }
        
        return shakingHand;
    }
    
    public static string[] GetDiceRollingAnimation3D(int frame)
    {
        string[][] frames = new string[3][];
        
        frames[0] = new string[]
        {
            "      ╱╲      ",
            "    ╱    ╲    ",
            "  ╱        ╲  ",
            "╱____________╲",
            "│            │",
            "│     •      │",
            "│            │",
            "│____________│",
        };
        
        frames[1] = new string[]
        {
            "   ╱╲╱╲╱╲    ",
            " ╱        ╲  ",
            "╱          ╲ ",
            "│    ••     │",
            "│           │",
            "│    ••     │",
            "│___________│",
            "            "
        };
        
        frames[2] = new string[]
        {
            "      ┌┐     ",
            "     ╱  ╲    ",
            "    │ •• │   ",
            "    │    │   ",
            "    │ •• │   ",
            "    └────┘   ",
            "             ",
            "             "
        };
        
        return frames[frame % frames.Length];
    }
    
    public static void DrawFirstPersonView(bool isPlayerTurn, List<Die> dice, List<Die> setAsideDice)
    {
        Console.WriteLine("\n");
        // Выводим стол в перспективе
        string tableView = @"
    ╔═════════════════════════════════════════════════════════════════════════╗
    ║                          ВИД ОТ ПЕРВОГО ЛИЦА                            ║ 
    ╠═════════════════════════════════════════════════════════════════════════╣
    ║                                                                         ║
    ║               _________________________________________________         ║
    ║              /                                                 \        ║
    ║             /                                                   \       ║
    ║            /                                                     \      ║
    ║           /                                                       \     ║
    ║          /                                                         \    ║
    ║         /___________________________________________________________\   ║";
        
        Console.WriteLine(tableView);
        
        // Выводим кости на столе (если есть)
        if (dice.Count > 0)
        {
            Console.WriteLine("    ║                                                                         ║");
            Console.WriteLine("    ║                             АКТИВНЫЕ КОСТИ                              ║");
            
            // Выводим кости в перспективе (уменьшенные)
            int padding = (75 - dice.Count * 10) / 2;
            Console.Write("    ║" + new string(' ', padding));
            
            foreach (var die in dice)
            {
                Console.Write("[" + die.Value + "]     ");
            }
            
            Console.WriteLine(new string(' ', padding) + "║");
        }
        
        // Выводим отложенные кости (если есть)
        if (setAsideDice.Count > 0)
        {
            Console.WriteLine("    ║                                                                         ║");
            Console.WriteLine("    ║                           ОТЛОЖЕННЫЕ КОСТИ                              ║");
            
            int padding = (75 - setAsideDice.Count * 5) / 2;
            Console.Write("    ║" + new string(' ', padding));
            
            foreach (var die in setAsideDice)
            {
                Console.Write("[" + die.Value + "] ");
            }
            
            Console.WriteLine(new string(' ', padding) + "║");
        }
        
        // Нижняя часть стола
        Console.WriteLine("    ║                                                                         ║");
        Console.WriteLine("    ║                                                                         ║");
        
        // Рука в нижней части экрана
        DrawHand(isPlayerTurn);
        
        Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════════╝");
    }
    
    public static void DrawHand(bool isPlayerTurn)
    {
        string[] handLines = {
            "    ║                                                                         ║",
            "    ║                                                    ╭────╮              ║",
            "    ║                                                 ╭──┘    └──╮           ║",
            "    ║                                                 │          │           ║",
            "    ║                                          ╭──────┴───╮  ╭───┴────╮      ║",
            "    ║                                          │          │  │        │      ║",
            "    ║                                       ╭──┴──╮    ╭──┴──┴──╮    │      ║",
            "    ║                                       │     │    │       │    │       ║"
        };
        
        // Анимация руки в зависимости от того, чей ход
        if (isPlayerTurn)
        {
            foreach (var line in handLines)
            {
                Console.WriteLine(line);
            }
            }
            else
            {
            // Компьютерного игрока рука не показывается
            for (int i = 0; i < handLines.Length; i++)
            {
                Console.WriteLine("    ║                                                                         ║");
            }
        }
    }
    
    public static void AnimateRollingDice()
    {
        // Позиция, откуда начинается анимация руки с костями
        int handStartRow = Console.CursorTop;
        
        // Анимация руки, трясущей кости
        for (int frame = 0; frame < 10; frame++)
        {
            string[] hand = GetShakingHand(frame);
            int cursorPosition = Console.CursorTop;
            
            // Очищаем предыдущий кадр
            for (int i = 0; i < hand.Length; i++)
            {
                Console.SetCursorPosition(10, handStartRow + i);
                Console.Write(new string(' ', 60));
            }
            
            // Рисуем новый кадр
            for (int i = 0; i < hand.Length; i++)
            {
                Console.SetCursorPosition(10, handStartRow + i);
                
                if (frame < 6)
                {
                    // В руке есть кости
                    if (i == 8 && frame % 2 == 0)
                    {
                        Console.Write(hand[i] + "    [■]    [■]");
                    }
                    else if (i == 8)
                    {
                        Console.Write(hand[i] + "   [■]   [■]");
                    }
                    else
                    {
                        Console.Write(hand[i]);
                    }
                }
                else
                {
                    // Кости вылетают из руки и падают
                    Console.Write(hand[i]);
                    
                    if (i == 3 && frame == 6)
                    {
                        Console.SetCursorPosition(35, handStartRow + i - 2);
                        Console.Write("[■] [■]");
                    }
                    else if (i == 2 && frame == 7)
                    {
                        Console.SetCursorPosition(40, handStartRow + i);
                        Console.Write("[■]   [■]");
                    }
                    else if (i == 4 && frame == 8)
                    {
                        Console.SetCursorPosition(38, handStartRow + i);
                        Console.Write("[■]     [■]");
                    }
                    else if (i == 6 && frame == 9)
                    {
                        Console.SetCursorPosition(35, handStartRow + i + 1);
                        Console.Write("[■]        [■]");
                    }
                }
            }
            
            Thread.Sleep(150);
        }
        
        // После анимации руки показываем анимацию костей на столе
        int diceStartRow = handStartRow + 10;
        
        for (int frame = 0; frame < 8; frame++)
        {
            string[] diceAnimation = GetDiceRollingAnimation3D(frame);
            
            // Очищаем предыдущий кадр
            for (int i = 0; i < diceAnimation.Length; i++)
            {
                Console.SetCursorPosition(20, diceStartRow + i);
                Console.Write(new string(' ', 50));
            }
            
            // Рисуем несколько костей в разных позициях
            for (int dice = 0; dice < 6; dice++)
            {
                int xPos = 20 + (dice * 10) % 40;
                int yOffset = dice % 3;
                
                for (int i = 0; i < diceAnimation.Length; i++)
                {
                    if (diceStartRow + i + yOffset < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(xPos, diceStartRow + i + yOffset);
                        Console.Write(diceAnimation[i]);
                    }
                }
            }
            
            Thread.Sleep(120);
        }
        
        // Очищаем анимацию
        for (int i = 0; i < 20; i++)
        {
            Console.SetCursorPosition(0, handStartRow + i);
            Console.Write(new string(' ', 80));
        }
        
        // Возвращаем курсор в исходное положение
        Console.SetCursorPosition(0, handStartRow);
    }

    public static string[] GetDice2DRollingAnimation(int frame)
    {
        string[][] frames = new string[4][];
        
        frames[0] = new string[]
        {
            "┌───────┐",
            "│       │",
            "│ ●   ● │",
            "│   ●   │",
            "│ ●   ● │",
            "│       │",
            "└───────┘"
        };
        
        frames[1] = new string[]
        {
            "┌───────┐",
            "│ ●     │",
            "│       │",
            "│     ● │",
            "│       │",
            "│ ●     │",
            "└───────┘"
        };
        
        frames[2] = new string[]
        {
            "┌───────┐",
            "│ ● ● ● │",
            "│       │",
            "│       │",
            "│       │",
            "│ ● ● ● │",
            "└───────┘"
        };
        
        frames[3] = new string[]
        {
            "┌───────┐",
            "│ ●   ● │",
            "│       │",
            "│   ●   │",
            "│       │",
            "│ ●   ● │",
            "└───────┘"
        };
        
        return frames[frame % frames.Length];
    }
    
    public static void DrawGameTableWithPlayers(bool isPlayerTurn, List<Die> dice, List<Die> setAsideDice)
    {
        Console.WriteLine("\n");
        // Верхняя часть стола
        Console.WriteLine("    ╔═════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("    ║                             ИГРОВОЙ СТОЛ                                 ║");
        Console.WriteLine("    ╠═════════════════════════════════════════════════════════════════════════╣");

        // Отображаем оппонента (компьютер)
        DrawOpponent(!isPlayerTurn);
        
        // Середина стола
        Console.WriteLine("    ║                                                                         ║");
        Console.WriteLine("    ║                  ┌───────────────────────────────┐                      ║");
        
        // Область для костей на столе
        if (dice.Count > 0)
        {
            // Центрируем кости
            int padding = (55 - dice.Count * 10) / 2;
            Console.Write("    ║" + new string(' ', 10 + padding));
            
            // Выводим только значения для компактности
            foreach (var die in dice)
            {
                Console.Write("[" + die.Value + "]     ");
            }
            
            Console.WriteLine(new string(' ', 10 + padding) + "║");
        }
        else
        {
            Console.WriteLine("    ║                                                                         ║");
        }
        
        // Отображаем отложенные кости (если есть)
        if (setAsideDice.Count > 0)
        {
            Console.WriteLine("    ║               Отложенные:                                               ║");
            
            // Разделяем на несколько рядов если много костей
            for (int i = 0; i < setAsideDice.Count; i += 10)
            {
                int count = Math.Min(10, setAsideDice.Count - i);
                int padding = (55 - count * 5) / 2;
                Console.Write("    ║" + new string(' ', 10 + padding));
                
                for (int j = i; j < i + count; j++)
                {
                    Console.Write("[" + setAsideDice[j].Value + "] ");
                }
                
                Console.WriteLine(new string(' ', 10 + padding) + "║");
            }
        }
        
        Console.WriteLine("    ║                  └───────────────────────────────┘                      ║");
        Console.WriteLine("    ║                                                                         ║");
        
        // Отображаем игрока
        DrawPlayer(isPlayerTurn);
        
        // Нижняя часть стола
        Console.WriteLine("    ╚═════════════════════════════════════════════════════════════════════════╝");
    }
    
    // Отображение компьютера с подсветкой его хода
    private static void DrawOpponent(bool isActive)
    {
        if (isActive)
            Console.ForegroundColor = ConsoleColor.Yellow;
            
        Console.WriteLine("    ║                            КОМПЬЮТЕР                                    ║");
        Console.WriteLine("    ║                         ╭─────────────╮                                 ║");
        Console.WriteLine("    ║                         │ o         o │                                 ║");
        Console.WriteLine("    ║                         │             │                                 ║");
        Console.WriteLine("    ║                         │     ___     │                                 ║");
        Console.WriteLine("    ║                         │    /   \\    │                                 ║");
        Console.WriteLine("    ║                         │    \\___/    │" + (isActive ? " <-- Ходит" : "                ") + "    ║");
        Console.WriteLine("    ║                         ╰─────────────╯                                 ║");
        
        if (isActive)
            Console.ResetColor();
    }
    
    // Отображение игрока с подсветкой его хода
    private static void DrawPlayer(bool isActive)
    {
        if (isActive)
            Console.ForegroundColor = ConsoleColor.Yellow;
            
        Console.WriteLine("    ║                              ИГРОК                                      ║");
        Console.WriteLine("    ║                        ╭──────────────╮                                 ║");
        Console.WriteLine("    ║                        │    _    _    │                                 ║");
        Console.WriteLine("    ║                        │   (o)  (o)   │                                 ║");
        Console.WriteLine("    ║                        │      \\/      │                                 ║");
        Console.WriteLine("    ║                        │     ____     │" + (isActive ? " <-- Ходит" : "                ") + "    ║");
        Console.WriteLine("    ║                        ╰──────────────╯                                 ║");
        
        if (isActive)
            Console.ResetColor();
    }
    
    public static void AnimateRollingDiceOnTable(int diceCount)
    {
        int tableRow = Console.CursorTop;
        
        // Анимация костей на столе
        for (int frame = 0; frame < 10; frame++)
        {
            // Очищаем предыдущий кадр
            Console.SetCursorPosition(0, tableRow);
            Console.WriteLine(new string(' ', 80));
            Console.WriteLine(new string(' ', 80));
            Console.WriteLine(new string(' ', 80));
            Console.WriteLine(new string(' ', 80));
            Console.WriteLine(new string(' ', 80));
            Console.WriteLine(new string(' ', 80));
            Console.WriteLine(new string(' ', 80));
            
            Console.SetCursorPosition(0, tableRow);
            
            // Случайно расположенные кости
            for (int dice = 0; dice < diceCount; dice++)
            {
                string[] diceFrame = GetDice2DRollingAnimation((frame + dice) % 4);
                
                // Случайная позиция на столе
                int posX = 20 + ((dice * 7) % 40);
                int posY = tableRow + (dice % 3);
                
                // Рисуем кость
                for (int i = 0; i < diceFrame.Length; i++)
                {
                    Console.SetCursorPosition(posX, posY + i);
                    Console.Write(diceFrame[i]);
                }
                
                // Звуковой эффект для броска (безопасный)
                if (frame == 0 && dice == 0)
                    SafeBeep(800, 50);
                else if (frame == 3 && dice == 0)
                    SafeBeep(600, 50);
                else if (frame == 6 && dice == 0)
                    SafeBeep(700, 50);
            }
            
            Thread.Sleep(100);
        }
        
        // Очищаем анимацию
        Console.SetCursorPosition(0, tableRow);
        for (int i = 0; i < 8; i++)
        {
            Console.WriteLine(new string(' ', 80));
        }
        
        // Возвращаем курсор
        Console.SetCursorPosition(0, tableRow);
    }
    
    // Улучшенная анимация "Все кости отложены"
    public static void AnimateAllDiceSetAside()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        
        // Звуковой эффект успеха (безопасный)
        SafeBeep(800, 100);
        SafeBeep(1000, 100);
        SafeBeep(1200, 150);
        
        string[] animationFrames = {
            @"
     ╔═══════════════════════════════════════════════════╗
     ║             ВСЕ КОСТИ ОТЛОЖЕНЫ!                   ║
     ╚═══════════════════════════════════════════════════╝
",
            @"
    *╔═══════════════════════════════════════════════════╗*
    *║>>           ВСЕ КОСТИ ОТЛОЖЕНЫ!           <<     ║*
    *╚═══════════════════════════════════════════════════╝*
"
        };
        
        for (int i = 0; i < 6; i++)
        {
            Console.WriteLine(animationFrames[i % 2]);
            Thread.Sleep(200);
            
            // Стираем предыдущий кадр
            for (int j = 0; j < 3; j++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', 80));
            }
        }
        
        Console.WriteLine(animationFrames[0]);
        Console.ResetColor();
    }

    // Новый метод для отображения активного счета в верхней части экрана
    public static void DrawActiveScore(int playerScore, int turnScore, int computerScore)
    {
        // Очищаем весь экран
        Console.Clear();
        
        // Заголовок с активным счетом
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"╔═════════════════════════════════════════════════════════════════════════════╗");
        
        // Форматируем строку счета с центрированием
        string scoreInfo = $"ИГРОК: {playerScore} очков | ХОД: {turnScore} очков | КОМПЬЮТЕР: {computerScore} очков";
        int padding = (Console.WindowWidth - 2 - scoreInfo.Length) / 2;
        padding = Math.Max(0, padding);
        
        Console.Write("║");
        Console.Write(new string(' ', padding));
        Console.Write(scoreInfo);
        Console.Write(new string(' ', Console.WindowWidth - 2 - padding - scoreInfo.Length));
        Console.WriteLine("║");
        
        Console.WriteLine($"╚═════════════════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        
        // Добавляем пустую строку для отделения счета от содержимого
        Console.WriteLine();
    }

    // Метод для анимации броска костей с сохранением счета
    public static void AnimatePlayerRollingDice(int diceCount, int playerScore, int turnScore, int computerScore)
    {
        Console.ForegroundColor = ConsoleColor.White;
        
        // Анимация падающих костей
        for (int frame = 0; frame < 15; frame++)
        {
            Console.Clear();
            
            // Сначала отображаем счет
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"╔═════════════════════════════════════════════════════════════════════════════╗");
            
            string scoreInfo = $"ИГРОК: {playerScore} очков | ХОД: {turnScore} очков | КОМПЬЮТЕР: {computerScore} очков";
            int padding = (Console.WindowWidth - 2 - scoreInfo.Length) / 2;
            padding = Math.Max(0, padding);
            
            Console.Write("║");
            Console.Write(new string(' ', padding));
            Console.Write(scoreInfo);
            Console.Write(new string(' ', Console.WindowWidth - 2 - padding - scoreInfo.Length));
            Console.WriteLine("║");
            
            Console.WriteLine($"╚═════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            
            // Меняем цвет на белый для костей
            Console.ForegroundColor = ConsoleColor.White;
            
            // Заголовок
            Console.WriteLine("   КОСТИ БРОШЕНЫ!\n");
            
            // Для каждой кости создаем свою траекторию падения
            for (int dice = 0; dice < diceCount; dice++)
            {
                string[] diceFrame = GetDice2DRollingAnimation((frame + dice) % 4);
                
                // Вычисляем позицию падения для каждой кости
                // Кости падают сверху вниз, с учетом отступа для счета
                int posX = 10 + (dice * 8) % 40;
                int posY = Math.Min(frame + (dice % 3), 15) + 5; // +5 для учета строк счета
                
                // Рисуем кость
                if (posY >= 5 && posY < Console.WindowHeight - diceFrame.Length)
                {
                    for (int i = 0; i < diceFrame.Length; i++)
                    {
                        Console.SetCursorPosition(posX, posY + i);
                        Console.Write(diceFrame[i]);
                    }
                }
                
                // Звуковой эффект для броска (безопасный)
                if (frame == 0 && dice == 0)
                    SafeBeep(800, 50);
            }
            
            Thread.Sleep(60);
        }
        
        Console.ResetColor();
        Console.Clear();
        
        // После анимации восстанавливаем табло счета
        DrawActiveScore(playerScore, turnScore, computerScore);
    }
}

public class ZonkGame
{
    private List<Die> dice;
    private List<Die> setAsideDice;
    private List<Combination> currentTurnCombinations;
    private int playerScore;
    private int computerScore;
    private const int WINNING_SCORE = 5000;

    public ZonkGame()
    {
        dice = new List<Die>();
        setAsideDice = new List<Die>();
        currentTurnCombinations = new List<Combination>();
        playerScore = 0;
        computerScore = 0;

        for (int i = 0; i < 6; i++)
        {
            dice.Add(new Die());
        }
    }

    public void Play()
    {
        Console.Clear();
        Console.WriteLine(AsciiArt.GetTitle());
        Console.WriteLine("Добро пожаловать в игру 'Зонк'!");
        Console.WriteLine("Цель игры: набрать 5000 очков раньше соперника.\n");
        
        Console.WriteLine("Правила начисления очков:");
        Console.WriteLine(" • За единицу начисляется 100 очков");
        Console.WriteLine(" • За пятерку начисляется 50 очков");
        Console.WriteLine(" • За три единицы начисляется 1000 очков");
        Console.WriteLine(" • За три одинаковых числа начисляется 100 * номинал очков");
        Console.WriteLine(" • За каждую дополнительную кость очки удваиваются");
        Console.WriteLine(" • За комбинацию 1-2-3-4-5-6 начисляется 1500 очков");
        Console.WriteLine(" • За комбинацию 2-3-4-5-6 (без 1) начисляется 750 очков");
        Console.WriteLine(" • За комбинацию 1-2-3-4-5 (без 6) начисляется 500 очков");
        
        Console.WriteLine("\nНажмите любую клавишу, чтобы начать игру...");
        Console.ReadKey();

        while (playerScore < WINNING_SCORE && computerScore < WINNING_SCORE)
        {
            Console.Clear();
            
            // Показываем активный счет вместо обычного табло
            AsciiArt.DrawActiveScore(playerScore, 0, computerScore);
            
            PlayerTurn();
            
            if (playerScore >= WINNING_SCORE)
                break;
            
            Console.Clear();
            
            // Показываем активный счет вместо обычного табло
            AsciiArt.DrawActiveScore(playerScore, 0, computerScore);
            
            ComputerTurn();
            
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        Console.Clear();
        Console.WriteLine(AsciiArt.GetTitle());
        
        if (playerScore >= WINNING_SCORE)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
  ╔════════════════════════════════════════════════════════╗
  ║                  ВЫ ПОБЕДИЛИ!                          ║
  ╚════════════════════════════════════════════════════════╝
");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
  ╔════════════════════════════════════════════════════════╗
  ║               КОМПЬЮТЕР ПОБЕДИЛ!                       ║
  ╚════════════════════════════════════════════════════════╝
");
            Console.ResetColor();
        }
        
        Console.WriteLine($"\nИтоговый счёт: Вы - {playerScore}, Компьютер - {computerScore}");
    }

    private void PlayerTurn()
    {
        // Очищаем экран перед началом хода игрока
        Console.Clear();
        
        // Сбрасываем все кости перед началом хода
        RestartDice();
        
        // Анимация начала хода игрока
        AnimatePlayerTurnStart();
        
        int turnScore = 0;
        bool turnContinues = true;
        
        while (turnContinues)
        {
            // Отображаем активный счет
            AsciiArt.DrawActiveScore(playerScore, turnScore, computerScore);
            
            // Бросаем кости
            Console.WriteLine($"\nБросаем {GetDiceCountWord(dice.Count)}...");
            
            RollDice();
            
            // Отображаем активный счет снова после броска
            AsciiArt.DrawActiveScore(playerScore, turnScore, computerScore);
            
            // Отображаем кубики после обновления счета (важно!)
            DisplayDice();
            
            // Получаем возможные комбинации
            List<Combination> possibleCombinations = GetPossibleCombinations();
            
            // Если нет комбинаций - ЗОНК!
            if (possibleCombinations.Count == 0)
            {
                AsciiArt.DrawZonk();
                Console.WriteLine("У вас ЗОНК! Вы теряете очки за этот ход.");
                turnScore = 0;
                
                // Обновляем счет после ЗОНК
                AsciiArt.DrawActiveScore(playerScore, turnScore, computerScore);
                
                Thread.Sleep(2000);
                break;
            }
            
            // Выводим возможные комбинации
            DisplayPossibleCombinations(possibleCombinations);
            
            // Игрок выбирает комбинации
            if (!ChooseCombinations(possibleCombinations, ref turnScore))
            {
                break;
            }
            
            // Обновляем счет после выбора комбинаций
            AsciiArt.DrawActiveScore(playerScore, turnScore, computerScore);
            
            // Отображаем кубики снова после выбора комбинаций
            DisplayDice();
            
            // Проверяем, остались ли кости
            if (dice.Count == 0)
            {
                // Все кости отложены
                AnimateAllDiceSetAside();
                Console.WriteLine("Все кости отложены! Вы получаете право бросить все 6 костей снова.");
                RestartDice();
                
                // Анимация продолжения хода
                AnimatePlayerTurnContinue();
                
                // Обновляем счет после продолжения хода
                AsciiArt.DrawActiveScore(playerScore, turnScore, computerScore);
            }
            
            // Запрашиваем у игрока, хочет ли он продолжить
            Console.WriteLine($"\nУ вас {turnScore} очков за этот ход и {dice.Count} костей остается.");
            Console.WriteLine("Хотите бросить кости еще раз? (да/нет)");
            
            string answer = Console.ReadLine().ToLower();
            if (answer != "да" && answer != "д" && answer != "yes" && answer != "y")
            {
                turnContinues = false;
                
                // Анимация завершения хода
                AnimatePlayerTurnEnd(turnScore);
            }
            else
            {
                // Анимация продолжения хода
                AnimatePlayerTurnContinue();
            }
        }
        
        playerScore += turnScore;
        
        // Отображаем финальный счет после завершения хода
        AsciiArt.DrawActiveScore(playerScore, 0, computerScore);
        
        Console.WriteLine($"Конец вашего хода. Вы заработали {turnScore} очков.");
        Thread.Sleep(2000);
    }

    private void ComputerTurn()
    {
        Console.WriteLine("\n--- ХОД КОМПЬЮТЕРА ---");
        Thread.Sleep(1000);
        
        // Сбрасываем все кости перед началом хода компьютера
        RestartDice();
        
        int turnScore = 0;
        bool turnContinues = true;
        
        // Отображаем активный счет
        AsciiArt.DrawActiveScore(playerScore, 0, computerScore);

        while (turnContinues)
        {
            RollDice();
            
            // Обновляем счет после броска компьютера
            AsciiArt.DrawActiveScore(playerScore, 0, computerScore + turnScore);
            
            // Отображаем кубики после обновления счета (важно!)
            Console.WriteLine("Компьютер бросает кости:");
            DisplayDice();
            
            Thread.Sleep(1000);

            var possibleCombinations = GetPossibleCombinations();
            if (possibleCombinations.Count == 0)
            {
                AsciiArt.DrawZonk(); // Анимированный Зонк
                Console.WriteLine("Зонк! Компьютер теряет все очки за этот ход.");
                turnScore = 0;
                
                // Обновляем счет после ЗОНК
                AsciiArt.DrawActiveScore(playerScore, 0, computerScore);
                
                break;
            }

            // Простая стратегия ИИ
            var bestCombination = possibleCombinations.OrderByDescending(c => c.Points).First();
            turnScore += bestCombination.Points;
            Console.WriteLine($"Компьютер выбирает: {bestCombination}");
            
            // Обновляем счет после выбора компьютера
            AsciiArt.DrawActiveScore(playerScore, 0, computerScore + turnScore);
            
            foreach (var value in bestCombination.DiceValues)
            {
                var dieToRemove = dice.FirstOrDefault(d => d.Value == value);
                if (dieToRemove != null)
                {
                    dice.Remove(dieToRemove);
                    setAsideDice.Add(dieToRemove);
                }
            }
            
            // Отображаем кубики после выбора компьютера
            DisplayDice();
            
            Thread.Sleep(1000);
            
            // Решение ИИ продолжать или нет
            if (turnScore >= 350 && dice.Count <= 2)
            {
                turnContinues = false;
                Console.WriteLine("Компьютер решает закончить ход.");
            }
            else if (turnScore >= 1000)
            {
                // Если набрано много очков, может завершить ход с вероятностью 50%
                turnContinues = new Random().Next(100) >= 50;
                if (!turnContinues)
                    Console.WriteLine("Компьютер решает закончить ход.");
            }
            
            if (dice.Count == 0 && turnContinues)
            {
                // Анимация "Все кости отложены"
                AnimateAllDiceSetAside();
                Console.WriteLine("Все кости отложены! Компьютер получает право бросить все 6 костей снова.");
                RestartDice();
                
                // Обновляем счет после продолжения хода
                AsciiArt.DrawActiveScore(playerScore, 0, computerScore + turnScore);
            }
        }

        computerScore += turnScore;
        
        // Отображаем финальный счет после завершения хода компьютера
        AsciiArt.DrawActiveScore(playerScore, 0, computerScore);
        
        Console.WriteLine($"Конец хода компьютера. Компьютер заработал {turnScore} очков.");
    }

    private void RollDice()
    {
        foreach (var die in dice)
        {
            die.Roll();
        }
        
        // Исправляем параметры метода - передаем 0 вместо несуществующей переменной turnScore
        AsciiArt.AnimatePlayerRollingDice(dice.Count, playerScore, 0, computerScore);
    }

    private string GetDiceCountWord(int count)
    {
        if (count == 1)
            return "кость";
        else if (count >= 2 && count <= 4)
            return "кости";
        else
            return "костей";
    }

    private void DisplayDice()
    {
        if (dice.Count > 0)
        {
            Console.WriteLine("\nВыпавшие кости:");
            
            // Используем 2D-представление для костей
            var diceRepresentation = dice.Select(d => d.Get2DAsciiRepresentation()).ToList();
            
            // Выводим кости в ряд по определенному количеству
            int maxDicePerRow = 5; // Увеличиваем для 2D-костей
            for (int dieIdx = 0; dieIdx < dice.Count; dieIdx += maxDicePerRow)
            {
                int diceInCurrentRow = Math.Min(maxDicePerRow, dice.Count - dieIdx);
                
                // Для каждой строки ASCII-кости
                for (int lineIdx = 0; lineIdx < diceRepresentation[0].Length; lineIdx++)
                {
                    // Собираем строку из всех костей в текущем ряду
                    for (int i = 0; i < diceInCurrentRow; i++)
                    {
                        int currentDieIdx = dieIdx + i;
                        Console.Write(diceRepresentation[currentDieIdx][lineIdx] + " ");
                    }
                    Console.WriteLine();
                }
                
                // Добавляем индексы под костями
                for (int i = 0; i < diceInCurrentRow; i++)
                {
                    int currentDieIdx = dieIdx + i;
                    Console.Write($"   [{currentDieIdx + 1}]    ");
                }
                Console.WriteLine("\n");
            }
        }
        
        if (setAsideDice.Count > 0)
        {
            Console.WriteLine("\nОтложенные кости:");
            
            // Отложенные кости выводим в меньшем размере
            var smallAsciiDice = setAsideDice.Select(d => d.GetSmallAsciiRepresentation()).ToList();
            int maxSmallDicePerRow = 8; // Больше костей в ряду
            
            for (int dieIdx = 0; dieIdx < setAsideDice.Count; dieIdx += maxSmallDicePerRow)
            {
                int diceInCurrentRow = Math.Min(maxSmallDicePerRow, setAsideDice.Count - dieIdx);
                
                for (int lineIdx = 0; lineIdx < smallAsciiDice[0].Length; lineIdx++)
                {
                    for (int i = 0; i < diceInCurrentRow; i++)
                    {
                        Console.Write(smallAsciiDice[dieIdx + i][lineIdx] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }

    private void RestartDice()
    {
        // Очищаем список отложенных костей
        setAsideDice.Clear();
        
        // Очищаем список активных костей
        dice.Clear();
        
        // Создаем заново все 6 костей
        for (int i = 0; i < 6; i++)
        {
            dice.Add(new Die());
        }
    }

    private List<Combination> GetPossibleCombinations()
    {
        var result = new List<Combination>();
        var diceValues = dice.Select(d => d.Value).ToList();
        var countByValue = new Dictionary<int, int>();
        
        for (int i = 1; i <= 6; i++)
        {
            countByValue[i] = diceValues.Count(v => v == i);
        }

        // Проверяем 1-2-3-4-5-6 (стрейт)
        if (countByValue.All(kv => kv.Value >= 1))
        {
            var comboValues = new List<int>();
            for (int i = 1; i <= 6; i++)
                comboValues.Add(i);
            result.Add(new Combination("Стрейт", 1500, comboValues));
        }
        // Проверяем 2-3-4-5-6 (без 1)
        else if (countByValue[2] >= 1 && countByValue[3] >= 1 && countByValue[4] >= 1 && 
                 countByValue[5] >= 1 && countByValue[6] >= 1)
        {
            var comboValues = new List<int> { 2, 3, 4, 5, 6 };
            result.Add(new Combination("Малый стрейт (без 1)", 750, comboValues));
        }
        // Проверяем 1-2-3-4-5 (без 6)
        else if (countByValue[1] >= 1 && countByValue[2] >= 1 && countByValue[3] >= 1 && 
                 countByValue[4] >= 1 && countByValue[5] >= 1)
        {
            var comboValues = new List<int> { 1, 2, 3, 4, 5 };
            result.Add(new Combination("Малый стрейт (без 6)", 500, comboValues));
        }

        // Проверяем комбинации из 6 одинаковых
        foreach (var kv in countByValue)
        {
            if (kv.Value == 6)
            {
                var value = kv.Key;
                var points = (value == 1) ? 8000 : value * 100 * 8;
                var comboValues = Enumerable.Repeat(value, 6).ToList();
                result.Add(new Combination($"Шесть {value}", points, comboValues));
            }
        }
        
        // Проверяем комбинации из 5 одинаковых
        foreach (var kv in countByValue)
        {
            if (kv.Value == 5)
            {
                var value = kv.Key;
                var points = (value == 1) ? 4000 : value * 100 * 4;
                var comboValues = Enumerable.Repeat(value, 5).ToList();
                result.Add(new Combination($"Пять {value}", points, comboValues));
            }
        }
        
        // Проверяем комбинации из 4 одинаковых
        foreach (var kv in countByValue)
        {
            if (kv.Value == 4)
            {
                var value = kv.Key;
                var points = (value == 1) ? 2000 : value * 100 * 2;
                var comboValues = Enumerable.Repeat(value, 4).ToList();
                result.Add(new Combination($"Четыре {value}", points, comboValues));
            }
        }
        
        // Проверяем комбинации из 3 одинаковых
        foreach (var kv in countByValue)
        {
            if (kv.Value >= 3)
            {
                var value = kv.Key;
                var points = (value == 1) ? 1000 : value * 100;
                var comboValues = Enumerable.Repeat(value, 3).ToList();
                result.Add(new Combination($"Три {value}", points, comboValues));
            }
        }

        // Отдельные единицы
        if (countByValue[1] > 0 && countByValue[1] < 3)
        {
            for (int i = 0; i < countByValue[1]; i++)
            {
                result.Add(new Combination("Единица", 100, new List<int> { 1 }));
            }
        }
        
        // Отдельные пятерки
        if (countByValue[5] > 0 && countByValue[5] < 3)
        {
            for (int i = 0; i < countByValue[5]; i++)
            {
                result.Add(new Combination("Пятерка", 50, new List<int> { 5 }));
            }
        }

        return result;
    }

    private void DisplayPossibleCombinations(List<Combination> combinations)
    {
        Console.WriteLine("\nДоступные комбинации:");
        
        for (int i = 0; i < combinations.Count; i++)
        {
            // Добавляем проверку на null
            var combination = combinations[i];
            if (combination != null)
            {
                Console.WriteLine($"{i + 1}. {combination}");
            }
        }
    }

    private bool ChooseCombinations(List<Combination> possibleCombinations, ref int turnScore)
    {
        int currentTurnScore = 0;
        
        while (true)
        {
            Console.WriteLine($"\n╔════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  Очки за текущий ход: {(turnScore + currentTurnScore).ToString().PadRight(28)} ║");
            Console.WriteLine($"╚════════════════════════════════════════════════════════╝");
            
            Console.WriteLine("\nВыберите одну из комбинаций, введя её номер, или 'п' чтобы передать ход:");
            var input = Console.ReadLine()?.Trim().ToLower();
            
            if (input == "п")
            {
                turnScore += currentTurnScore;
                return false;
            }
            
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= possibleCombinations.Count)
            {
                var selectedCombo = possibleCombinations[choice - 1];
                currentTurnScore += selectedCombo.Points;
                currentTurnCombinations.Add(selectedCombo);
                
                // Удаляем кости из игры
                foreach (var value in selectedCombo.DiceValues)
                {
                    var dieToRemove = dice.FirstOrDefault(d => d.Value == value);
                    if (dieToRemove != null)
                    {
                        dice.Remove(dieToRemove);
                        setAsideDice.Add(dieToRemove);
                    }
                }
                
                Console.WriteLine($"Вы выбрали: {selectedCombo}");
                
                if (dice.Count == 0)
                {
                    Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║             ВСЕ КОСТИ ОТЛОЖЕНЫ!                        ║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════╝");
                    turnScore += currentTurnScore;
                    
                    Console.WriteLine("Хотите бросить все 6 костей снова? (д/н)");
                    var rollAgain = Console.ReadLine()?.Trim().ToLower();
                    return rollAgain == "д";
                }
                
                Console.WriteLine("Хотите бросить оставшиеся кости? (д/н)");
                var rollOrNot = Console.ReadLine()?.Trim().ToLower();
                
                if (rollOrNot == "д")
                {
                    turnScore += currentTurnScore;
                    return true;
                }
                else
                {
                    turnScore += currentTurnScore;
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод. Пожалуйста, попробуйте снова.");
            }
        }
    }

    private void AnimateAllDiceSetAside()
    {
        AsciiArt.AnimateAllDiceSetAside();
    }

    // Новая анимация для начала хода игрока
    private void AnimatePlayerTurnStart()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        string[] frames = {
            @"
       ╔═══════════════════════════════════════════╗
       ║                                           ║
       ║              ВАШ ХОД                      ║
       ║                                           ║
       ╚═══════════════════════════════════════════╝
",
            @"
      ⊛╔═══════════════════════════════════════════╗⊛
      ⊛║               ВАШ ХОД                     ║⊛
      ⊛║                                           ║⊛
      ⊛║          ПРИГОТОВЬТЕСЬ!                   ║⊛
      ⊛╚═══════════════════════════════════════════╝⊛
",
            @"
     ⊛⊛╔═══════════════════════════════════════════╗⊛⊛
     ⊛⊛║             >>>ВАШ ХОД<<<                 ║⊛⊛
     ⊛⊛║                                           ║⊛⊛
     ⊛⊛║            ПРИГОТОВЬТЕСЬ!                 ║⊛⊛
     ⊛⊛╚═══════════════════════════════════════════╝⊛⊛
"
        };
        
        for (int i = 0; i < 6; i++)
        {
            Console.Clear();
            Console.WriteLine(frames[i % 3]);
            Thread.Sleep(150);
        }
        
        // Финальный эффект
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"
     ╔═══════════════════════════════════════════════════╗
     ║                                                   ║
     ║                   ВАШ ХОД!                        ║
     ║                                                   ║
     ╚═══════════════════════════════════════════════════╝
");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
    }
    
    // Анимация продолжения хода
    private void AnimatePlayerTurnContinue()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Clear();
        
        string[] dice = {
            @"  ┌───┐ ",
            @"  │ • │ ",
            @"  └───┘ "
        };
        
        // Анимация костей, перемещающихся через экран
        for (int pos = -10; pos < Console.WindowWidth; pos += 2)
        {
            Console.Clear();
            
            Console.SetCursorPosition(Math.Max(0, pos), 5);
            if (pos >= 0) Console.Write(dice[0]);
            
            Console.SetCursorPosition(Math.Max(0, pos), 6);
            if (pos >= 0) Console.Write(dice[1]);
            
            Console.SetCursorPosition(Math.Max(0, pos), 7);
            if (pos >= 0) Console.Write(dice[2]);
            
            Console.SetCursorPosition(Math.Max(0, pos - 15), 3);
            if (pos - 15 >= 0) Console.Write(dice[0]);
            
            Console.SetCursorPosition(Math.Max(0, pos - 15), 4);
            if (pos - 15 >= 0) Console.Write(dice[1]);
            
            Console.SetCursorPosition(Math.Max(0, pos - 15), 5);
            if (pos - 15 >= 0) Console.Write(dice[2]);
            
            // Сообщение по центру
            if (pos > 20)
            {
                int centerX = Console.WindowWidth / 2 - 15;
                int centerY = Console.WindowHeight / 2;
                
                Console.SetCursorPosition(centerX, centerY);
                Console.WriteLine("ПРОДОЛЖАЕМ БРОСАТЬ КОСТИ!");
            }
            
            Thread.Sleep(10);
        }
        
        Console.ResetColor();
        Console.Clear();
    }
    
    // Анимация завершения хода игрока
    private void AnimatePlayerTurnEnd(int score)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        string scoreText = score.ToString();
        string message = $"ХОД ЗАВЕРШЕН! ЗАРАБОТАНО {score} ОЧКОВ";
        int width = message.Length + 10;
        
        string topBorder = "╔" + new string('═', width) + "╗";
        string bottomBorder = "╚" + new string('═', width) + "╝";
        string emptyLine = "║" + new string(' ', width) + "║";
        
        for (int i = 0; i < 5; i++)
        {
            Console.Clear();
            
            // Звездочки вокруг рамки для анимации
            string decoration = (i % 2 == 0) ? "★" : "☆";
            
            Console.WriteLine("\n\n");
            Console.WriteLine($"  {decoration} {topBorder} {decoration}");
            Console.WriteLine($"  {decoration} {emptyLine} {decoration}");
            
            // Центровка сообщения
            int padding = (width - message.Length) / 2;
            string paddedMessage = "║" + new string(' ', padding) + message + new string(' ', width - padding - message.Length) + "║";
            Console.WriteLine($"  {decoration} {paddedMessage} {decoration}");
            
            Console.WriteLine($"  {decoration} {emptyLine} {decoration}");
            Console.WriteLine($"  {decoration} {bottomBorder} {decoration}");
            
            Thread.Sleep(200);
        }
        
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
    }
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var game = new ZonkGame();
        game.Play();
    }
}