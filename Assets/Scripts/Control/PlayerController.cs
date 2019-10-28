using UnityEngine;

//HORIZONTAL - POZIOME
//VERTICAL - PIONOWE
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        public Joystick joystick;
        public float moveSpeed = 5f;
        void Update()
        {
            float sum = VecSum();
            GetComponent<Animator>().SetFloat("speed", sum);
            if(sum > 0.1f)
            {
                gameObject.transform.eulerAngles = 
                    new Vector3(0f, Vector3.SignedAngle(new Vector3(joystick.Horizontal ,0f, joystick.Vertical*(-1)), Vector3.forward, Vector3.up), 0f);

                transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * sum);
            }
        }
        public float VecSum()
        {
            return Mathf.Sqrt(Mathf.Pow(joystick.Vertical, 2) + Mathf.Pow(joystick.Horizontal, 2));
        }
    }
}