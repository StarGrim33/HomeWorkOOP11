namespace HomeWorkOOP11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new();
            FishBuilder fishBuilder = new();
            aquarium.Run(fishBuilder);
        }
    }

    class Aquarium
    {
        private FishBuilder _fishBuilder = new();

        public void Run(FishBuilder fish)
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
                        Start(fish);
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

        private void Start(FishBuilder fish)
        {
            int maxFishes = 10;
            Console.WriteLine("Введите название популяции рыб: ");
            string? userInput = Console.ReadLine();

            Population population = _fishBuilder.Build(userInput, maxFishes);

            Console.WriteLine("Популяция рыб сформирована, нажмите любую кнопку для продолжения");
            Console.ReadKey();
            Console.Clear();

            population.ShowPopulation();
            Console.WriteLine($"{new string('-', 25)}");

            while(population.HasAliveFishes)
            {
                population.ToOldPopulation();
                population.RemoveDead();

                Console.Clear();
                population.ShowPopulation();
                population.AskAddFishes(fish);
            }

            if (population.HasAliveFishes == false)
            {
                Console.WriteLine("Все рыбки состарились и умерли");
            }

            Console.ReadKey();
        }
    }

    class Population
    {
        private List<Fish> _fish = new();

        public Population(string name, List<Fish> fish)
        {
            Name = name;
            _fish = fish;
            Quantity = _fish.Count;
        }

        public int Quantity { get; private set; }
        public string Name { get; private set; }

        public bool HasAliveFishes => _fish.Count > 0;

        public void ShowPopulation()
        {
            string fishesAlive = "Перед Вами красочный аквариум, самые красивые и необычные рыбки плавают и радуются жизни";
            string fishesAreDead = "Аквариум пуст, рыбки состарились";

            if(HasAliveFishes)
            {
                Console.WriteLine($"{fishesAlive}");
            }
            else
            {
                Console.WriteLine($"{fishesAreDead}");
            }

            for(int i = 0; i < _fish.Count; i++)
            {
                var fish = _fish[i];
                Console.WriteLine($"{i + 1} - {fish.Name}, здоровье: {fish.Health}, возраст: {fish.Age}");
            }
        }

        public void ToOldPopulation()
        {
            foreach(Fish fish in _fish)
            {
                fish.GrowOld();
            }

            Console.WriteLine("Одна итерация старения прошла, нажмите любую клавишу");
        }

        public void RemoveDead()
        {
            for(int i = 0; i < _fish.Count; i++)
            {
                if (_fish[i].Health < 0)
                    _fish.RemoveAt(i);
            }
        }

        public void AskAddFishes(FishBuilder fishBuilder)
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

            while(isUserChoice)
            {
                switch (userInput)
                {
                    case CommandAddFishes:
                        _fish.Add(fishBuilder.CreateRandomFish());
                        isUserChoice = false;
                        break;

                        case CommandRemoveFishes:
                        RemoveFish();
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

        private void RemoveFish()
        {
            Console.Clear();
            Console.WriteLine("Какую рыбку Вы хотите достать из аквариума?");
            ShowPopulation();

            bool isNumber = int.TryParse(Console.ReadLine(), out int userChoice);

            if(isNumber)
            {
                if(userChoice <= _fish.Count && userChoice > 0)
                {
                    _fish.RemoveAt(userChoice - 1);
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

    class FishBuilder
    {
        public Population Build(string name, int quantity)
        {
            List<Fish> fish = new List<Fish>();

            for (int i = 0; i < quantity; i++)
            {
                fish.Add(CreateRandomFish());
            }

            return new Population(name, fish);
        }

        public Fish CreateRandomFish()
        {
            Random random = new Random();
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
            Age += 2;
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
            Age += 3;
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
            Age += 4;
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
}