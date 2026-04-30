using UnityEngine;

namespace Platformer
{
    public class GameTimer : MonoBehaviour
    {
        public static GameTimer instance;

        private float elapsedTime = 0f;
        private bool isRunning = false;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            StartTimer();
        }

        void Update()
        {
            if (!isRunning) return;
            elapsedTime += Time.deltaTime;
        }

        public void StartTimer()
        {
            elapsedTime = 0f;
            isRunning = true;
        }

        public void StopTimer()
        {
            isRunning = false;
            Debug.Log("Final Time: " + GetFormattedTime());
        }

        public float GetElapsedTime()
        {
            return elapsedTime;
        }

        // Returns time as 00:00:00
        public string GetFormattedTime()
        {
            int hours = Mathf.FloorToInt(elapsedTime / 3600);
            int minutes = Mathf.FloorToInt((elapsedTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        public void ResetTimer()
        {
            elapsedTime = 0f;
            isRunning = false;
        }
    }
}