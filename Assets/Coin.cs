using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool collected_y_n = false;

    [SerializeField] private Magnet magnet;
    [SerializeField] private GameObject player;

    private Vector3 toDirection;
    //private float y_postn;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            magnet.coins.Remove(gameObject);
            Destroy(gameObject);
            CoinManager.coinCount+=1;
        }


    }

    void Start(){
        //y_postn = transform.position.y;
        var toPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        toDirection = (toPos - transform.position).normalized;
    }

    void Update(){
        if(collected_y_n){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), Time.deltaTime * 15f);
            //transform.position.y = y_postn;
        }
            //transform.position += toDirection * Time.deltaTime * 15f;

    }
    public bool GetCollected(){
        return collected_y_n;
    }
    public void SetCollected(){
        collected_y_n = true;
    }
}