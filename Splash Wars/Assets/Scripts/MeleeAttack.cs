using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MeleeAttack : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PhotonView>().IsMine)
        {

            Debug.Log("Hited the Player " + collision.gameObject.GetComponent<PhotonView>().name);

            collision.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 100);
        }
    }
}
