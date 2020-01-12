using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleARCore;

namespace ARMathGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _planeGenerator = null;
        private DetectedPlaneController _planeGeneratorController = null;

        [SerializeField] private GameCharacterController _gameCharacterController = null;

        [SerializeField] private GameObject _gameCompositionPrefab = null;

        [SerializeField] private GameObject _levelsCompositionPrefab = null;

        [SerializeField] private Image _gradeImageUI = null;
        [SerializeField] private Image _levelImageUI = null;
        [SerializeField] private GameObject _scoreUI = null;
        [SerializeField] private Slider _scoreSliderUI = null;
        [SerializeField] private Text _scoreCorrectAnswersUI = null;
        [SerializeField] private Text _scoreWrongAnswersUI = null;

        [SerializeField] private Button _backToLevelsButtonUI = null;
        [SerializeField] private Button _backToMainManuButtonUI = null;

        [SerializeField] private Sprite _grade_1 = null;
        [SerializeField] private Sprite _grade_2 = null;

        [SerializeField] private List<Sprite> _levelsFirstGrade = null;
        [SerializeField] private List<Sprite> _levelsSecondGrade = null;

        private GameObject _compositionObject = null;

        private GameComposition _gameComposition = null;
        private LevelsComposition _levelsComposition = null;
        
        private int _answer = 15;
        private int _correctAnswers = 0;
        private int _wrongAnswers = 0;
        
        private Anchor _anchor;
        private Pose _pose;
        
        public AudioClip _correctAnswerSound;
        public AudioClip _wrongAnswerSound;
        public AudioClip _endGameSound;
        public AudioClip _backButtonSound;
        public AudioClip _danceSound;
        public AudioClip _sadSound;
        
        void Awake()
        {
            _planeGeneratorController = _planeGenerator.GetComponent<DetectedPlaneController>();
            _planeGeneratorController.OnPlaneTap += OnPlaneTap;

            if (GameData.grade == Grade.First)
            {
                _gradeImageUI.sprite = _grade_1;
            }
            else
            {
                _gradeImageUI.sprite = _grade_2;
            }

            _levelImageUI.gameObject.SetActive(false);
            _scoreUI.gameObject.SetActive(false);
            _backToLevelsButtonUI.gameObject.SetActive(false);

            _backToLevelsButtonUI.onClick.AddListener(OnBackToLevelsButtonClicked);
            _backToMainManuButtonUI.onClick.AddListener(OnBackToMainMenuButtonClicked);
        }

        private void OnPlaneTap(Pose pose)
        {
            _planeGenerator.SetActive(false);

            CreateAnchor(pose);

            _compositionObject = PlaceComposition(_levelsCompositionPrefab);

            _levelsComposition = _compositionObject.GetComponent<LevelsComposition>();

            if (GameData.grade == Grade.First)
            {
                _levelsComposition.Init(_levelsFirstGrade, CreateGameLevel);
            }
            else
            {
                _levelsComposition.Init(_levelsSecondGrade, CreateGameLevel);
            }
        }

        private void CreateAnchor(Pose pose)
        {
            _pose = pose;
            _anchor = Session.CreateAnchor(pose);
        }

        private GameObject PlaceComposition(GameObject compositionPrefab)
        {
            GameObject gameObject = Instantiate(compositionPrefab, _pose.position, Quaternion.identity);

            gameObject.transform.parent = _anchor.transform;

            return gameObject;
        }

        private void CreateGameLevel(Levels level)
        {
            _correctAnswers = 0;
            _wrongAnswers = 0;

            Destroy(_compositionObject);

            _levelsComposition = null;

            if (GameData.grade == Grade.First)
            {
                _levelImageUI.sprite = _levelsFirstGrade[(int)level];
            }
            else
            {
                _levelImageUI.sprite = _levelsSecondGrade[(int)level];
            }

            _levelImageUI.gameObject.SetActive(true);
            _scoreUI.gameObject.SetActive(true);

            _backToLevelsButtonUI.gameObject.SetActive(true);
            _backToMainManuButtonUI.gameObject.SetActive(false);

            _scoreCorrectAnswersUI.text = "0";
            _scoreWrongAnswersUI.text = "0";

            _scoreSliderUI.value = 1;

            _compositionObject = PlaceComposition(_gameCompositionPrefab);

            _gameComposition = _compositionObject.GetComponent<GameComposition>();

            _gameComposition.Init(out _answer, level, 6, _gameCharacterController, OnGetAnswer, BackToLevelsComposition);
        }

        private void OnGetAnswer(Flower flower)
        {
            if (flower.GetAnswer() == _answer)
            {
                _correctAnswers++;

                _gameComposition.CorrectAnswer();

                _scoreCorrectAnswersUI.text = _correctAnswers.ToString();
                
                GetComponent<AudioSource>().PlayOneShot (_correctAnswerSound);
            }
            else
            {
                _wrongAnswers++;

                _gameComposition.WrongAnswer();

                _scoreWrongAnswersUI.text = _wrongAnswers.ToString();
                GetComponent<AudioSource>().PlayOneShot (_wrongAnswerSound);
            }

            _scoreSliderUI.value = (float)_correctAnswers / (float)(_correctAnswers + _wrongAnswers);

            if ((_correctAnswers + _wrongAnswers) == GameData.numberOfAttempts)
            {
                GetComponent<AudioSource>().PlayOneShot (_endGameSound);
                EndGame();
            }
            else
            {
                _gameComposition.ResetFlowers(out _answer);
            }
        }

        private void BackToLevelsComposition()
        {
            Destroy(_compositionObject);

            _gameComposition = null;

            _levelImageUI.gameObject.SetActive(false);
            _scoreUI.gameObject.SetActive(false);

            _backToMainManuButtonUI.gameObject.SetActive(true);
            _backToLevelsButtonUI.gameObject.SetActive(false);

            _compositionObject = PlaceComposition(_levelsCompositionPrefab);

            _levelsComposition = _compositionObject.GetComponent<LevelsComposition>();

            if (GameData.grade == Grade.First)
            {
                _levelsComposition.Init(_levelsFirstGrade, CreateGameLevel);
            }
            else
            {
                _levelsComposition.Init(_levelsSecondGrade, CreateGameLevel);
            }
        }

        private void EndGame()
        {
            if (_correctAnswers > _wrongAnswers)
            {
                if (GameData.grade == Grade.First)
                {
                    GameData.data.firstGradeData[(int)_gameComposition.GetLevel()].complete = true;

                    if (GameData.data.firstGradeData[(int)_gameComposition.GetLevel()].correctAnswers < _correctAnswers)
                    {
                        GameData.data.firstGradeData[(int)_gameComposition.GetLevel()].correctAnswers = _correctAnswers;
                    }

                    SaveSystem.SaveData(GameData.data);
                }
                else
                {
                    GameData.data.secondGradeData[(int)_gameComposition.GetLevel()].complete = true;

                    if (GameData.data.secondGradeData[(int)_gameComposition.GetLevel()].correctAnswers < _correctAnswers)
                    {
                        GameData.data.secondGradeData[(int)_gameComposition.GetLevel()].correctAnswers = _correctAnswers;
                    }

                    SaveSystem.SaveData(GameData.data);
                }

                _gameComposition.DisplayWinUI();
                GetComponent<AudioSource>().PlayOneShot (_danceSound);
            }
            else
            {
                _gameComposition.DisplayLoseUI();
                GetComponent<AudioSource>().PlayOneShot (_sadSound);
            }
        }

        void OnBackToLevelsButtonClicked()
        {
            GetComponent<AudioSource>().PlayOneShot (_backButtonSound);
            BackToLevelsComposition();
        }

        void OnBackToMainMenuButtonClicked()
        {
            GetComponent<AudioSource>().PlayOneShot (_backButtonSound);
            ARMathGame.DetectedPlaneVisualizer.RestGridColor();

            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
