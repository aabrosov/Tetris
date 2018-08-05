using UnityEngine;

namespace Tetris
{
    public class UserInput : Tetramino
    {
        private static float CurrentTime = 0;
        private static float FallSpeed = 1;

        public string CheckUserInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.L))
            {
                return "RotateLeft";
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                return "RotateRight";
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return "MoveLeft";
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                return "MoveRight";
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - CurrentTime >= FallSpeed)
            {
                CurrentTime = Time.time;
                return "MoveDown";
            }
            return "DoNothing";
        }
    }
}
