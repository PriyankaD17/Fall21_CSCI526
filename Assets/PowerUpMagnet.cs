using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Player")){
            //Magnet.magnetPowerUp = true;
            Magnet.powerUpTime = 3f;
            Destroy(gameObject);
        }
    }
}
