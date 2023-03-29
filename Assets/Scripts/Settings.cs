
using System.Collections.Generic;
namespace Settings

{
    /* global vars */
    public enum GameProgress { HowToPlay, GameOver };

    public static class Messages
    {
        public static Dictionary<GameProgress, string[]> MessageDict = new Dictionary<GameProgress, string[]>
        {
            {
                GameProgress.HowToPlay, new string[] {"Use Arrow Keys to move Platforms","Use Spacebar to Jump","While falling, move Left and Right with Arrow Keys"}
            },
            {
                GameProgress.GameOver, new string[] {"Game Over"}
            }
        };
    }

}