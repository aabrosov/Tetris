using UnityEngine;
namespace Tetris
{
    public class RandomGenerator
    {
        public static int Generate()
        {
            float rnd = Random.Range(0.0f, 100.0f);
            float LimitLeft = 0.0f;
            float LimitRight = 0.0f;
            for (int i = 0; i < NewBehaviourScript.FigCount; i++)
            {
                LimitLeft = LimitRight;
                LimitRight = LimitLeft + Tiles.Probabilities[i];
                if (rnd > LimitLeft & rnd < LimitRight)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
