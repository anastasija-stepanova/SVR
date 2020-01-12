using System.Collections.Generic;

namespace ARMathGame
{
    [System.Serializable]
    public class LevelsData
    {
        public List<LevelData> firstGradeData = new List<LevelData>();
        public List<LevelData> secondGradeData = new List<LevelData>();

        public LevelsData()
        {
            for (int i = 0; i < GameData.numberOfLevels; i++)
            {
                firstGradeData.Add(new LevelData((Levels)i));
                secondGradeData.Add(new LevelData((Levels)i));
            }
        }
    }

}
