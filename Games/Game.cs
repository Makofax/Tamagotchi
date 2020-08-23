using System;

namespace Games
{
    public class Game
    {
        private GameBoard board = new GameBoard();
        private uint points;
        private ushort misses;
        private Random random = new Random();

        public uint Points { get => points; }
        public ushort Misses { get => misses; }

        /// <summary>
        /// checks by asking the board if the given square contains a tamagotchi
        /// updates points or misses according to the answer
        /// </summary>
        /// <param name="square">1..9</param>
        /// <returns>true = hits tamagotchi, false = empty square</returns>
        public bool IsHit(int square)
        {
            bool result = false;
            if (board.IsHit(square - 1))
            {
                points++;
                result = true;
            }
            else
            {
                misses++;
            }
            return result;
        }
        public int Pointss()
        {
            int TotalPoints = 1;

            if (Misses >= 3)
            {
                TotalPoints = TotalPoints - 10;
            }
            else if ((Points > 0) && (Misses < 3))
            {
                double Val = Math.Floor(Points / 10.0);
                int sw = Convert.ToInt32(Val);
                TotalPoints = TotalPoints + sw;
            }
            return TotalPoints;
        }
        /// <summary>
        /// starts a new game with empty board and tamagotchi in a random location
        /// points and misses resetted 
        /// </summary>
        public void Init()
        {
            points = 0;
            misses = 0;
            board.Init();
            Move();
        }

        /// <summary>
        /// places tamagotchi into new random location
        /// </summary>
        public void Move()
        {
            //empty board
            board.Init();
            //random move between 0..8 (numbers are not visible in API)
            board.Move(random.Next(9));
        }
        
        /// <summary>
        /// alternative Move that returns the move
        /// </summary>
        /// <param name="isReturn">true if called</param>
        /// <returns></returns>
        public int Move(bool isReturn = false )
        {
            //empty board
            board.Init();
            //random move 0..8 (numbering not visible outside API)
            int square = random.Next(9);
            board.Move(square);
            return square + 1; //Dodgeball knows squares 1..9
        }

        /// <summary>
        /// checkes if the game is over
        /// </summary>
        /// <returns>true = over, false = continue</returns>
        public bool IsReady()
        {
            return misses == 3 ? true : false;
        }
    }
}
