using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LinkedListLib;

namespace _7oop
{
    class Program
    {
        static Dictionary<string, LinkedList> userLists = new Dictionary<string, LinkedList>();
        static string? currentUser = null;
        static readonly string defaultListName = "default";

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleKeyInfo keyInfo;

            LinkedList defaultList = new LinkedList();
            userLists[defaultListName] = defaultList;
            currentUser = defaultListName;

            DisplayMenu();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"За замовчуванням створено список \"{defaultListName}\". Він використовується як активний.");
            Console.ResetColor();

            do
            {
                IFListIsChosen();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nВАШ ВИБІР ОПЦІЇ: ");
                Console.ResetColor();
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape) {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("ЗАВЕРШЕННЯ ПРОГРАМИ");
                    Console.ResetColor();
                    break;
                }

                SwitchOptions(keyInfo, userLists[currentUser]);

            } while (keyInfo.Key != ConsoleKey.Escape);

        }

        public static void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("ПРОГРАМА ПРАЦЮЄ З ОДНОСПРЯМОВАНИМИ СПИСКАМИ");
            Console.WriteLine("+-----+     +-----+     +-----+     +-----+");
            Console.WriteLine("| 1.0 | --> | 2.0 | --> | 3.0 | --> | N.N |");
            Console.WriteLine("+-----+     +-----+     +-----+     +-----+");
            Console.WriteLine("\nДОСТУПНІ ОПЦІЇ:");
            Console.WriteLine("0: додати числа до початку списку");
            Console.WriteLine("1: знайти перший елемент, більший за середнє значення");
            Console.WriteLine("2: знайти суму елементів, більших за задане значення");
            Console.WriteLine("3: отримати новий список з елементів, менших за середнє значення");
            Console.WriteLine("4: видалити елементи на парних позиціях у списку");
            Console.WriteLine("5: переглянути/змінити елемент за індексом");
            Console.WriteLine("6: переглянути активний список");
            Console.WriteLine("7: видалити елемент списку за індексом");
            Console.WriteLine("8: створити новий список");
            Console.WriteLine("9: обрати інший активний список");
            Console.WriteLine("ESC, щоб завершити");
            Console.ResetColor();
        }
    
        public static void SwitchOptions(ConsoleKeyInfo keyInfo, LinkedList anylist)
        {
            if (char.IsDigit(keyInfo.KeyChar))
            {
                int choice = keyInfo.KeyChar - '0';

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("0: Додати числа до списку");
                        Case0_EnterElements(keyInfo, anylist);
                        break;
                    case 1:
                        Console.WriteLine("1: Знайти перший елемент, більший за середнє значення.");
                        Case1_FindFirstGreaterThanAverage(anylist);
                        break;
                    case 2:
                        Console.WriteLine("2: Знайти суму елементів, більших за задане значення");
                        Case2_SumOfElemMoreTnGiven(anylist);
                        break;
                    case 3:
                        Console.WriteLine("3: Отримати новий список з елементів, менших за середнє значення");
                        Case3_BelowAverageList(anylist);
                        break;
                    case 4:
                        Console.WriteLine("4: Видалити елементи на парних позиціях у списку");
                        Case4_RemoveElementsAtEvenPositions(anylist);
                        break;
                    case 5:
                        Console.WriteLine("5: Переглянути елемент за індексом");
                        Case5_ViewOrEditElementByIndex(anylist);
                        break;
                    case 6:
                        Console.WriteLine("6: Переглянути активний список");
                        Case6_DisplayList(anylist);
                        break;
                    case 7:
                        Console.WriteLine("7: Видалити елемент списку за індексом");
                        Case7_DeleteElementByIndex(anylist);
                        break;
                    case 8:
                        Console.WriteLine("8: Створити новий список");
                        CreateNewList();
                        break;
                    case 9:
                        Console.WriteLine("9: (Пере)Обрати активний список");
                        SelectList();
                        break;

                    default:
                        Console.WriteLine("Опція не існує. Повторіть вибір.");
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ви ввели не цифру. Повторіть вибір.");
                Console.ResetColor();            
            }
        }

        public static void Case0_EnterElements(ConsoleKeyInfo keyInfo, LinkedList list)
        {
            Console.WriteLine("\nВведення елементів списку (введіть '-' для завершення ):");
            char inputTrigger = ' ';

            while (inputTrigger != '-')
            {
                Console.Write("Введіть число: ");
                string input = Console.ReadLine();

                if (input == "-")
                {
                    inputTrigger = '-';
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Завершення введення.");
                    Console.ResetColor();
                }
                else if (double.TryParse(input.Replace('.', ','), out double value))
                {
                    list.Add(value);
                }
                else if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("Продовжити введення? (+/-): ");
                    string response = Console.ReadLine();
                    if (response == "-")
                    {
                        inputTrigger = '-';
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Завершення введення.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Некоректний ввід. Спробуйте ще раз.");
                    Console.ResetColor();
                }
            } 
            Console.WriteLine("\nВаш список:");
            PrintList(list);
        }
        public static void Case1_FindFirstGreaterThanAverage(LinkedList list)
        {
            BlueMessageCurrentList();
            if (list.Count > 0)
            {
                try
                {
                    double average = list.GetAverage();
                    Console.WriteLine($"\nСереднє значення: {average:F2}");

                    try
                    {
                        double firstGreater = list.FindFirstGreaterThanAverage();
                        Console.WriteLine($"Перший елемент більший за середнє значення: {firstGreater}");
                    }
                    catch (InvalidOperationException ex)
                    {
                        RedMessageException(ex);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    RedMessageException(ex);
                }
            }
            else
            {
                RedMessageListIsEmpty();
            }
        }
        public static void Case2_SumOfElemMoreTnGiven(LinkedList list)
        {
            BlueMessageCurrentList();

            if (list.Count > 0)
            {
                try
                {
                    Console.Write("\nВведіть порогове значення для суми: ");
                    if (double.TryParse(Console.ReadLine().Replace('.', ','), out double threshold))
                    {
                        double sum = list.SumOfElementsGreaterThan(threshold);
                        Console.WriteLine($"Сума елементів більших за {threshold:F2}: {sum:F2}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Некоректне число.");
                        Console.ResetColor();
                    }
                }
                catch (InvalidOperationException ex)
                {
                    RedMessageException(ex);
                }
            }
            else
            {
                RedMessageListIsEmpty();
            }
        }
        public static void Case3_BelowAverageList(LinkedList list)
        {
            BlueMessageCurrentList();
            if (list.Count > 0)
            {
                try
                {
                    double average = list.GetAverage();
                    Console.WriteLine($"\nСереднє значення: {average:F2}");

                    LinkedList LessThanAverage = list.GetNewListWithElementsLessThanAverage();
                    Console.WriteLine("Новий список з елементів менших за середнє значення:");
                    PrintList(LessThanAverage);

                    Console.Write("\nВведіть ім’я для нового списку, щоб зберегти: ");
                    string newName = Console.ReadLine();

                    if (!userLists.ContainsKey(newName))
                    {
                        userLists[newName] = LessThanAverage;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Список \"{newName}\" додано до вашої колекції.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Список з ім’ям \"{newName}\" вже існує. Новий список **не був збережений**.");
                        Console.ResetColor();
                    }
                }
                catch (InvalidOperationException ex)
                {
                    RedMessageException(ex);
                }
            }
            else
            {
                RedMessageListIsEmpty();
            } 
        }
        public static void Case4_RemoveElementsAtEvenPositions(LinkedList list)
        {
            BlueMessageCurrentList();

            if (list.Count > 0)
            {
                try
                {
                    Console.WriteLine("\nСписок після видалення елементів на парних позиціях:");
                    list.RemoveElementsAtEvenPositions();
                    PrintList(list);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Примітка: тепер ваш список \"{currentUser}\" містить лише ці елементи");
                    Console.ResetColor();
                }
                catch (InvalidOperationException ex)
                {
                    RedMessageException(ex);
                }
            }
            else
            {
                RedMessageListIsEmpty();
            }
        }
        public static void Case5_ViewOrEditElementByIndex(LinkedList list)
        {
            BlueMessageCurrentList();

            if (list.Count > 0)
            {
                try
                {
                    Console.Write("\nВведіть індекс елемента для отримання: ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < list.Count)
                    {
                        Console.WriteLine($"Елемент з індексом {index}: {list[index]}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Елемента з індексом {index} не існує у даному списку.");
                        Console.ResetColor();
                    }
                }
                catch (InvalidOperationException ex)
                {
                    RedMessageException(ex);
                }
            }
            else
            {
                RedMessageListIsEmpty();
            }
        }
        public static void Case6_DisplayList(LinkedList list)
        {
            BlueMessageCurrentList();
            Console.WriteLine($"\nВаш список \"{currentUser}\" містить:");
            PrintList(list);
        }
        public static void Case7_DeleteElementByIndex(LinkedList list)
        {
            BlueMessageCurrentList();
            if (list.Count == 0)
            {
                RedMessageListIsEmpty();
                return;
            }
            Console.WriteLine("Вміст списку:");
            PrintList(list);
            Console.Write("Введіть індекс елемента, який потрібно видалити: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                try
                {
                    list.RemoveAt(index);
                    Console.WriteLine("Елемент успішно видалено.");
                    Console.WriteLine("Список після видалення:");
                    PrintList(list);
                }
                catch (InvalidOperationException ex)
                {
                    RedMessageException(ex);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Некоректний ввід індексу.");
                Console.ResetColor();
            }
        }
        public static void BlueMessageCurrentList()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Поточний список: \"{currentUser}\"");
            Console.ResetColor();
        }
        public static void RedMessageListIsEmpty()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Список порожній");
            Console.ResetColor();
        }
        public static void RedMessageException(InvalidOperationException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        public static void PrintList(LinkedList list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"[{i}] = {list[i]}");
            }
        }
        public static void IFListIsChosen()
        {
            while (currentUser == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nНаразі немає активного списку.");
                Console.ResetColor();
                Console.WriteLine("99 - Створити новий список");
                Console.WriteLine("33 - Вибрати існуючий список");

                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                if (choice == "99")
                    CreateNewList();
                else if (choice == "33")
                    SelectList();
                else { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невірний вибір.");
                    Console.ResetColor(); }
            }
        }
        public static void CreateNewList()
        {
            Console.Write("Введіть ім'я для нового списку: ");
            string name = Console.ReadLine();

            if (!userLists.ContainsKey(name))
            {
                userLists[name] = new LinkedList();
                currentUser = name;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Список \"{name}\" створено і вибрано як активний.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Список з таким ім'ям вже існує. Помилка створення.");
                Console.ResetColor();
            }
        }
        public static void SelectList()
        {
            if (userLists.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Наразі немає жодного списку. Спочатку створіть новий список.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Доступні списки:");
            foreach (var listname in userLists.Keys)
            {
                Console.WriteLine($"- {listname}");
            }

            Console.Write("Введіть ім’я списку, з яким хочете працювати: ");
            string name = Console.ReadLine();

            if(name == "" || name == null)
            {
                name = "default";
            }

            if (userLists.ContainsKey(name))
            {
                currentUser = name;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Активний список: {name}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Список не знайдено.");
                Console.ResetColor();
            }
        }

    }
}
