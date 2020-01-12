namespace ARMathGame
{
    [System.Serializable]
    public class LevelData
    {
        public Levels level;
        public bool complete;
        public int correctAnswers;

        public LevelData(Levels level)
        {
            this.level = level;
            complete = false;
            correctAnswers = 0;
        }
    }
}
