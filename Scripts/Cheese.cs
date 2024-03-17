using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheese : MonoBehaviour
{
    public Text cheeseCountText; // Toplanan peynir sayısını gösterecek metin nesnesi
    public Text completionMessageText; // Tamamlanma mesajını gösterecek metin nesnesi
    public int totalCheeses = 10; // Toplam peynir sayısı
    private int collectedCheeses = 0; // Toplanan peynir sayısı

    void Start()
    {
        UpdateCheeseCountText(); // Başlangıçta peynir sayısını güncelle
        HideCompletionMessage(); // Tamamlanma mesajını gizle
    }

    // Peynir toplandığında bu fonksiyon çağrılır
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectedCheeses++; // Toplanan peynir sayısını arttır
            UpdateCheeseCountText(); // Metin nesnesini güncelle
            if (collectedCheeses >= totalCheeses)
            {
                ShowCompletionMessage(); // Eğer tüm peynirler toplandıysa tamamlanma mesajını göster
            }
            gameObject.SetActive(false); // Peyniri etkisiz hale getir
        }
    }

    // Peynir sayısını güncelleyen fonksiyon
    void UpdateCheeseCountText()
    {
        cheeseCountText.text = "Peynir Sayısı: " + collectedCheeses.ToString() + " / " + totalCheeses.ToString();
    }

    // Tamamlanma mesajını gösteren fonksiyon
    void ShowCompletionMessage()
    {
        completionMessageText.text = "Tüm peynirler toplandı!";
    }

    // Tamamlanma mesajını gizleyen fonksiyon
    void HideCompletionMessage()
    {
        completionMessageText.text = "";
    }
}
