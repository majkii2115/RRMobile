using UnityEngine;
using RPG.Resources;

//HORIZONTAL - POZIOME
//VERTICAL - PIONOWE
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject dead;
        protected GameObject enemy = null;
        PlayerControls controls;
        public float moveSpeed = 8f;
        public bool isAlive = true;
        private void Awake() 
        {
            controls = new PlayerControls();
            controls.Gameplay.Attack.performed += ctx => Attack();
        }
        public void Hit()
        {
            if(!enemy.GetComponent<Health>().IsDead())
            enemy.GetComponent<Health>().TakeDamage(gameObject, 20);
        }
        private void OnTriggerStay(Collider other) 
        {
            if(other.gameObject.CompareTag("Enemy") && Input.GetButtonDown("attackButton") && !other.gameObject.GetComponent<Health>().IsDead())
            {
                enemy = other.gameObject;
            } 
            else if(Input.GetButtonDown("attackButton"))
            {
                enemy = null;
                Attack();  
            } 
            else if(other.gameObject.CompareTag("Enemy"))
            {
                enemy = other.gameObject;
            }
        }
        private void OnTriggerExit(Collider other) 
        {
            enemy = null;
        }
        public void Attack()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }
        
        void Update()
        {
            float sum = VecSum();
            GetComponent<Animator>().SetFloat("speed", sum);
            if(sum > 0.1f && isAlive)
            {
                gameObject.transform.eulerAngles = 
                    new Vector3(0f, Vector3.SignedAngle(new Vector3(Input.GetAxis("Horizontal") ,0f, Input.GetAxis("Vertical")*(-1)), Vector3.forward, Vector3.up), 0f);

                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * sum);
            } else if (sum > 0.1 && isAlive && Input.GetButton("sprint"))
            {
                Debug.Log("DZIALA SPRINT");
                transform.Translate(Vector3.forward * Time.deltaTime * (moveSpeed + 5) * sum);
            }
            if(GetComponent<Health>().GetHealthPoint() <= 0)
            {
                isAlive = false;
                dead.SetActive(true);
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