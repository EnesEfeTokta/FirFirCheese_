using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseItem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectCheese(); // Oyuncunun peynir topladığını bildir
            gameObject.SetActive(false); // Peyniri etkisiz hale getir
        }
    }
}
