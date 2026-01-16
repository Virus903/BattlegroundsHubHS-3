using BattlegroundsHubHS.Core.Services;
using BattlegroundsHubHS.Core.Services;
using System;
using System.Linq;

namespace Battlegrounds_HS_Hub
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine("   BATTLEGROUNDS HUB - МЕНЮ");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Показать всех героев");
                Console.WriteLine("2. Показать всех миньонов");
                Console.WriteLine("3. Добавить нового героя");
                Console.WriteLine("4. Добавить нового миньона");
                Console.WriteLine("5. Найти героя по имени");
                Console.WriteLine("6. Выход");
                Console.WriteLine("==================================");
                Console.Write("Выберите действие (1-6): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllHeroes();
                        break;
                    case "2":
                        ShowAllMinions();
                        break;
                    case "3":
                        AddNewHero();
                        break;
                    case "4":
                        AddNewMinion();
                        break;
                    case "5":
                        SearchHeroByName();
                        break;
                    case "6":
                        exit = true;
                        Console.WriteLine("Выход из программы...");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор! Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowAllHeroes()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК ГЕРОЕВ ===");

            var heroes = InMemoryStorage.Heroes;

            if (heroes.Count == 0)
            {
                Console.WriteLine("Героев пока нет.");
            }
            else
            {
                foreach (var hero in heroes)
                {
                    Console.WriteLine($"[ID: {hero.Id}] {hero.Name}");
                    Console.WriteLine($"   Титул: {hero.Title}");
                    Console.WriteLine($"   Рейтинг: {hero.Tier}");
                    Console.WriteLine($"   Описание: {hero.Description}");
                    Console.WriteLine("----------------------------------");
                }
            }

            Console.WriteLine($"Всего героев: {heroes.Count}");
            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        static void ShowAllMinions()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК МИНЬОНОВ ===");

            var minions = InMemoryStorage.Minions;

            if (minions.Count == 0)
            {
                Console.WriteLine("Миньонов пока нет.");
            }
            else
            {
                foreach (var minion in minions)
                {
                    Console.WriteLine($"[ID: {minion.Id}] {minion.Name}");
                    Console.WriteLine($"   Уровень таверны: {minion.TavernTier}");
                    Console.WriteLine($"   Тип: {minion.Type}");
                    Console.WriteLine($"   Статы: {minion.Attack}/{minion.Health}");
                    Console.WriteLine($"   Эффект: {minion.Effect}");
                    Console.WriteLine("----------------------------------");
                }
            }

            Console.WriteLine($"Всего миньонов: {minions.Count}");
            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        static void AddNewHero()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОГО ГЕРОЯ ===");

            try
            {
                Console.Write("Введите имя героя: ");
                string name = Console.ReadLine();

                // Проверка как в методичке (не пустая строка)
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Ошибка: имя не может быть пустым!");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Введите титул (например, 'Пират', 'Демон'): ");
                string title = Console.ReadLine();

                Console.WriteLine("Выберите рейтинг:");
                Console.WriteLine("S - Самый сильный");
                Console.WriteLine("A - Очень сильный");
                Console.WriteLine("B - Средний");
                Console.WriteLine("C - Слабый");
                Console.WriteLine("D - Очень слабый");
                Console.WriteLine("F - Самый слабый");
                Console.Write("Рейтинг (S/A/B/C/D/F): ");
                string tierInput = Console.ReadLine().ToUpper();

                // Создаём нового героя
                var newHero = new BattlegroundsHubHS.Core.Models.Hero
                {
                    Name = name,
                    Title = title,
                    Description = "Новый герой, описание можно добавить позже"
                };

                // Парсим рейтинг
                if (Enum.TryParse<BattlegroundsHubHS.Core.Models.HeroTier>(tierInput, out var tier))
                {
                    newHero.Tier = tier;
                }
                else
                {
                    newHero.Tier = BattlegroundsHubHS.Core.Models.HeroTier.B; // по умолчанию
                }

                // Добавляем в хранилище
                InMemoryStorage.AddHero(newHero);

                Console.WriteLine($"\nГерой '{name}' успешно добавлен! ID: {newHero.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        static void AddNewMinion()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ НОВОГО МИНЬОНА ===");

            try
            {
                Console.Write("Введите имя миньона: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Ошибка: имя не может быть пустым!");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Уровень таверны (1-6): ");
                int tavernTier;
                while (!int.TryParse(Console.ReadLine(), out tavernTier) || tavernTier < 1  || tavernTier > 6)
                {
                    Console.Write("Ошибка! Введите число от 1 до 6: ");
                }

                Console.WriteLine("Тип миньона:");
                Console.WriteLine("1 - Пират");
                Console.WriteLine("2 - Демон");
                Console.WriteLine("3 - Механизм");
                Console.WriteLine("4 - Зверь");
                Console.WriteLine("5 - Дракон");
                Console.WriteLine("6 - Элементаль");
                Console.WriteLine("7 - Нага");
                Console.WriteLine("8 - Квилбоар");
                Console.WriteLine("9 - Нежить");
                Console.WriteLine("10 - Нейтральный");
                Console.Write("Выберите тип (1-10): ");

                var minionType = Console.ReadLine() switch
                {
                    "1" => BattlegroundsHubHS.Core.Models.MinionType.Pirate,
                    "2" => BattlegroundsHubHS.Core.Models.MinionType.Demon,
                    "3" => BattlegroundsHubHS.Core.Models.MinionType.Mech,
                    "4" => BattlegroundsHubHS.Core.Models.MinionType.Beast,
                    "5" => BattlegroundsHubHS.Core.Models.MinionType.Dragon,
                    "6" => BattlegroundsHubHS.Core.Models.MinionType.Elemental,
                    "7" => BattlegroundsHubHS.Core.Models.MinionType.Naga,
                    "8" => BattlegroundsHubHS.Core.Models.MinionType.Quilboar,
                    "9" => BattlegroundsHubHS.Core.Models.MinionType.Undead,
                    _ => BattlegroundsHubHS.Core.Models.MinionType.Neutral
                };

                Console.Write("Атака: ");
                int attack;
                while (!int.TryParse(Console.ReadLine(), out attack) || attack < 0)
                {
                    Console.Write("Ошибка! Введите положительное число: ");
                }

                Console.Write("Здоровье: ");
                int health;
                while (!int.TryParse(Console.ReadLine(), out health) || health < 1)
                {
                    Console.Write("Ошибка! Введите число не менее 1: ");
                }

                Console.Write("Эффект (способность): ");
                string effect = Console.ReadLine();

                var newMinion = new BattlegroundsHubHS.Core.Models.Minion
                {
                    Name = name,
                    TavernTier = tavernTier,
                    Type = minionType,
                    Attack = attack,
                    Health = health,
                    Effect = effect
                };

                InMemoryStorage.AddMinion(newMinion);
                Console.WriteLine($"\nМиньон '{name}' успешно добавлен! ID: {newMinion.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        static void SearchHeroByName()
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК ГЕРОЯ ПО ИМЕНИ ===");

            Console.Write("Введите часть имени для поиска: ");
            string searchTerm = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Поисковый запрос не может быть пустым!");
                Console.ReadKey();
                return;
            }

            var foundHeroes = InMemoryStorage.Heroes
                .Where(h => h.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (foundHeroes.Count == 0)
            {
                Console.WriteLine($"Героев с именем содержащим '{searchTerm}' не найдено.");
            }
            else
            {
                Console.WriteLine($"Найдено героев: {foundHeroes.Count}");
                foreach (var hero in foundHeroes)
                {
                    Console.WriteLine($"- {hero.Name} (ID: {hero.Id}, Рейтинг: {hero.Tier})");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }
    }
}
