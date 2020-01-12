using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace ARMathGame
{
	public class Flower : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private Text _answerText = null;

		private int _answer;
		private Action<Flower> _onClick;

        private static bool _listenOnFlowerClick;
        
        void Start()
        {
            _listenOnFlowerClick = true;
        }

        public void Init(int answer, Action<Flower> onClick)
		{
			_answer = answer;
            _answerText.text = answer.ToString();

            _onClick = onClick;
        }

        public int GetAnswer()
        {
            return _answer;
        }

		public void OnPointerClick(PointerEventData eventData)
		{
            if (_listenOnFlowerClick)
            {
                _onClick?.Invoke(this);

                _listenOnFlowerClick = false;
            }
        }
	}
}
