using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Lava : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Health>().getHealth() >= 1)
            {
                Debug.Log("Lava !!!!!");
                collision.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 100);
            }
                
        }
    }
}
