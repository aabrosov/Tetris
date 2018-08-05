using UnityEngine;
using System.Collections.Generic;

namespace Tetris
{
    /// <summary>
    /// class for create all figures
    /// and select one with provided possibility
    /// </summary>
    public class Figures
    {
        List<Tetramino> AllFigs;

        /// <summary>
        /// constructor for all tetraminos
        /// </summary>
        /// <param name="Mode">if mode=2 it's extended mode with more figures</param>
        public Figures(int Mode)
        {
            AllFigs = new List<Tetramino>();
            AllFigs.Add(new TetraminoO());
            AllFigs.Add(new TetraminoL());
            AllFigs.Add(new TetraminoJ());
            AllFigs.Add(new TetraminoS());
            AllFigs.Add(new TetraminoZ());
            AllFigs.Add(new TetraminoI());
            AllFigs.Add(new TetraminoT());

            if (Mode == 2)
            {
                AllFigs[6].probability = 5;
                AllFigs.Add(new TetraminoX());
                AllFigs.Add(new TetraminoU());
                AllFigs.Add(new TetraminoW());
            }
        }

        /// <summary>
        /// select tetramino with provided possibility
        /// </summary>
        /// <returns>selected figure</returns>
        public Tetramino Select()
        {
            float rnd = Random.Range(0.0f, 100.0f);
            float LimitLeft = 0.0f;
            float LimitRight = 0.0f;
            foreach(Tetramino tmino in AllFigs)
            {
                LimitLeft = LimitRight;
                LimitRight = LimitLeft + tmino.probability;
                if (rnd >= LimitLeft & rnd <= LimitRight)
                {
                    return tmino;
                }
            }
            return AllFigs[0];
        }
    }
}
