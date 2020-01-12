using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UnityEngine.EventSystems;


namespace ARMathGame
{
    public class DetectedPlaneController : MonoBehaviour
    {
        [SerializeField] private GameObject _detectedPlanePrefab = null;

        [SerializeField] private Camera _firstPersonCamera = null;

        private List<DetectedPlane> m_NewPlanes = new List<DetectedPlane>();

        public delegate void OnPlaneTapHandler(Pose pose);

        public event OnPlaneTapHandler OnPlaneTap;

        public void Update()
        {
            VisualizeDetectedPlane();

            OnDetectPlaneTap();
        }

        private void VisualizeDetectedPlane()
        {
            if (Session.Status != SessionStatus.Tracking)
            {
                return;
            }

            Session.GetTrackables<DetectedPlane>(m_NewPlanes, TrackableQueryFilter.New);
            for (int i = 0; i < m_NewPlanes.Count; i++)
            {
                GameObject planeObject =
                    Instantiate(_detectedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
                planeObject.GetComponent<DetectedPlaneVisualizer>().Initialize(m_NewPlanes[i]);
            }
        }

        private void OnDetectPlaneTap()
        {
            Touch touch;

            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                if (!((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(_firstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0))
                {
                    OnPlaneTap?.Invoke(hit.Pose);
                }
            }
        }
    }
}
