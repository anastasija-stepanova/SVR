using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARMathGame
{
    public class GameComposition : MonoBehaviour
    {
        [SerializeField] private Terrain _terrain = null;
        [SerializeField] private GameCharacter _gameCharacter = null;
        private GameCharacterController _gameCharacterController = null;

        [SerializeField] private Text _equationText = null;
        [SerializeField] private Text _winText = null;
        [SerializeField] private Text _loseText = null;
        [SerializeField] private Button _toLevelsButton = null;

        [SerializeField] private List<GameObject> _flowerObjects = null;

        private float _terrainWidth;
        private float _terrainLength;
        private float _terrainPosX;
        private float _terrainPosZ;

        private Levels _level;
        private int _amountFlowers;

        private string _equation = null;

        private List<Flower> _flowers = new List<Flower>();

        private Action<Flower> _callback;
        private Action _backToLevels;

        void Start()
        {
            _winText.gameObject.SetActive(false);
            _loseText.gameObject.SetActive(false);

            _toLevelsButton.gameObject.SetActive(false);
            _toLevelsButton.GetComponent<Button>().onClick.AddListener(OnToLevelsCompositionButtonClicked);
        }

        public void Init(out int answer, Levels level, int amountFlowers, GameCharacterController gameCharacterController, Action<Flower> callback, Action backToLevels)
        {
            _level = level;
            _backToLevels = backToLevels;
            _callback = callback;
            _amountFlowers = amountFlowers;
            _gameCharacterController = gameCharacterController;

            _terrainWidth = _terrain.terrainData.size.x;
            _terrainLength = _terrain.terrainData.size.z;
            _terrainPosX = _terrain.transform.position.x;
            _terrainPosZ = _terrain.transform.position.z;

            ResetFlowers(out answer);
        }

        public void ResetFlowers(out int answer)
        {
            DeleteFlowers();

            LevelEquation.GetEquation(GameData.grade, _level, out _equation, out answer);

            _equationText.text = _equation;

            GameObject flowerObj;
            Flower flower;

            for (int i = 0; i < _amountFlowers; ++i)
            {
                Vector3 position = GetRandomPlanePos();

                flowerObj = Instantiate(_flowerObjects[(int)_level], position, Quaternion.identity);

                flowerObj.transform.parent = this.transform;

                flower = flowerObj.GetComponent<Flower>();

                flower.Init(LevelEquation.GetAnswerVariant(GameData.grade, _level), OnFlowerClick);

                _flowers.Add(flower);
            }

            _flowers[UnityEngine.Random.Range(0, _flowers.Count)].Init(answer, OnFlowerClick);
        }

        private void DeleteFlowers()
        {
            while (_flowers.Count != 0)
            {
                Destroy(_flowers[0].gameObject);
                _flowers.RemoveAt(0);
            }
        }

        public Vector3 GetRandomPlanePos()
        {
            float posX = UnityEngine.Random.Range(_terrainPosX + 0.1f, (_terrainPosX + _terrainWidth) - 0.1f);
            float posZ = UnityEngine.Random.Range(_terrainPosZ + 0.1f, (_terrainPosZ + _terrainLength) - 0.1f);
            
            Vector3 position = new Vector3(posX, this.transform.position.y, posZ);

            return position;
        }

        private void OnFlowerClick(Flower flower)
        {
            _gameCharacterController.GoToTarget(_gameCharacter.GetAnimator(), _gameCharacter.GetPosition(), _gameCharacter.transform, flower, _callback);
        }

        private void OnToLevelsCompositionButtonClicked()
        {
            _backToLevels?.Invoke();
        }

        public Levels GetLevel()
        {
            return _level;
        }

        public void CorrectAnswer()
        {
            _gameCharacterController.CorrectAnswer(_gameCharacter.GetAnimator());
        }
        public void WrongAnswer()
        {
            _gameCharacterController.WrongAnswer(_gameCharacter.GetAnimator());
        }

        public void DisplayWinUI()
        {
            _equationText.gameObject.SetActive(false);
            _winText.gameObject.SetActive(true);
            _toLevelsButton.gameObject.SetActive(true);

            _gameCharacterController.Win(_gameCharacter.GetAnimator());
        }

        public void DisplayLoseUI()
        {
            _equationText.gameObject.SetActive(false);
            _loseText.gameObject.SetActive(true);
            _toLevelsButton.gameObject.SetActive(true);

            _gameCharacterController.Lose(_gameCharacter.GetAnimator());
        }
    }
}
