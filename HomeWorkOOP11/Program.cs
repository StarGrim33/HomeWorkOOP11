namespace HomeWorkOOP11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    class Aquarium
    {
        private List<Fish> _fish = new List<Fish>();

        public Aquarium(List<Fish> fish)
        {
            _fish = fish;
        }

        public int Quantity { get; private set; }
    }

    class FishBuilder
    {
        public Fish Build(int quantity)
        {
            List<Fish> fish = new List<Fish>();

            for(int i = 0; i < quantity; i++)
            {
                fish.
            }
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

        private Fish CreateRandomFish()
        {
            Random random = new Random();
            var fishes = CreateFish();
            int randomIndex = random.Next(fish.Count);
            Fish fish = fishes[randomIndex];

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

        public void ShowStats()
        {
            Console.WriteLine($"{Name}, здоровье: {Health}, возраст: {Age}");
        }

        protected virtual void GrowOld()
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

        protected override void GrowOld()
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

        protected override void GrowOld()
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

        protected override void GrowOld()
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

        protected override void GrowOld()
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

        protected override void GrowOld()
        {
            Health -= 4;
            Age += 1;
        }
    }
}