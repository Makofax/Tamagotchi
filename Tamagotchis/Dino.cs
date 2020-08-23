namespace Tamagotchis
{
    public class Dino : Tamagotchi
    {
        public Dino(string name):base(name)
        {
            HappinessIndex = 10;
        }
        public override string ToString()
        {
            return GetType().Name + ":" + name + ", Reaction pace " + Pace + ", Happinessindex " + HappinessIndex;
        }
       
    }
}
