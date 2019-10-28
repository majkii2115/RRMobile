using UnityEngine;

namespace RPG.Core
{
    public class CameraController2 : MonoBehaviour 
    {
        public Transform playerTransform;
        private Vector3 cameraOffset;
        [Range(0.01f, 1f)] public float smoothFactor = 0.5f;
        public bool lookAtPlayer = false;
        public bool shouldRotate = true;
        private void Start() 
        {
            cameraOffset = transform.position - playerTransform.position;
        }
        private void LateUpdate() 
        {
            Vector3 newPos = playerTransform.position + cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
            if(lookAtPlayer || shouldRotate)
            {
                transform.LookAt(playerTransform);
            }
        }
    }
}