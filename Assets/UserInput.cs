using UnityEngine;

namespace Tetris
{
    public class UserInput
    {
        private static float CurrentTime = 0;
        private static float FallSpeed = 1;

        /// <summary>
        /// this function will read user input
        /// Down Left Right Arrows = Move
        /// Up Arrow and l = Left rotate
        /// r = Right rotate
        /// </summary>
        /// <returns>
        /// string with recieved command
        /// </returns>
        public static string CheckUserInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.L))
            {
                return "RotateLeft";
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                return "RotateRight";
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                return "MoveDown";
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return "MoveLeft";
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                return "MoveRight";
            }
            else if (Time.time - CurrentTime >= FallSpeed)
            {
                CurrentTime = Time.time;
                return "FallDown";
            }
            return "";
        }
    }
}
