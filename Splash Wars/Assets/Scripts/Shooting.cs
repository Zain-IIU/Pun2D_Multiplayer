using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class Shooting : MonoBehaviourPun
{
    public bool isRight;

    [SerializeField]
    float moveSpeed;

   
    Rigidbody2D RB;
        private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered,1.5f);
    }
    private void Update()
    {
        if (isRight)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

    }


    IEnumerator WaitforDestruction(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
    [PunRPC]
    public void Reverse()
    {
        isRight = false;
    }
    [PunRPC]
    void  Destroy(float time)
    {
        StartCoroutine(WaitforDestruction(time));
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PhotonView>().IsMine && collision.gameObject.GetComponent<Health>()!=null)
        {
            if(isRight)
            {
                Vector3 oldPos = collision.gameObject.GetComponent<Transform>().position;
                oldPos.x += 3f;
                collision.gameObject.GetComponent<Transform>().position = oldPos;
                Debug.Log("FOrce added to right");
            }
            else
            {
                Vector3 oldPos = collision.gameObject.GetComponent<Transform>().position;
                oldPos.x -= 3f;
                collision.gameObject.GetComponent<Transform>().position = oldPos;
                Debug.Log("FOrce added to left");
            }


            if (photonView.IsMine)
                PhotonNetwork.Destroy(this.gameObject);

            if (collision.gameObject.GetComponent<Health>().getHealth() >= 1)
                collision.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10);

        }
       
    }

    
    
}
