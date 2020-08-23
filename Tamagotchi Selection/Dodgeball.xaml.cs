using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Tamagotchis;

namespace Tamagotchi_Selection
{
    /// <summary>
    /// Interaction logic for Dodgeball.xaml
    /// </summary>
    public partial class Dodgeball : Window
    {
        private Tamagotchi temp;
        public Dodgeball(Tamagotchi t)
        {
            InitializeComponent();
            temp = t;
            Init();
            DataContext = this;
            //DispatcherTimer settings and Dispatcher start
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            //adding event
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, millis);
            dispatcherTimer.Start();
        }
        private DispatcherTimer dispatcherTimer;
        private int millis = 2000;
        public int Points;
        public ushort Misses;

        private double[] X = { 100, 300, 500, 100, 300, 500, 100, 300, 500 };
        private double[] Y = { 100, 100, 100, 200, 200, 200, 300, 300, 300 };
        private int square; //tamagotchi location
        private Games.Game game = new Games.Game();
        private void PointTxt() { PointText.Text = $"Points: {game.Points}"; }
        private void MissesTxt() { MissedText.Text = $"Misses: {game.Misses}"; }

        private void Init()
        {
             Points = (int)game.Points;
            Misses = game.Misses;
            millis = 2000;
            Points = 0;
            Misses = 0;
            game.Init();
            ChangePicture();
            PointTxt();
            MissesTxt();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            ChangePicture();
            CommandManager.InvalidateRequerySuggested();
        }


        private void ChangePicture()
        {
            square = game.Move(true) - 1;
 
            Canvas.SetLeft(Picture, X[square]);
            Canvas.SetTop(Picture, Y[square]);
        }

        private void MyCanvas_MouseDown(object sender,
            MouseButtonEventArgs e)
        {

            PointTxt();
            MissesTxt();
            int value = temp.HappinessIndex + game.Pointss();
            double x = e.GetPosition(MyCanvas).X;
            double y = e.GetPosition(MyCanvas).Y;
            int hit = 1;
            if (x >= 100 && x <= 200 && y >= 100 && y < 200) hit = 1;
            else if (x >= 300 && x <= 400 && y >= 100 && y < 200) hit = 2;
            else if (x >= 500 && y <= 600 && y >= 100 && y < 200) hit = 3;
            else if (x >= 100 && x <= 200 && y >= 200 && y < 300) hit = 4;
            else if (x >= 300 && x <= 400 && y >= 200 && y < 300) hit = 5;
            else if (x >= 500 && y <= 600 && y >= 200 && y < 300) hit = 6;
            else if (x >= 100 && x <= 200 && y >= 300 && y < 400) hit = 7;
            else if (x >= 300 && x <= 400 && y >= 300 && y < 400) hit = 8;
            else if (x >= 500 && y <= 600 && y >= 300 && y < 400) hit = 9;
            if (game.IsHit(hit))
            {
                Points = (int)game.Points;
                millis = (Points % 10 == 0) ? millis - 100 : millis;
                ChangePicture();
            }
            else
            {
                Misses++;
                if (game.IsReady())
                {
                    dispatcherTimer.Stop();
                    if (MessageBoxResult.Yes == MessageBox.Show("Do you want to play again?", "Third miss!", MessageBoxButton.YesNo))
                    {
                        temp.HappinessIndex = value;
                        MessageBox.Show(value.ToString());
                        Init();
                        dispatcherTimer.Start();

                    }
                    else
                    {
                        temp.HappinessIndex = value;
                        Close();
                        
                    }
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            int sum = temp.HappinessIndex + game.Pointss();
            temp.HappinessIndex = sum;
            
            this.Close();
        }

    }
}
