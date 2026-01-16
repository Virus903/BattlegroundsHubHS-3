using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using BattlegroundsHubHS.Core.Models;
using System.Security.Cryptography.X509Certificates;

namespace BattlegroundsHubHS.Core.Services
{
    public static class InMemoryStorage
    {
        // Коллекция для харанения данных в памяти 

        public static List<Hero> Heroes { get; set; } = new();
        public static List<Minion> Minions { get; set; } = new();


        // Инциализация тестовыми данными
        static InMemoryStorage()
        {
            InitializeData();
        }

        private static void InitializeData()
        {
            // тестовые герои
            Heroes.AddRange(new List<Hero>
            {
                new Hero
                {
                    Id = 1,
                    Name = "Милхаус Манашторм",
                    Title = "Демон",
                    Tier = HeroTier.S,
                    Description = "Сильный герой для агрессивной игры. Начинает с дополнительным золотом.",
                    ImageUrl = "/images/piteratecaptain.png"

                },
                new Hero
                {
                    Id = 2,
                    Name = "Пачкай Смертохлёст",
                    Title = "Пират",
                    Tier = HeroTier.A,
                    Description = "Хорош в пиратских сборках. Имеет увеличенное здоровье.",
                    ImageUrl = "/images/patchwerk.png"
                },
                new Hero
                {
                    Id = 3,
                    Name = "Штормовая зыбь",
                    Title = "Нага",
                    Tier = HeroTier.B,
                    Description = "Стабильный герой для наг. Даёт бонусы магам в таверне",
                    ImageUrl = "/images/tidemistress.png"
                }
            });


            // Тестовые миньоны
            Minions.AddRange(new List<Minion>
            {
                new Minion
                {
                    Id = 1,
                    Name = "Гончая Термопласта",
                    TavernTier = 4,
                    Type = MinionType.Mech,
                    Attack = 4,
                    Health = 4,
                    Effect = "В конце хода даёт магнит соседним мехам",
                    ImageUrl = "/images/thermoplast.png"
                },

                 new Minion
                {
                    Id = 2,
                    Name = "Пиратский капитан",
                    TavernTier = 5,
                    Type = MinionType.Pirate,
                    Attack = 5,
                    Health = 4,
                    Effect = "Даёт +1/+1 всем пиратам в бою",
                    ImageUrl = "/images/piratecapitane.png"
                },

                  new Minion
                {
                    Id = 3,
                    Name = "Гниющий зомби",
                    TavernTier = 2,
                    Type = MinionType.Undead,
                    Attack = 2,
                    Health = 3,
                    Effect = "Возраждается с 1 здоровьем после смерти.",
                    ImageUrl = "/images/rottenzombie.png"
                }
            });

        }

        public static void AddHero(Hero hero)
        {
            hero.Id = Heroes.Count > 0 ? Heroes.Max(h => h.Id) + 1 : 1;
            Heroes.Add(hero);
        }
        public static void AddMinion(Minion minion)
        {
            minion.Id = Minions.Count > 0 ? Heroes.Max(h => h.Id) + 1 : 1;
            Minions.Add(minion);
        }

        // Метод для поиска героя по имени
        public static Hero?
            FindHeroByName(string name)
        {
            return Heroes.FirstOrDefault(h => h.Name.Contains(name, System.StringComparison.OrdinalIgnoreCase));

        }
    }
}
