using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public int coinsCounter = 0;
        public GameObject playerGameObject;
        private PlayerController player;
        public GameObject deathPlayerPrefab;
        public Text coinText;

        private Vector3 respawnPoint;

        public AudioClip deathSound;
        public AudioClip backgroundMusic;

        private AudioSource bgmAudioSource;
        private AudioSource sfxAudioSource;

        [Range(0f, 1f)]
        public float bgmVolume = 0.3f;
        [Range(0f, 1f)]
        public float sfxVolume = 1f;

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();

            // จำตำแหน่งเริ่มต้นไว้เป็น Respawn Point
            respawnPoint = playerGameObject.transform.position;

            SetupAudioSources();

            if (backgroundMusic != null && bgmAudioSource != null)
            {
                bgmAudioSource.clip = backgroundMusic;
                bgmAudioSource.loop = true;
                bgmAudioSource.volume = bgmVolume;
                bgmAudioSource.Play();
            }
        }

        void Update()
        {
            coinText.text = coinsCounter.ToString();
            bgmAudioSource.volume = bgmVolume;
            sfxAudioSource.volume = sfxVolume;

            if (player.deathState == true)
            {
                PlaySound(deathSound);

                // เรียก TriggerDeath ก่อน SetActive(false)
                PlayerDeathMarker marker = playerGameObject.GetComponent<PlayerDeathMarker>();
                if (marker != null)
                    marker.TriggerDeath();

                playerGameObject.SetActive(false);

                GameObject deathPlayer = (GameObject)Instantiate(deathPlayerPrefab,
                    playerGameObject.transform.position,
                    playerGameObject.transform.rotation);
                deathPlayer.transform.localScale = playerGameObject.transform.localScale;

                player.deathState = false;
                Invoke("RespawnPlayer", 3);
            }
        }

        private void RespawnPlayer()
        {
            // ลบ Death Animation
            GameObject deathAnim = GameObject.FindWithTag("DeathAnimation");
            if (deathAnim != null) Destroy(deathAnim);

            // ย้าย Player กลับจุดเริ่มต้น
            playerGameObject.transform.position = respawnPoint;
            playerGameObject.SetActive(true);

            // Reset Player State
            player = playerGameObject.GetComponent<PlayerController>();
            player.deathState = false;
        }

        private void SetupAudioSources()
        {
            AudioSource[] sources = GetComponents<AudioSource>();
            if (sources.Length == 0)
            {
                bgmAudioSource = gameObject.AddComponent<AudioSource>();
                sfxAudioSource = gameObject.AddComponent<AudioSource>();
            }
            else if (sources.Length == 1)
            {
                bgmAudioSource = sources[0];
                sfxAudioSource = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                bgmAudioSource = sources[0];
                sfxAudioSource = sources[1];
            }

            bgmAudioSource.playOnAwake = false;
            bgmAudioSource.loop = true;
            sfxAudioSource.playOnAwake = false;
            sfxAudioSource.loop = false;
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip != null && sfxAudioSource != null)
                sfxAudioSource.PlayOneShot(clip);
        }
    }
}