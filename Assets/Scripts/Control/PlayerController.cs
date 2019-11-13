using UnityEngine;
using RPG.Combat;
using UnityEngine.InputSystem;
using RPG.Resources;

//HORIZONTAL - POZIOME
//VERTICAL - PIONOWE
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject activator;
        PlayerControls controls;
        public float moveSpeed = 5f;
        public bool isAlive = true;
        private void Awake() 
        {
            controls = new PlayerControls();
            controls.Gameplay.Attack.performed += ctx => Attack();
        }
        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.CompareTag("Enemy") && Input.GetButtonDown("attackButton"))
            {
                other.GetComponent<simpleHealth>().Damage();
            } 
            else
            {
                Attack();  
            }      
        }
        public void Attack()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }
        void Update()
        {
            // if(Input.GetButtonDown("attackButton"))
            // {
            //     Attack();
            // }
            float sum = VecSum();
            GetComponent<Animator>().SetFloat("speed", sum);
            if(sum > 0.1f && isAlive)
            {
                gameObject.transform.eulerAngles = 
                    new Vector3(0f, Vector3.SignedAngle(new Vector3(Input.GetAxis("Horizontal") ,0f, Input.GetAxis("Vertical")*(-1)), Vector3.forward, Vector3.up), 0f);

                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * sum);
            }
            if(GetComponent<Health>().GetHealthPoint() <= 0)
            {
                isAlive = false;
            }
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