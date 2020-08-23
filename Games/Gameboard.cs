using System;

namespace Games
{
    internal class GameBoard
    {
        private bool[] squares = new bool[9];

        /// <summary>        
        /// checkes if the given squar is empty (false) or has a tamagotchi (true)
        /// </summary>
        /// <param name="square">0..8</param>
        /// <returns>true = hit tamagotchi, false = empty sqare</returns>
        internal bool IsHit(int square)
        {
            return (squares[square]) ? true : false;
        }

        /// <summary>        
        /// empties board, does not create new
        /// </summary>
        internal void Init()
        {
            for (int i = 0; i < 9; i++)
            {
                squares[i] = false;
            }
        }

        /// <summary>        
        /// places tamagotchi (true) into given square 
        /// </summary>
        /// <param name="square">0..8</param>
        internal void Move(int square)
        {
            squares[square] = true;
        }
    }
}