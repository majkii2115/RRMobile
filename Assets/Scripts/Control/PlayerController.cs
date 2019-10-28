using RPG.Movement;
using UnityEngine;
using RPG.Resources;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;

//HORIZONTAL - POZIOME
//VERTICAL - PIONOWE

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        protected Joystick joystick;
        public float moveSpeed = 5f;
        private void Start() 
        {
            joystick = FindObjectOfType<Joystick>();
        }
        void Update()
        {
            float sum = Mathf.Sqrt(Mathf.Pow(joystick.Vertical, 2) + Mathf.Pow(joystick.Horizontal, 2));
            GetComponent<Animator>().SetFloat("speed", sum);
            if(sum > 0.1f)
            {
                gameObject.transform.eulerAngles = new Vector3(0f, Vector3.SignedAngle(new Vector3(joystick.Horizontal*(-1) ,0f, joystick.Vertical), Vector3.forward, Vector3.up), 0f);
                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * sum);
            }
    
        }
    }
}