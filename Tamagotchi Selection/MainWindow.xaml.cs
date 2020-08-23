using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tamagotchis;
using System.ComponentModel;
using Games;
using System.Windows.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;


namespace Tamagotchi_Selection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        private DispatcherTimer ticker;
        private string Path = "../Resources/";
        private string filep = "G:/Tamagotchi Selection/Tamagotchi Selection/Resources/Tam9.dat";
        private uint count = 1000;
        private bool playing = false;
        public MainWindow()
        {
            InitializeComponent(); //DataContext = this;
            Init();
            this.Closed += new EventHandler(WindowClose);
            DeserializeData();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            /*dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5000);
            dispatcherTimer.Start();*/
            ticker = new System.Windows.Threading.DispatcherTimer();
            ticker.Tick += new EventHandler(Ticker_Tick);
            ticker.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            ticker.Start();

        }
        /*private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            

        }*/
        //vvvvvvvA real random number generator, each iteration is differentvvvvvv
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }

        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^//
        private Games.Game game = new Games.Game();
        private Tamagotchi z = new Tamagotchi();
        //Could have used tamagotchi list but I wanted to make it harder for myself
        /*Lebowski Rex = new Lebowski("Rex") { Pace = 3 };
        Lebowski Worm = new Lebowski("Worm") { Pace = 2 };
        Lebowski Beast = new Lebowski("Beast") { Pace = 7 };*/

        private List<Tamagotchi> tamagotchis = new List<Tamagotchi>();
        private List<string> states = new List<string>();    
        public void DeserializeData()
        {
            if (z.aa[0] != "Dead" && z.aa[1] != "Dead" && z.aa[2] != "Dead")
            {
                Deserialize();
            }
            else
            {
                File.Delete(filep);
            }
        }
        public void Deserialize()
        {
            
            if (File.Exists(filep) == false)
            {
                tamagotchis.Add(new Lebowski("Worm") { Pace = 2 });
                tamagotchis.Add(new Lebowski("Rex") { Pace = 3 });
                tamagotchis.Add(new Lebowski("Beast") { Pace = 7 });
            }
            else
            {
                BinaryFormatter serializer3 = new BinaryFormatter();

                FileStream fs2 = File.OpenRead(filep);
                
                    try
                    {
                    tamagotchis = (List<Tamagotchi>)serializer3.Deserialize(fs2);
                    }
                    catch (Exception )
                    {
                        
                    }
                    
                
            }
        }
       
        //private List<Window> tamagotchis = new List<Window>();
        public void Init()
        {
           
          

            
        }
        //actions are done according to pace and ticks
        private void Ticker_Tick(object sender, EventArgs e)
        {


            Statetext();
            if (dispatcherTimer != null)
            {
                if (count >= 0)
                {
                    for (int i = 0; i < tamagotchis.Count; i++)
                    {
                        if (count-- % tamagotchis[i].Pace == 0)
                        {
                            
                            HappinessText();
                            CheckDead();
                            Random_State();
                        }
                    }
                }
                else
                {
                    count = 1000;
                    CommandManager.InvalidateRequerySuggested();
                }
            }

        } 
        public void WindowClose(object sender,EventArgs e)
        {
            SerializeData();
            this.Close();
        }
        private void SerializeData()
        {



            using (Stream fs = new FileStream(filep, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                try
                {

                    serializer.Serialize(fs, tamagotchis);
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Serialization failed");
                }
                finally
                {

                    fs.Close();

                }

            }
            
           

        }
        public Tamagotchis.Tamagotchi Lempis { get; set; }
        private Tamagotchi temp = new Tamagotchi();
        //HappinessIndex to text to display on Xaml
        private void HappinessText()
        {
            WormPoint.Text = $"Worm's Happiness: {tamagotchis[0].HappinessIndex.ToString()}";
            RexPoint.Text = $"Rex's Happiness: {tamagotchis[1].HappinessIndex.ToString()}";
            BeastPoint.Text = $"Beast's Happiness: {tamagotchis[2].HappinessIndex.ToString()}";
        }
        //States to text to display on Xaml

        private void Statetext()
        {
            Wormtx.Text = tamagotchis[0].State;
            Rextx.Text = tamagotchis[1].State;
            Beasttx.Text = tamagotchis[2].State;
        }
        private void CheckDead()
        {
            for (int i = 0; i < tamagotchis.Count; i++)
            {
                if (tamagotchis[i].HappinessIndex < 1)
                {
                    tamagotchis[i].State = "Dead";
                    z.aa[i] = "Dead";
                    tamagotchis[i].HappinessIndex = 0;
                    if (i == 0)
                    {
                        Picture1.Source = new BitmapImage(new Uri(String.Concat(Path, "Worm" + "dead.png"), UriKind.Relative));
                    }
                    else if (i == 1)
                    {
                        Picture3.Source = new BitmapImage(new Uri(String.Concat(Path, "Rex" + "dead.png"), UriKind.Relative));
                    }
                    else if (i == 2)
                    {
                        Picture2.Source = new BitmapImage(new Uri(String.Concat(Path, "Beast" + "dead.png"), UriKind.Relative));
                    }
                }
            }
        }

        //Random state generator
        private void Random_State()
        {

           
            for (int i = 0; i < tamagotchis.Count; i++)
            {
                int roll = RandomNumber(0, 11);
                if (tamagotchis[i].State != "Hungry" && tamagotchis[i].State != "Sleepy" && tamagotchis[i].State != "Dead")
                {
                    if (roll == 7)
                    {
                        tamagotchis[i].State = "Sleepy";
                        if (i == 0)
                        {
                            tamagotchis[i].HappinessIndex -= 3;
                            Picture1.Source = new BitmapImage(new Uri(String.Concat(Path, "Worm" + "tired.png"), UriKind.Relative));
                        }
                        else if (i == 1)
                        {
                            tamagotchis[i].HappinessIndex -= 5;
                            Picture2.Source = new BitmapImage(new Uri(String.Concat(Path, "Rex" + "tired.png"), UriKind.Relative));
                        }
                        else if (i == 2)
                        {
                            tamagotchis[i].HappinessIndex -= 4;
                            Picture3.Source = new BitmapImage(new Uri(String.Concat(Path, "Beast" + "tired.png"), UriKind.Relative));
                        }
                    }
                    else if (roll == 2)
                    {
                        tamagotchis[i].State = "Hungry";
                        tamagotchis[i].HappinessIndex--;
                        if (i == 0)
                        {
                            tamagotchis[i].HappinessIndex--;
                            Picture1.Source = new BitmapImage(new Uri(String.Concat(Path, "Worm" + "hungry.png"), UriKind.Relative));
                        }
                        else if (i == 1)
                        {
                            tamagotchis[i].HappinessIndex--;
                            Picture2.Source = new BitmapImage(new Uri(String.Concat(Path, "Rex" + "hungry.png"), UriKind.Relative));
                        }
                        else if (i == 2)
                        {
                            tamagotchis[i].HappinessIndex--;
                            Picture3.Source = new BitmapImage(new Uri(String.Concat(Path, "Beast" + "hungry.png"), UriKind.Relative));
                        }
                    }
                }
            }

        }


        private void Eat_Button_Click(object sender, RoutedEventArgs e)
        {
            string Name = (sender as System.Windows.Controls.Button).Name.ToString();
            
            if (Name == RexEat.Name)
            {

                if (tamagotchis[1].State == "Hungry")
                {
                    tamagotchis[1].HappinessIndex++;
                    Picture2.Source = new BitmapImage(new Uri(String.Concat(Path, "Rex" + ".png"), UriKind.Relative));
                    tamagotchis[1].State = "Normal";

                }

            }
            if (Name == BeastEat.Name )
            {

                if (tamagotchis[2].State == "Hungry")
                {
                    tamagotchis[2].HappinessIndex++;
                    Picture3.Source = new BitmapImage(new Uri(String.Concat(Path, "Beast" + ".png"), UriKind.Relative));
                    tamagotchis[2].State = "Normal";

                }

            }
            if (Name == WormEat.Name )
            {

                if (tamagotchis[0].State == "Hungry")
                {
                    tamagotchis[0].HappinessIndex++;
                    Picture1.Source = new BitmapImage(new Uri(String.Concat(Path, "Worm" + ".png"), UriKind.Relative));
                    tamagotchis[0].State = "Normal";

                }

            }

        }

        private void Sleep_Button_Click(object sender, RoutedEventArgs e)
        {
            string Name = (sender as System.Windows.Controls.Button).Name.ToString();
            
            if (Name == RexSleep.Name)
            {
                if ((tamagotchis[1].State == "Sleepy" && playing == false))
                {
                    Picture2.Source = new BitmapImage(new Uri(String.Concat(Path, "Rex" + ".png"), UriKind.Relative));
                    tamagotchis[1].State = "Normal";
                }

            }
            if (Name == BeastSleep.Name)
            {
                if ((tamagotchis[2].State == "Sleepy" && playing == false))
                {
                    Picture3.Source = new BitmapImage(new Uri(String.Concat(Path, "Beast" + ".png"), UriKind.Relative));
                    tamagotchis[2].State = "Normal";
                }

            }
            if (Name == WormSleep.Name)
            {
                if ((tamagotchis[0].State == "Sleepy" && playing == false))
                {
                    Picture1.Source = new BitmapImage(new Uri(String.Concat(Path, "Worm" + ".png"), UriKind.Relative));
                    tamagotchis[0].State = "Normal";
                }

            }
           
            
        }

        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {

           
            Pick(sender,e);


        }
        private void Pick(object sender, RoutedEventArgs e)
        {
            string Name = (sender as System.Windows.Controls.Button).Name.ToString();

            if (tamagotchis[1].HappinessIndex > 0 && tamagotchis[2].State != "Dead" && tamagotchis[1].State == "Normal")
            {
                if (Name == RexPlay.Name )
                {
                    Dodgeball main = new Dodgeball(tamagotchis[1]);
                    playing = true;
                    tamagotchis[1].State = "Playing";
                    main.Show();
                    tamagotchis[1].State = "Normal";
                }
            }
            if (tamagotchis[2].HappinessIndex > 0 && tamagotchis[2].State != "Dead" && tamagotchis[2].State == "Normal")
            {
                if (Name == BeastPlay.Name)
                {
                    Dodgeball main = new Dodgeball(tamagotchis[2]);
                    tamagotchis[2].State = "Playing";
                    playing = true;
                    main.Show();
                    tamagotchis[2].State = "Normal";
                }
            }

            if (tamagotchis[0].HappinessIndex > 0 && tamagotchis[0].State != "Dead" && tamagotchis[0].State == "Normal")
            {
                if (Name == WormPlay.Name)
                {
                    Dodgeball main = new Dodgeball(tamagotchis[0]);
                    tamagotchis[0].State = "Playing";
                    playing = true;
                    main.Show();
                    tamagotchis[0].State = "Normal";
                }
            }
            playing = false;

        }
        private void Interact_Button_Click(object sender, RoutedEventArgs e)
        {
            string Name = (sender as System.Windows.Controls.Button).Name.ToString();

            if (tamagotchis[1].HappinessIndex > 0 && tamagotchis[2].State != "Dead" && tamagotchis[1].State == "Normal")
            {
                if (Name == RexInteractWorm.Name)
                {
                    Interaction main = new Interaction(tamagotchis[1], tamagotchis[0]);
                    tamagotchis[1].State = "Playing";
                    tamagotchis[0].State = "Playing";
                    main.Show();
                    tamagotchis[1].State = "Normal";
                    tamagotchis[0].State = "Normal";
                }
                if (Name == RexInteractBeast.Name)
                {
                    Interaction main = new Interaction(tamagotchis[1], tamagotchis[2]);
                    tamagotchis[1].State = "Playing";
                    tamagotchis[2].State = "Playing";
                    main.Show();
                    tamagotchis[1].State = "Normal";
                    tamagotchis[2].State = "Normal";
                }
            }
            if (tamagotchis[2].HappinessIndex > 0 && tamagotchis[2].State != "Dead" && tamagotchis[2].State == "Normal")
            {
                if (Name == BeastInteractRex.Name)
                {
                    Interaction main = new Interaction(tamagotchis[2], tamagotchis[1]);
                    tamagotchis[1].State = "Playing";
                    tamagotchis[2].State = "Playing";
                    main.Show();
                    tamagotchis[1].State = "Normal";
                    tamagotchis[2].State = "Normal";
                }
                if (Name == BeastInteractWorm.Name)
                {
                    Interaction main = new Interaction(tamagotchis[2], tamagotchis[0]);
                    tamagotchis[0].State = "Playing";
                    tamagotchis[2].State = "Playing";
                    main.Show();
                    tamagotchis[0].State = "Normal";
                    tamagotchis[2].State = "Normal";
                }

            }
            if (tamagotchis[0].HappinessIndex > 0 && tamagotchis[0].State != "Dead" && tamagotchis[0].State == "Normal")
            {
                if (Name == WormInteractRex.Name)
                {
                    Interaction main = new Interaction(tamagotchis[0], tamagotchis[1]);
                    tamagotchis[0].State = "Playing";
                    tamagotchis[1].State = "Playing";
                    main.Show();
                    tamagotchis[1].State = "Normal";
                    tamagotchis[0].State = "Normal";
                }
                if (Name == WormInteractBeast.Name)
                {
                    Interaction main = new Interaction(tamagotchis[0], tamagotchis[2]);
                    tamagotchis[0].State = "Playing";
                    tamagotchis[2].State = "Playing";
                    main.Show();
                    tamagotchis[2].State = "Normal";
                    tamagotchis[0].State = "Normal";
                }
            }
            //Interaction main = new Interaction(tamagotchis[1], tamagotchis[2]);
            
        }

        //just an exit button
        private void Exit(object sender, RoutedEventArgs e)
        {
            SerializeData();
            this.Close();
            
        }

       /* public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Rex State", Rex.State);
            info.AddValue("Beast State", Beast.State);
            info.AddValue("Worm State", Worm.State);

            throw new NotImplementedException();
        }*/
    }
}
