using System;
using System.Collections.Generic;
using UnityEngine;

namespace ARMathGame
{
    public class LevelsComposition : MonoBehaviour
    {
        [SerializeField] private Material _redMaterial = null;
        [SerializeField] private Material _whiteMaterial = null;
        [SerializeField] private Material _grayMaterial = null;

        [SerializeField] private List<Level> _levels = new List<Level>();

        private Action<Levels> _callback;

        public void Init(List<Sprite> levelImages, Action<Levels> callback)
        {
            _callback = callback;

            bool previousComplete = true;

            if (GameData.grade == Grade.First)
            {
                foreach (LevelData levelData in GameData.data.firstGradeData)
                {
                    InitFlower(levelData, levelImages[(int)levelData.level], previousComplete);

                    previousComplete = levelData.complete;
                }
            }
            else
            {
                foreach (LevelData levelData in GameData.data.secondGradeData)
                {
                    InitFlower(levelData, levelImages[(int)levelData.level], previousComplete);

                    previousComplete = levelData.complete;
                }
            }
        }

        private void InitFlower(LevelData levelData, Sprite leveImage, bool previousComplete)
        {
            Material material;

            if (levelData.complete == true)
            {
                material = _redMaterial;
            }
            else if (previousComplete == true && levelData.complete == false)
            {
                material = _whiteMaterial;
            }
            else
            {
                material = _grayMaterial;
            }

            _levels[(int)levelData.level].Init(levelData.correctAnswers, leveImage, levelData.complete, material, OnLevelClick);
        }

        private void OnLevelClick(Levels level)
        {
            if (level != Levels.Level_1)
            {
                if (_levels[(int)level - 1].GetComplete() == true)
                {
                    _callback?.Invoke(level);
                }
            }
            else
            {
                _callback?.Invoke(level);
            }
        }
    }
}
