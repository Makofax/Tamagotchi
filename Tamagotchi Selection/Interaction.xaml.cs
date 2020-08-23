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
using System.Windows.Shapes;
using System.Windows.Threading;
using Tamagotchis;
using Games;

namespace Tamagotchi_Selection
{
    /// <summary>
    /// Interaction logic for Interaction.xaml
    /// </summary>
    public partial class Interaction : Window
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }

        }
        private string Path = "../Resources/";
        private DispatcherTimer dispatcherTimer;
        private Tamagotchi temp1; private Tamagotchi temp2;
        public Interaction(Tamagotchi o, Tamagotchi t)
        {
            temp1 = o;
            temp2 = t;
            InitializeComponent();
            Init();
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            //adding event
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }
        private void Init()
        {
            
            if ( temp2.name.ToString() == "Beast")
            {
                Player2img.RenderTransformOrigin = new Point(0.5, 0.5);
                ScaleTransform flipTrans = new ScaleTransform();
                flipTrans.ScaleX = -1;
                //flipTrans.ScaleY = -1;
                Player2img.RenderTransform = flipTrans;
            }
            if (temp1.name.ToString() == "Rex" || temp1.name.ToString() == "Worm")
            {
                Player1img.RenderTransformOrigin = new Point(0.5, 0.5);
                ScaleTransform flipTrans = new ScaleTransform();
                flipTrans.ScaleX = -1;
                //flipTrans.ScaleY = -1;
                Player1img.RenderTransform = flipTrans;
            }
            Player1img.Source = new BitmapImage(new Uri(String.Concat(Path, temp1.name.ToString() + ".png"), UriKind.Relative));
            Player2img.Source = new BitmapImage(new Uri(String.Concat(Path, temp2.name.ToString() + ".png"), UriKind.Relative));
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            int roll1 = RandomNumber(0, 3);
            int roll2 = RandomNumber(0, 3);
            Player1hand(roll1);
            Player2hand(roll2);
           

        }
        private void Player1hand(int roll)
        {
            if (roll == 0)
            {
                temp1.Hand = "rock";
                Player1.Source = new BitmapImage(new Uri(String.Concat(Path, "rock.jpg"), UriKind.Relative));
            }
            else if (roll == 1)
            {
                temp1.Hand = "scissors";
                Player1.Source = new BitmapImage(new Uri(String.Concat(Path, "scissors.jpg"), UriKind.Relative));
            }
            else if (roll == 2)
            {
                temp1.Hand = "paper";
                Player1.Source = new BitmapImage(new Uri(String.Concat(Path, "paper.jpg"), UriKind.Relative));
            }
        }
        private void Player2hand(int roll)
        {
            if (roll == 0)
            {
                temp2.Hand = "rock";
                Player2.Source = new BitmapImage(new Uri(String.Concat(Path, "rock.jpg"), UriKind.Relative));
            }
            else if (roll == 1)
            {
                temp2.Hand = "scissors";
                Player2.Source = new BitmapImage(new Uri(String.Concat(Path, "scissors.jpg"), UriKind.Relative));
            }
            else if (roll == 2)
            {
                temp2.Hand = "paper";
                Player2.Source = new BitmapImage(new Uri(String.Concat(Path, "paper.jpg"), UriKind.Relative));
            }
        }
        private void Play_Button_Click(object sender, EventArgs e)
        {
            if (temp1.Hand == "rock" && temp2.Hand == "rock" )
            {
                dispatcherTimer.Stop();
                Result.Text = $"It is Draw";
            }
            else if (temp1.Hand == "rock" && temp2.Hand == "paper")
            {
                dispatcherTimer.Stop();
                Result.Text = $"{temp2.name.ToString()} Wins";
                temp2.HappinessIndex += 2;
                temp1.HappinessIndex--;
            }
            else if (temp1.Hand == "rock" && temp2.Hand == "scissors")
            {
                dispatcherTimer.Stop();
                Result.Text = $"{temp1.name.ToString()} Wins, rock breaks scissors";
                temp1.HappinessIndex += 2;
                temp2.HappinessIndex--;
            }
            else if (temp1.Hand == "paper" && temp2.Hand == "rock")
            {
                dispatcherTimer.Stop();
                Result.Text = $"{temp1.name.ToString()} Wins, paper covers rock";
                temp1.HappinessIndex += 2;
                temp2.HappinessIndex--;
            }
            else if (temp1.Hand == "scissors" && temp2.Hand == "rock")
            {
                dispatcherTimer.Stop();
                Result.Text = $"{temp2.name.ToString()} Wins, rock breaks scissors";
                temp2.HappinessIndex += 2 ;
                temp1.HappinessIndex--;
            }
            else if (temp1.Hand == "paper" && temp2.Hand == "paper")
            {
                dispatcherTimer.Stop();
                Result.Text = $"It is Draw";
            }
            else if (temp1.Hand == "scissors" && temp2.Hand == "scissors")
            {
                dispatcherTimer.Stop();
                Result.Text = $"It is Draw";
            }
            else if (temp1.Hand == "scissors" && temp2.Hand == "paper")
            {
                dispatcherTimer.Stop();
                Result.Text = $"{temp1.name.ToString()} Wins, scissors cut paper";
                temp1.HappinessIndex += 2;
                temp2.HappinessIndex--;
            }
            else if (temp1.Hand == "paper" && temp2.Hand == "scissors")
            {
                dispatcherTimer.Stop();
                Result.Text = $"{temp2.name.ToString()} Wins, scissors cut paper";
                temp2.HappinessIndex += 2;
                temp1.HappinessIndex--;
            }
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            
            this.Close();

        }
    }
}
