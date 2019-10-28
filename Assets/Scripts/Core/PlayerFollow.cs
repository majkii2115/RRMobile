using UnityEngine;

namespace RPG.Core
{
    public class PlayerFollow : MonoBehaviour 
    {
        public Transform playerTransform;
        private Vector3 cameraOffset;
        [Range(0.01f, 1f)] public float smoothFactor = 0.5f;
        public bool lookAtPlayer = false;
        public bool shouldRotate = true;
        public float rotationSpeed = 5f;
        private void Start() 
        {
            cameraOffset = transform.position - playerTransform.position;
        }
        private void Update() 
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase = TouchPhase.Moved)
                {
                    playerTransform.rotation.y =  
                }
            }   
        }
        private void LateUpdate() 
        {
            if(shouldRotate)
            {
                Quaternion cameraTurningAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                cameraOffset = cameraTurningAngle * cameraOffset;
            }
            Vector3 newPos = playerTransform.position + cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
            if(lookAtPlayer || shouldRotate)
            {
                transform.LookAt(playerTransform);
            }
        }
    
    }
}
