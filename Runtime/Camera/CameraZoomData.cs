using UnityEngine;

namespace Yube.Camera
{
    [CreateAssetMenu(menuName = "Yube/Data/Camera/Zoom")]
    public class CameraZoomData : ScriptableObject
    {
        public float ZoomDelta { get => zoomDelta; }
        public float ZoomIn { get => zoomIn; }
        public float ZoomStay { get => zoomStay; }
        public float ZoomOut { get => zoomOut; }

        [SerializeField]
        private float zoomDelta = 0.0f;
        [SerializeField]
        private float zoomIn = 0.0f;
        [SerializeField]
        private float zoomStay = 0.0f;
        [SerializeField]
        private float zoomOut = 0.0f;
    }
}