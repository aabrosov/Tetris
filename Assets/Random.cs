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
            while (i < Game.FigCount)
            {
                LimitLeft = LimitRight;
                LimitRight = LimitLeft + Game.Figures[i].probability;
                if (rnd >= LimitLeft & rnd <= LimitRight)
                {
                    return Game.Figures[i];
                }
                i++;
            }
            return Game.Figures[i - 1];
        }
    }
}
