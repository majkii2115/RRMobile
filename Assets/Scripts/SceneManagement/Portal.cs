using System.Collections;
using RPG.Control;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour 
    {
        enum DestinationID
        {
            A, B, C, D
        }
        [SerializeField] int nextScene = 1;
        [SerializeField] Transform spawnPoint = null; 
        [SerializeField] DestinationID destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 1f;
        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player")
            {
                Debug.Log("triggered PORTAL");
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (nextScene < 0) 
            {
                Debug.Log("scene not found!");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            Fader fader = FindObjectOfType<Fader>();
            player.enabled = false;

            yield return fader.FadeOut(fadeOutTime);
            wrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(nextScene);
            PlayerController newPlayer = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newPlayer.enabled = false;
            wrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayerLocation(otherPortal);
            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);
            newPlayer.enabled = true;
            Destroy(gameObject);
        }

        private void UpdatePlayerLocation(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }

            return null;
        }
    }
}