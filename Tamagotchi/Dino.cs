namespace Tamagotchis
{
    public class Dino : Tamagotchi
    {
        public Dino(string name):base(name)
        {
            happinessIndex = 10;
        }
        public override string ToString()
        {
            return GetType().Name + ":" + name + ", Reaction pace " + pace + ", Happinessindex " + happinessIndex;
        }
        override public string Jump()
        {
            return ($"{GetType().Name}, {name},   jumps");
        }
    }
}
