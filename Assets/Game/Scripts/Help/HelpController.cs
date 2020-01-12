using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ARMathGame
{
    public class HelpController : MonoBehaviour
    {
        [SerializeField] public Button _backToMainManuButton;
        
        void Awake()
        {
            //_backToMainManuButton = GameObject.Find("BackMainMenuButton").GetComponent<Button>();
            _backToMainManuButton.onClick.AddListener(OnBackToMainMenuButtonClicked);
        }
        
        public void OnBackToMainMenuButtonClicked()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
