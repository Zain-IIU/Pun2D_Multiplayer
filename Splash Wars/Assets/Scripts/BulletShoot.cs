using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BulletShoot : MonoBehaviour
{
    Rigidbody2D RB;
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        RB.velocity = new Vector2(10f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
