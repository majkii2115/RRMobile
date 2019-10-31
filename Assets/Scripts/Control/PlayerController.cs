using UnityEngine;
using RPG.Combat;
using UnityEngine.InputSystem;

//HORIZONTAL - POZIOME
//VERTICAL - PIONOWE
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        PlayerControls controls;
        // public Joystick joystick;
        // joystick.Horizontal
        // joystick.Vertical
        public float moveSpeed = 5f;
        private void Awake() 
        {
            controls = new PlayerControls();
            controls.Gameplay.Attack.performed += ctx => Attack();
        }
        void Update()
        {
            float sum = VecSum();
            GetComponent<Animator>().SetFloat("speed", sum);
            if(sum > 0.1f)
            {
                gameObject.transform.eulerAngles = 
                    new Vector3(0f, Vector3.SignedAngle(new Vector3(Input.GetAxis("Horizontal") ,0f, Input.GetAxis("Vertical")*(-1)), Vector3.forward, Vector3.up), 0f);

                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * sum);
            }
        }
        void Attack()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }
        public float VecSum()
        {
            return Mathf.Sqrt(Mathf.Pow(Input.GetAxis("Vertical"), 2) + Mathf.Pow(Input.GetAxis("Horizontal"), 2));
        }
        void OnEnable() 
        {
            controls.Gameplay.Enable();    
        }
        void OnDisable() 
        {
            controls.Gameplay.Disable();    
        }
    }
}