using UnityEngine;
using System.Collections.Generic;

namespace Platformer
{
    public class PlayerDeathMarker : MonoBehaviour
    {
        [Header("Settings")]
        public GameObject deathPrefab;

        [Header("Ghost Settings")]
        [Range(0f, 1f)]
        public float ghostAlpha = 0.5f;

        private static List<GameObject> markers = new List<GameObject>();
        private bool isDead = false;

        public void TriggerDeath()
        {
            isDead = true;
        }

        void OnDestroy()
        {
            if (!isDead) return;
            if (deathPrefab == null) return;

            GameObject marker = Instantiate(deathPrefab, transform.position, Quaternion.identity);

            // ไม่ให้หายตอน Scene โหลดใหม่
            DontDestroyOnLoad(marker);

            // หยุดไม่ให้ตก
            Rigidbody2D rb = marker.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.bodyType = RigidbodyType2D.Static;

            // ให้ Player เดินทะลุได้
            Collider2D playerCol = GetComponent<Collider2D>();
            foreach (Collider2D col in marker.GetComponentsInChildren<Collider2D>())
            {
                if (playerCol != null)
                    Physics2D.IgnoreCollision(col, playerCol, true);
            }

            // โปร่งแสง
            foreach (SpriteRenderer sr in marker.GetComponentsInChildren<SpriteRenderer>())
            {
                Color c = sr.color;
                c.a = ghostAlpha;
                sr.color = c;
            }

            markers.Add(marker);
            Debug.Log("Marker added! Total: " + markers.Count);

            // เก็บแค่ 2 อันล่าสุด
            while (markers.Count > 2)
            {
                if (markers[0] != null)
                    Destroy(markers[0]);
                markers.RemoveAt(0);
            }
        }

        public static void ClearMarkers()
        {
            foreach (GameObject m in markers)
                if (m != null) Destroy(m);
            markers.Clear();
        }
    }
}