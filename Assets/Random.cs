namespace Tetris
{
    public class Random
    {
        /// <summary>
        /// random selection from 0 to 100 percent
        /// and check for
        /// </summary>
        /// <returns>
        /// selected figure from Figures
        /// </returns>
        public static Figure Select()
        {
            float rnd = UnityEngine.Random.Range(0.0f, 100.0f);
            float LimitLeft = 0.0f;
            float LimitRight = 0.0f;
            int i = 0;
            while (i < Tetris.FigCount)
            {
                LimitLeft = LimitRight;
                LimitRight = LimitLeft + Tetris.Figures[i].probability;
                if (rnd >= LimitLeft & rnd <= LimitRight)
                {
                    return Tetris.Figures[i];
                }
                i++;
            }
            return Tetris.Figures[i - 1];
        }
    }
}
