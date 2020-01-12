using System;
using UnityEngine;
using System.Collections;


namespace ARMathGame
{
    public class GameCharacterController : MonoBehaviour
    {
        private float _speed = 0.2f;

        public void GoToTarget(Animator animator, Vector3 startPosition, Transform character, Flower targetFlower, Action<Flower> callback)
        {
            Vector3 dir = targetFlower.transform.localPosition - character.localPosition;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            float dist;
            float time;

            StartCoroutine(RotateToY(character, angle, 0.1f, null, () =>
            {
                dist = Vector3.Distance(targetFlower.transform.localPosition, character.localPosition);
                time = dist / _speed;

                animator.SetBool("Run", true);
                
                StartCoroutine(MoveTo(character, targetFlower.transform.localPosition, time, 0.86f, null, () =>
                {
                    animator.SetBool("Run", false);
                    animator.SetTrigger("Pickup");

                    StartCoroutine(WaitAndDo(1.95f, () =>
                    {
                        targetFlower.transform.gameObject.SetActive(false);

                        dir = startPosition - character.localPosition;
                        angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

                        StartCoroutine(RotateToY(character, angle, 0.1f, null, () =>
                        {
                            dist = Vector3.Distance(startPosition, character.localPosition);
                            time = dist / _speed;

                            animator.SetBool("Run", true);

                            StartCoroutine(MoveTo(character, startPosition, time, 1.0f, null, () =>
                            {
                                animator.SetBool("Run", false);

                                StartCoroutine(RotateToY(character, 180, 0.1f, null, () =>
                                {
                                    callback?.Invoke(targetFlower);
                                }));
                            }));
                        }));
                    }));
                }));
            }));
        }

        public void Win(Animator animator)
        {
            animator.SetBool("Dance", true);
        }

        public void Lose(Animator animator)
        {
            animator.SetBool("Sad", true);
        }

        public void CorrectAnswer(Animator animator)
        {
            animator.SetTrigger("Correct");
        }
        public void WrongAnswer(Animator animator)
        {
            animator.SetTrigger("Wrong");
        }

        private static IEnumerator MoveTo(Transform obj, Vector3 posEnd, float time, float percent, AnimationCurve curve, Action callback)
        {
            if (curve == null) curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            var posStart = obj.localPosition;
            var timer = 0.0f;
            while (timer < percent)
            {
                timer = Mathf.Min(percent, timer + Time.deltaTime / time);
                var value = curve.Evaluate(timer);
                obj.localPosition = Vector3.Lerp(posStart, posEnd, value);

                yield return null;
            }
            if (callback != null) callback();
        }

        private static IEnumerator RotateToX(Transform obj, float x,
            float time, AnimationCurve curve = null, Action callback = null)
        {
            var rot = obj.localRotation.eulerAngles;
            return RotateTo(obj, new Vector3(x, rot.y, rot.z), time, curve, callback);
        }

        private static IEnumerator RotateToY(Transform obj, float y,
            float time, AnimationCurve curve = null, Action callback = null)
        {     
            var rot = obj.localRotation.eulerAngles;
            return RotateTo(obj, new Vector3(rot.x, y, rot.z), time, curve, callback);
        }

        private static IEnumerator RotateToZ(Transform obj, float z,
            float time, AnimationCurve curve = null, Action callback = null)
        {
            var rot = obj.localRotation.eulerAngles;
            return RotateTo(obj, new Vector3(rot.x, rot.y, z), time, curve, callback);
        }

        private static IEnumerator RotateTo(Transform obj, Vector3 rotEnd, float time,
            AnimationCurve curve = null, Action callback = null)
        {
            if (curve == null) curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            var quatStart = obj.localRotation;
            var quatEnd = Quaternion.Euler(rotEnd);

            var timer = 0.0f;
            while (timer < 1.0f)
            {
                timer = Mathf.Min(1.0f, timer + Time.deltaTime/time);
                var value = curve.Evaluate(timer);
                obj.localRotation = Quaternion.Lerp(quatStart, quatEnd, value);

                yield return null;
            }
            if (callback != null) callback();
        }
        
        private static IEnumerator WaitAndDo(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            if (callback != null) callback();
        }

    }
}
