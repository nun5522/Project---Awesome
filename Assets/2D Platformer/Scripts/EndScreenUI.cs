using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class EndScreenUI : MonoBehaviour
    {
        [Header("UI References")]
        public Text finalTimeText;
        public Text rankText;
        public Button restartButton;

        void Start()
        {
            if (GameTimer.instance == null)
            {
                finalTimeText.text = "Time: --:--:--";
                return;
            }

            string time = GameTimer.instance.GetFormattedTime();
            float elapsed = GameTimer.instance.GetElapsedTime();

            // Show final time
            finalTimeText.text = "Your Time: " + time;

            // Show rank based on time
            rankText.text = "Rank: " + GetRank(elapsed);

            // Restart button
            restartButton.onClick.AddListener(() =>
            {
                GameTimer.instance.ResetTimer();
                SceneManager.LoadScene(0); // Load first scene
            });
        }

        string GetRank(float seconds)
        {
            if (seconds < 1201f) return "S  - Speedrunner!";
            if (seconds < 1401f) return "A  - Amazing!";
            if (seconds < 1601f) return "B  - Good Job!";
            if (seconds < 1801f) return "C  - Not Bad!";
            return "D  - Keep Trying!";
        }

        public void ExitGame()
        {
            Debug.Log("Exiting game...");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        }
    }
}