namespace ARMathGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using GoogleARCore;

    public class LookAtCamera : MonoBehaviour
    {
        void Update()
        {
            Vector3 cameraPosition = new Vector3(Frame.Pose.position.x, Frame.Pose.position.y, Frame.Pose.position.z);

            transform.LookAt(cameraPosition);
        }
    }
}