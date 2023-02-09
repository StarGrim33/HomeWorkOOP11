namespace HomeWorkOOP11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new();
            user.Run();
        }
    }

    class Aquarium
    {
        private FishBuilder _fishBuilder = new();
        private List<Fish> _fishes = new();

        public Aquarium()
        {
            Fill();
        }

        public bool HasAliveFishes => _fishes.Count > 0;

        public int FishesCount => _fishes.Count;

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
                Console.WriteLine($"{i + 1} - {_fishes[i].Name}, здоровье: {_fishes[i].Health}, возраст: {_fishes[i].Age}");
            }
        }

        public void AddFish()
        {
            _fishes.Add(_fishBuilder.CreateRandomFish());
        }

        public void RemoveFish(int index)
        {
            _fishes.RemoveAt(index - 1);
        }

        public void SkipDay()
        {
            foreach (Fish fish in _fishes)
            {
                fish.GrowOld();
            }

            Console.WriteLine("Одна итерация старения прошла, нажмите любую клавишу");
        }

        public void RemoveDeadFishes()
        {
            //for (int i = 0; i < _fishes.Count; i++)
            //{
            //    if (_fishes[i].Health <= 0)
            //    {
            //        _fishes.RemoveAt(i);
            //    }
            //}

            foreach(Fish fish in _fishes.ToList())
            {
                if(fish.Health <= 0)
                {
                    _fishes.Remove(fish);
                }
            }
        }

        private void Fill()
        {
            int startAmount = 10;

            for (int i = 0; i < startAmount; i++)
            {
                AddFish();
            }
        }
    }

    class FishBuilder
    {
        private static Random _random = new();

        private List<Fish> _fishTemplates;

        public FishBuilder()
        {
            _fishTemplates = new List<Fish>()
            {
                new DropFish("Рыба капля", 120, 1),
                new ClowFish("Рыба клоун", 160, 1),
                new Pike("Щука", 120, 1),
                new Salmon("Семга", 170, 1),
                new Tuna("Тунец", 200, 1),
            };
        }

        public Fish CreateRandomFish()
        {
            int randomIndex = _random.Next(_fishTemplates.Count);
            Fish fish = _fishTemplates[randomIndex];
            return new Fish(fish.Name, fish.Health, fish.Age);
        }
    }

    class Fish
    {
        private int _minHealth = 0;

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

            if (Health < _minHealth)
                Health = _minHealth;
        }
    }

    class DropFish : Fish
    {
        public DropFish(string name, int health, int age) : base(name, health, age) { }

        public override void GrowOld()
        {
            Health -= 20;
            Age += 1;
        }
    }

    class ClowFish : Fish
    {
        public ClowFish(string name, int health, int age) : base(name, health, age) { }

        public override void GrowOld()
        {
            Health -= 30;
            Age += 1;
        }
    }

    class Pike : Fish
    {
        public Pike(string name, int health, int age) : base(name, health, age) { }

        public override void GrowOld()
        {
            Health -= 10;
            Age += 1;
        }
    }

    class Salmon : Fish
    {
        public Salmon(string name, int health, int age) : base(name, health, age) { }

        public override void GrowOld()
        {
            Health -= 5;
            Age += 1;
        }
    }

    class Tuna : Fish
    {
        public Tuna(string name, int health, int age) : base(name, health, age) { }

        public override void GrowOld()
        {
            Health -= 10;
            Age += 1;
        }
    }

    class User
    {
        private Aquarium _aquarium = new();

        public User()
        {
            Name = ToWelcome();
        }

        public string Name { get; private set; }

        public void Run()
        {
            const string CommandStartALife = "1";
            const string CommandExit = "2";

            Console.Clear();
            Console.WriteLine($"Здравствуйте, {Name}");
            Console.WriteLine($"Жизнь аквариумная, жизнь общажная!\n");
            Console.WriteLine($"{CommandStartALife}-Начать жизнь в аквариуме");
            Console.WriteLine($"{CommandExit}-Выйти");

            string userInput = Console.ReadLine()!;
            bool isProgramOn = true;

            while (isProgramOn)
            {
                switch (userInput)
                {
                    case CommandStartALife:
                        Start();
                        isProgramOn = false;
                        break;

                    case CommandExit:
                        isProgramOn = false;
                        break;

                    default:
                        Console.WriteLine("Нужно ввести цифру пункта меню");
                        isProgramOn = false;
                        break;
                }
            }
        }

        private void ShowFeatures()
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
                        _aquarium.AddFish();
                        isUserChoice = false;
                        break;

                    case CommandRemoveFishes:
                        AskRemoveFish();
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

        private void AskRemoveFish()
        {
            Console.Clear();
            Console.WriteLine("Какую рыбку Вы хотите достать из аквариума?");
            _aquarium.ShowPopulation();

            bool isNumber = int.TryParse(Console.ReadLine(), out int userChoice);

            if (isNumber)
            {
                if (userChoice <= _aquarium.FishesCount && userChoice > 0)
                {
                    _aquarium.RemoveFish(userChoice);
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

        private void Start()
        {
            _aquarium.ShowPopulation();
            Console.WriteLine($"{new string('-', 25)}");

            while (_aquarium.HasAliveFishes)
            {
                _aquarium.SkipDay();
                _aquarium.RemoveDeadFishes();

                Console.Clear();

                _aquarium.ShowPopulation();
                ShowFeatures();
            }

            if (_aquarium.HasAliveFishes == false)
            {
                Console.WriteLine("Все рыбки состарились и умерли");
            }

            Console.ReadKey();
        }

        private string ToWelcome()
        {
            Console.WriteLine("Как Вас зовут?");
            string? userName = Console.ReadLine();

            if (userName == null)
            {
                return userName = "Аноним";
            }

            return userName;
        }
    }
}