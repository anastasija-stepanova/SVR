namespace ARMathGame
{
    public enum Grade
    {
        First,
        Second
    }

    public enum Levels
    {
        Level_1 = 0, Level_2, Level_3, Level_4, Level_5,
        Level_6, Level_7, Level_8, Level_9, Level_10
    }

    public class GameData
    {
        public const int numberOfAttempts = 20;
        public const int numberOfLevels = 10;
        public static Grade grade = Grade.First;

        public static LevelsData data;
    }
}
