using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 3; 

    void OnCollisionEnter(Collision collision)
    {
        health -= 1;
    }
}
