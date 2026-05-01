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

        // Sound Effects
        public AudioClip deathSound;
        public AudioClip backgroundMusic;

        // Separate Audio Sources
        private AudioSource bgmAudioSource;      // For background music
        private AudioSource sfxAudioSource;      // For sound effects

        [Range(0f, 1f)]
        public float bgmVolume = 0.3f;           // BGM volume control (adjustable in inspector)
        [Range(0f, 1f)]
        public float sfxVolume = 1f;             // SFX volume control

        void Start()
        {
            player = GameObject.Find("Player").GetComponent<PlayerController>();

            // Setup Audio Sources
            SetupAudioSources();

            // Play background music on loop
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

            // Allow runtime volume adjustment
            bgmAudioSource.volume = bgmVolume;
            sfxAudioSource.volume = sfxVolume;

            if (player.deathState == true)
            {
                // Play death sound on SFX channel
                PlaySound(deathSound);

                playerGameObject.SetActive(false);
                GameObject deathPlayer = (GameObject)Instantiate(deathPlayerPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
                deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
                player.deathState = false;
                Invoke("ReloadLevel", 3);
            }
        }

        private void SetupAudioSources()
        {
            // Get or create BGM AudioSource
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

            // Configure BGM
            bgmAudioSource.playOnAwake = false;
            bgmAudioSource.loop = true;

            // Configure SFX
            sfxAudioSource.playOnAwake = false;
            sfxAudioSource.loop = false;
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Play sound effect on SFX channel
        private void PlaySound(AudioClip clip)
        {
            if (clip != null && sfxAudioSource != null)
            {
                sfxAudioSource.PlayOneShot(clip);
            }
        }
    }
}