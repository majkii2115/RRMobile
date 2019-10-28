using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class XPDisplay : MonoBehaviour 
    {
        Experience xp;
        private void Awake() 
        {
            xp = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }
        private void Update() 
        {
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", xp.GetPoints(), xp.GetMaxXP());  
        }
    }
}