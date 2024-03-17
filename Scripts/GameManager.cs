using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text cheeseCountText; // Toplanan peynir sayısını gösterecek metin nesnesi
    public GameObject cheeseCounterPanel; // Peynir sayacının bulunduğu panel
    public Text completionMessageText; // Tamamlanma mesajını gösterecek metin nesnesi
    public int totalCheeses = 10; // Toplam peynir sayısı
    private int collectedCheeses = 0; // Toplanan peynir sayısı

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(GameManager).Name;
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    void Start()
    {
        UpdateCheeseCountText(); // Başlangıçta peynir sayısını güncelle
    }

    // Peynir toplandığında bu fonksiyon çağrılır
    public void CollectCheese()
    {
        collectedCheeses++; // Toplanan peynir sayısını arttır
        UpdateCheeseCountText(); // Metin nesnesini güncelle
        if (collectedCheeses >= totalCheeses)
        {
            ShowCompletionMessage(); // Eğer tüm peynirler toplandıysa tamamlanma mesajını göster
        }
    }

    // Peynir sayısını güncelleyen fonksiyon
    void UpdateCheeseCountText()
    {
        cheeseCountText.text = "x" + collectedCheeses.ToString();
    }

    // Tamamlanma mesajını gösteren fonksiyon
    void ShowCompletionMessage()
    {
        completionMessageText.text = "Tüm peynirler toplandı!";
        cheeseCounterPanel.SetActive(false); // Peynir sayacını gizle
    }
}
