using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ARMathGame
{
    public class Level : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Levels _level = 0;
        [SerializeField] private GameObject _bouquet = null;
        [SerializeField] private Renderer _renderer = null;

        [SerializeField] private Image _levelImage = null;
        [SerializeField] private Text _correctAnswersUI = null;
        [SerializeField] private Text _wrongAnswersUI = null;
        [SerializeField] private Slider _scoreSliderUI = null;

        private bool _complete = false;

        private Action<Levels> _onClick;

        //void Awake()
        //{
         
        //}

        public void Init(int correctAnswers, Sprite levelImage, bool complete, Material material,Action<Levels> onClick)
        {
            _correctAnswersUI.text = correctAnswers.ToString();
            _wrongAnswersUI.text = correctAnswers == 0 ? 0.ToString() : (GameData.numberOfAttempts - correctAnswers).ToString();

            _scoreSliderUI.value = correctAnswers  == 0 ? 1 : ((float)correctAnswers / (float)GameData.numberOfAttempts);

            _levelImage.sprite = levelImage;
            _complete = complete;
            _renderer.material = material;
            _onClick = onClick;

            if (_complete == true)
            {
                _bouquet.SetActive(true);
            }
            else
            {
                _bouquet.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick?.Invoke(_level);
        }

        public bool GetComplete()
        {
            return _complete;
        }

        public Levels GetLevel()
        {
            return _level;
        }
    }
}
