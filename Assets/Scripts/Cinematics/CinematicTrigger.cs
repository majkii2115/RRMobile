﻿using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        static bool alreadyTriggered = false;

        private void OnTriggerEnter(Collider other) 
        {
            if(alreadyTriggered) return;
            if(!alreadyTriggered && other.gameObject.tag == "Player")
            {
            GetComponent<PlayableDirector>().Play();
            alreadyTriggered = true;
            }
        }
        public object CaptureState()
        {
            return alreadyTriggered;
        }

        public void RestoreState(object state)
        {
            alreadyTriggered = (bool)state;
        }
    }

}