using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Tamagotchis 
{
    [Serializable]
    public class Tamagotchi: IPet, ISerializable
    {
        protected int happinessIndex = 10;

        public readonly string name;
        private uint pace;
       
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Pace", Pace); info.AddValue("Happinessindex", happinessIndex); info.AddValue("State", state);
        }
        public List<string> aa = new List<string>() {"Normal","Normal","Normal"};
        public Tamagotchi() { }
        public Tamagotchi(SerializationInfo info, StreamingContext context)
        {
            pace = (uint)info.GetValue("pace", typeof(uint));
            happinessIndex = (int)info.GetValue("Happinessindex", typeof(int));
            state = (string)info.GetValue("State", typeof(string));
        }
        public Tamagotchi(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("message", nameof(name));
            }
            this.name = name;
        }
        private string state = "Normal";
        
        public string State
        {
            get => state;
            set
            {
                state = value;
               
            }
        }
        private string hand = "None";
        public string Hand
        {
            get => hand;
            set
            {
                hand = value;

            }
        }


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
        public uint Pace
        {
            get
            {
                return pace;
            }
            set
            {
                if (value != null)
                {
                    pace = value;
                }
            }
        }

        public int HappinessIndex { get => happinessIndex; set => happinessIndex = value; }

        public string Eat() { return string.Format($"{GetType().Name}, {name}, eats"); }
        public string Sleep()
        {  
           return string.Format($"{GetType().Name}, {name}, sleeps");
        }
        virtual public void Play() { System.Console.WriteLine($"{GetType().Name}, {name}, plays"); }

       
    }
    [Serializable]
    public class Lebowski : Tamagotchi, ISerializable
    {
        public Lebowski(SerializationInfo info, StreamingContext context)
        {
            Pace = (uint)info.GetValue("Pace", typeof(uint));
            happinessIndex = (int)info.GetValue("Happinessindex", typeof(int));
            State = (string)info.GetValue("State", typeof(string));
        }
        public Lebowski() { }
        public Lebowski(string name) : base(name) { }

        virtual public void Medicate() {/*Increases happiness index by 1 point,TOO MUCH MEDICINE KILLS HIM*/}
        virtual public void Distress() {/*Decreases happiness index by 3 points */System.Console.WriteLine($"{GetType().Name}, {name}, needs to pee"); }
        virtual public void Sickness() {/*Decreases happiness index by 1 point*/System.Console.WriteLine($"{GetType().Name}, {name}, Sick"); }
        virtual public void Hunger() {/*Decreases happiness index by 1 point*/System.Console.WriteLine($"{GetType().Name}, {name}, hungry"); }
        virtual public void Peeing() {/*increases happiness index by 1 point*/}
        virtual public void Boredom() {/*Decreases happiness index*/ System.Console.WriteLine($"{GetType().Name}, {name}, Bored"); }
        virtual public void Jump() { }
        
        public override bool Act(uint count)
        {
            bool result = false;
            if (count % Pace == 0)
            {
                Hunger();
                Console.WriteLine(Eat());
                Console.WriteLine(Sleep());
                Boredom();
                Play();
                result = true;
            }
            return result;
        }
     /*  public override string ToString()
        {
            return string.Format("{0}:{1} reaction pace is {2} and current happiness index is {3}", GetType().Name, name, Pace, HappinessIndex);
        }*/

    }

}
