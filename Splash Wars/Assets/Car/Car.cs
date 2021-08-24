using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float rotSpeed;
    [SerializeField]
    float accelaration;
    [SerializeField]
    float decelaration;
    [SerializeField]
    float rotationInertia;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    LayerMask groundLayer;
    Rigidbody2D RB;
    float curSpeed;
    float curTorque;
   bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        curSpeed = 0;
        curTorque = 0;
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);

        if (Input.GetMouseButton(0) && curSpeed < maxSpeed)
        {
            if (isGrounded)
                curSpeed += accelaration;
            else
            {
                if ( RB.angularDrag>0.15)
                {
                    RB.angularDrag -= accelaration;
                }

                RB.AddTorque(rotSpeed);
            }

        }
        else
        {
            if (curSpeed > 0)
                curSpeed -= decelaration;

            if (RB.angularDrag < 1.5f )
            {
                RB.angularDrag += rotationInertia;
            }

        }


    }
    private void FixedUpdate()
    {
        RB.AddForce(Vector2.right * curSpeed );
      //  RB.velocity = new Vector2(curSpeed, 0f);
    }

}
