using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ARMathGame
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _firstGradeButton = null;
        [SerializeField] private GameObject _secondGradeButton = null;
        [SerializeField] private GameObject _resetGameButton = null;
        [SerializeField] private GameObject _helpGameButton = null;
        
        public AudioClip _mainMenuSound;
        public AudioClip _selectLevelSound;

        void Awake()
        {
            GameData.data = SaveSystem.LoadData();

            if (GameData.data == null)
            {
                SaveSystem.CreateSave(out GameData.data);
            }

            _firstGradeButton.GetComponent<Button>().onClick.AddListener(OnFirstGradeButtonClicked);
            _secondGradeButton.GetComponent<Button>().onClick.AddListener(OnSecondGradeButtonClicked);
            _resetGameButton.GetComponent<Button>().onClick.AddListener(OnResetGameButtonClicked);
            _helpGameButton.GetComponent<Button>().onClick.AddListener(OnHelpGameButtonClicked);
            
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().clip = _mainMenuSound;
            GetComponent<AudioSource>().Play();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        void OnFirstGradeButtonClicked()
        {
            GameData.grade = Grade.First;
            GetComponent<AudioSource>().PlayOneShot (_selectLevelSound);
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        void OnSecondGradeButtonClicked()
        {
            GameData.grade = Grade.Second;
            GetComponent<AudioSource>().PlayOneShot (_selectLevelSound);    
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        void OnResetGameButtonClicked()
        {
            GetComponent<AudioSource>().PlayOneShot (_selectLevelSound);
            SaveSystem.DeleteSave(out GameData.data);
        }

        void OnHelpGameButtonClicked()
        {
            GetComponent<AudioSource>().PlayOneShot (_selectLevelSound);
            SceneManager.LoadScene("Help", LoadSceneMode.Single);
        }
    }
}
