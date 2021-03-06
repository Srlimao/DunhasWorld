﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using RPG.Control;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] 
        public enum PortalId{
            A,B,C,D
        }

        [SerializeField] int sceneIndexToLoad;
        [SerializeField] public Transform spawnPoint;
        [SerializeField] public PortalId portalId = PortalId.A;
        [SerializeField] float fadeOutTime, fadeInTime;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper saver = FindObjectOfType<SavingWrapper>();

            DontDestroyOnLoad(this.gameObject);

            yield return fader.FadeOut(fadeOutTime);
            saver.Save();
            yield return SceneManager.LoadSceneAsync(sceneIndexToLoad);
            saver.Load();
            

            Portal otherPortal = GetOtherPortal();

            UpdatePlayerSpawn(otherPortal);

            saver.Save();
            yield return new WaitForSeconds(1f);
            yield return fader.FadeIn(fadeInTime);
            




            Destroy(this.gameObject);
            
        }

        private void UpdatePlayerSpawn(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //PlayerController p = FindObjectOfType<PlayerController>();
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            Portal p = null;
            foreach(GameObject g in gameObjects)
            {
                if (g.TryGetComponent<Portal>(out p) && p.portalId==this.portalId) break;
                
            }
            return p;
        }
    }
}
