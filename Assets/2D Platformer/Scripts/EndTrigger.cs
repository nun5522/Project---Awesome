using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class EndTrigger : MonoBehaviour
    {
        [Header("Settings")]
        public string endSceneName = "EndScreen";

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            // Stop the timer
            if (GameTimer.instance != null)
                GameTimer.instance.StopTimer();

            // Load end screen
            SceneManager.LoadScene(endSceneName);
        }
    }
}