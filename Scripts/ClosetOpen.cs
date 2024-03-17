using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetOpen : MonoBehaviour
{
    public float acmaAciY = -180;
    public float acmaHizi;
    public float acmaAciX;
    public float acmaAciZ;
    bool isOpen = false;

    private Quaternion kapaliRotasyon; // Kapaðýn kapalý rotasyonu
    private Quaternion acikRotasyon; // Kapaðýn açýk rotasyonu
     

    public GameObject player;
    public Vector3 playerVector;

    public bool menzileGirdi;

    private void Awake()
    {
        // Kapalý ve açýk rotasyonlarý belirle
        kapaliRotasyon = transform.rotation;
        acikRotasyon = Quaternion.Euler(acmaAciX, acmaAciY, acmaAciZ);
        

        
    }
    private void Update()
    {
        //RotateFonks();
        if(Input.GetKeyDown(KeyCode.E))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, acmaAciY, 0), Time.deltaTime * acmaHizi);
        }
        
    }


    public void RotateFonks()
    {

        if(Input.GetKeyDown(KeyCode.E))
            {
            // Kapalý ise aç, açýk ise kapat
            if (!isOpen)
            {
                isOpen = true;
                transform.rotation = Quaternion.Lerp(transform.rotation, acikRotasyon, Time.deltaTime * acmaHizi);
                Debug.Log("girdi");
            }
            else
            {
                isOpen = false;
                transform.rotation = Quaternion.Lerp(transform.rotation, kapaliRotasyon, Time.deltaTime * acmaHizi);
                Debug.Log("girdi else");
            }
        }

    }

}
