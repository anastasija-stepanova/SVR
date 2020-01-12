using System;
using UnityEngine;

namespace ARMathGame
{
    public class GameCharacter : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;

        private Vector3 _startPosition;

        void Start()
        {
            _startPosition = transform.localPosition;
        }

        private void OnCollisionEnter(Collision other)
        {
            print(other.gameObject.name);
        }

        public Vector3 GetPosition()
        {
            return _startPosition;
        }

        public Animator GetAnimator()
        {
            return _animator;
        }
    }
}
