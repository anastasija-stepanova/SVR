namespace ARMathGame
{
    using System;
    using System.Collections.Generic;
    using UnityEngine.EventSystems;
    using UnityEngine;
    using UnityEngine.UI;

    public class Test : MonoBehaviour
    {
        public Slider slider;

        void Start()
        {
            float score;

            int _correctAnswers = 10;
            int _wrongAnswers = 5;

            if (_correctAnswers == 0)
            {
                score = 0.0f;

                Debug.Log("score zero");
            }
            else
            {
                score = (float)_correctAnswers / (float)(_correctAnswers + _wrongAnswers);

                Debug.Log("score not zero");
                Debug.Log(score);
            }

            Debug.Log(score);

            slider.value = score;
        }

        void Update()
        {
        }
    }
}