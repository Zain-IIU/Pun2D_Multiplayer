using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class JumpControl : MonoBehaviourPun
{
    [SerializeField]
    float fallMultiple;
    [SerializeField]
    float lowJumpMultiple;

    Rigidbody2D RB;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(photonView.IsMine)
        {
            if (RB.velocity.y < 0)
            {
                RB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiple - 1) * Time.deltaTime;
            }
            else if (RB.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                RB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiple - 1) * Time.deltaTime;
            }
        }
        
    }
}
