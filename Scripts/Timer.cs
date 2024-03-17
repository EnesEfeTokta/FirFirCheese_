using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalTime = 60f; // Toplam süre (saniye cinsinden)
    private float currentTime = 0f; // Geçen süre
    private Text timerText; // Süreyi gösteren metin nesnesi

    void Start()
    {
        timerText = GetComponent<Text>(); // Metin nesnesini al
        currentTime = totalTime; // Başlangıç süresini ayarla
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Geçen süreyi güncelle
            UpdateTimerDisplay(); // Süreyi gösteren metin nesnesini güncelle
        }
        else
        {
            // Süre dolduğunda yapılacak işlemler
            Debug.Log("Süre doldu!");
            EndGame(); // Oyunu bitir
        }
    }

    void UpdateTimerDisplay()
    {
        // Süreyi metin nesnesine uygun formatta göster
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        // Oyunu bitir
        Debug.Log("Oyun bitti!");
        // Burada istediğiniz oyun bitirme işlemlerini yapabilirsiniz.
        // Örneğin:
        // Time.timeScale = 0f; // Oyun zamanını durdur
        // GameObject.FindGameObjectWithTag("Player").SetActive(false); // Oyuncuyu devre dışı bırak
        // gameOverScreen.SetActive(true); // Oyun bitiş ekranını etkinleştir
    }
}
