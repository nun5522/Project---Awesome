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
            if (seconds < 30f) return "S  - Speedrunner!";
            if (seconds < 60f) return "A  - Amazing!";
            if (seconds < 120f) return "B  - Good Job!";
            if (seconds < 180f) return "C  - Not Bad!";
            return "D  - Keep Trying!";
        }
    }
}