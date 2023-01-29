namespace HomeWorkOOP11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new();
            FishBuilder fishBuilder = new();
            aquarium.Run(fishBuilder, aquarium);
        }
    }

    class Aquarium
    {
        private FishBuilder _fishBuilder = new();
        private List<Fish> _fishes = new();

        public Aquarium()
        {
            int startAmount = 10;

            for(int i = 0; i < startAmount; i++)
            {
                _fishes.Add(_fishBuilder.CreateRandomFish());
            }
        }

        public bool HasAliveFishes => _fishes.Count > 0;
        public int FishesCount => _fishes.Count;

        public void Run(FishBuilder fish, Aquarium aquarium)
        {
            const string CommandStartALife = "1";
            const string CommandExit = "2";

            Console.WriteLine($"Жизнь аквариумная, жизнь общажная!\n");
            Console.WriteLine($"{CommandStartALife}-Начать жизнь в аквариуме");
            Console.WriteLine($"{CommandExit}-Выйти");

            string? userInput = Console.ReadLine();
            bool isProgramOn = true;

            while(isProgramOn)
            {
                switch (userInput)
                {
                    case CommandStartALife:
                        Start(fish, aquarium);
                        break;

                    case CommandExit:
                        isProgramOn = false;
                        break;

                    default:
                        Console.WriteLine("Ввведите цифру пункта меню");
                        break;
                }
            }
        }

        public void ShowPopulation()
        {
            string fishesAlive = "Перед Вами красочный аквариум, самые красивые и необычные рыбки плавают и радуются жизни";
            string fishesAreDead = "Аквариум пуст, рыбки состарились";

            if (HasAliveFishes)
            {
                Console.WriteLine($"{fishesAlive}");
            }
            else
            {
                Console.WriteLine($"{fishesAreDead}");
            }

            for (int i = 0; i < _fishes.Count; i++)
            {
                var fish = _fishes[i];
                Console.WriteLine($"{i + 1} - {fish.Name}, здоровье: {fish.Health}, возраст: {fish.Age}");
            }
        }

        public void AddFish(FishBuilder fishBuilder)
        {
            _fishes.Add(fishBuilder.CreateRandomFish());
        }

        public void RemoveFish(int index)
        {
            _fishes.RemoveAt(index - 1);
        }

        private void Start(FishBuilder fish, Aquarium aquarium)
        {
            User user = new();

            ShowPopulation();
            Console.WriteLine($"{new string('-', 25)}");

            while(HasAliveFishes)
            {
                SkipDay();
                RemoveDead();

                Console.Clear();
                ShowPopulation();
                user.AskAddFishes(fish, aquarium);
            }

            if (HasAliveFishes == false)
            {
                Console.WriteLine("Все рыбки состарились и умерли");
            }

            Console.ReadKey();
        }

        private void SkipDay()
        {
            foreach (Fish fish in _fishes)
            {
                fish.GrowOld();
            }

            Console.WriteLine("Одна итерация старения прошла, нажмите любую клавишу");
        }

        private void RemoveDead()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].Health < 0)
                    _fishes.RemoveAt(i);
            }
        }
    }

    class FishBuilder
    {
        public Aquarium Build()
        {
            int fishesCount = 10;

            List<Fish> fish = new ();

            for (int i = 0; i < fishesCount; i++)
            {
                fish.Add(CreateRandomFish());
            }

            return new Aquarium();
        }

        public Fish CreateRandomFish()
        {
            Random random = new();
            var fishes = CreateFish();
            int randomIndex = random.Next(fishes.Count);
            return fishes[randomIndex];
        }

        private List<Fish> CreateFish()
        {
            return new List<Fish>()
            {
                new DropFish("Рыба капля", 100, 1),
                new ClowFish("Рыба клоун", 160, 1),
                new Pike("Щука", 120, 1),
                new Salmon("Семга", 170, 1),
                new Tuna("Тунец", 200, 1),
            };
        }
    }

    abstract class Fish
    {
        public Fish(string name, int health, int age)
        {
            Name = name;
            Health = health;
            Age = age;
        }

        public string Name { get; protected set; }
        public int Age { get; protected set; } = 0;
        public int Health { get; protected set; } = 100;

        public virtual void GrowOld()
        {
            Health -= 10;
            Age += 1;
        }
    }

    class DropFish : Fish
    {
        public DropFish(string name, int health, int age) : base(name, health, age)
        {
        }

        public override void GrowOld()
        {
            Health -= 20;
            Age += 1;
        }
    }

    class ClowFish : Fish
    {
        public ClowFish(string name, int health, int age) : base(name, health, age)
        {
        }

        public override void GrowOld()
        {
            Health -= 20;
            Age += 1;
        }
    }

    class Pike : Fish
    {
        public Pike(string name, int health, int age) : base(name, health, age)
        {
        }

        public override void GrowOld()
        {
            Health -= 30;
            Age += 1;
        }
    }


    class Salmon : Fish
    {
        public Salmon(string name, int health, int age) : base(name, health, age)
        {
        }

        public override void GrowOld()
        {
            Health -= 5;
            Age += 1;
        }
    }

    class Tuna : Fish
    {
        public Tuna(string name, int health, int age) : base(name, health, age)
        {
        }

        public override void GrowOld()
        {
            Health -= 10;
            Age += 1;
        }
    }

    class User
    {
        public User()
        {
            Name = ToWelcome();
        }

        public string Name { get; set; }

        public string ToWelcome()
        {
            Console.WriteLine("Как Вас зовут?");
            string? userName = Console.ReadLine();

            if(userName == null)
            {
                return userName = "Аноним";
            }

            return userName;
        }

        public void AskAddFishes(FishBuilder fishBuilder, Aquarium aquarium)
        {
            const string CommandAddFishes = "1";
            const string CommandRemoveFishes = "2";
            const string CommandNo = "3";

            bool isUserChoice = true;

            Console.WriteLine("Желаете добавить рыбку?");
            Console.WriteLine($"{CommandAddFishes}-Добавить случайную рыбку");
            Console.WriteLine($"{CommandRemoveFishes}-Вытащить рыбок");
            Console.WriteLine($"{CommandNo}-Отказаться");

            string? userInput = Console.ReadLine();

            while (isUserChoice)
            {
                switch (userInput)
                {
                    case CommandAddFishes:
                        aquarium.AddFish(fishBuilder);
                        isUserChoice = false;
                        break;

                    case CommandRemoveFishes:
                        AskRemoveFish(aquarium);
                        isUserChoice = false;
                        break;

                    case CommandNo:
                        isUserChoice = false;
                        break;

                    default:
                        Console.WriteLine("Нужно ввести цифру пункта меню");
                        isUserChoice = false;
                        break;
                }
            }
        }

        public void AskRemoveFish(Aquarium aquarium)
        {
            Console.Clear();
            Console.WriteLine("Какую рыбку Вы хотите достать из аквариума?");
            aquarium.ShowPopulation();

            bool isNumber = int.TryParse(Console.ReadLine(), out int userChoice);

            if (isNumber)
            {
                if (userChoice <= aquarium.FishesCount && userChoice > 0)
                {
                    aquarium.RemoveFish(userChoice);
                    Console.WriteLine($"Рыбка вынута");
                }
                else
                {
                    Console.WriteLine("Ошибка");
                }
            }
            else
            {
                Console.WriteLine("Нужно ввести число");
            }
        }
    }
}