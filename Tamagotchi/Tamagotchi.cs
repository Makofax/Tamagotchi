using Pets;

namespace Tamagotchis
{
    abstract public class Tamagotchi : IPet
    {
        protected uint happinessIndex;
        public readonly string name;
        protected uint pace;

        protected Tamagotchi(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }

            this.name = name;
        }

        public uint Pace { get => pace; set => pace = value; }

        virtual public bool Act(uint count)
        {
            bool result = false;
            if (count % pace == 0)
            {
                Eat();
                Sleep();
                Play();                
                result = true;
            }
            return result;
        }
        virtual public string Eat()
        {
            return($"{GetType().Name}, {name},   eats");
        }

        virtual public string Sleep()
        {
            return($"{GetType().Name}, {name},   sleeps");
        }
        virtual public string Play()
        {
            return($"{GetType().Name}, {name},   plays");
        }
        virtual public string Jump()
        {
            return ($"{GetType().Name}, {name},   jumps");
        }
    }
}
