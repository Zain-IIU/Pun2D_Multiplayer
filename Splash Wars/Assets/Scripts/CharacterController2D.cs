using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class CharacterController2D : MonoBehaviourPunCallbacks
{
    #region Variables
    [Header("Movement Parameters")]
    [SerializeField]
    float Speed;
    [SerializeField]
    float JumpForce;
    [Header("Ground Parameters")]
    [SerializeField]
    Transform GroundCheck;
    [SerializeField]
    LayerMask GroundLayer;
    [Header("Shooting")]
    [SerializeField]
    Transform bulletHole;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    int bulletsCount;
    [SerializeField]
    TextMeshProUGUI bulletCounter;
    int curShots;
    GameObject coolDownPanel;
    [SerializeField]
    float cooldownTime;
    float curTime;
    [Header("Attack")]
    [SerializeField]
    GameObject lightSaber;
    [SerializeField]
    Transform attackPoint;
    [Header("Name Plate")]
    [SerializeField]
    Text nameText;
    PhotonView PV;
    [SerializeField]
    Camera followCamera;
    [Header("SFX")]
    AudioSource Source;
    [SerializeField]
    AudioClip laserShoot;
    [SerializeField]
    AudioClip jumpSFX;
    [SerializeField]
    AudioClip knifeSFX;
    [SerializeField]
    CinemachineConfiner confiner;

    Rigidbody2D RB;
    Animator Anim;
    public bool facingRight;
    bool canShoot;
    bool isGrounded;
    float X;

    #endregion

    #region Unity Functions
    private void Awake()
    {
        
        PV = GetComponent<PhotonView>();
        lightSaber.SetActive(false);
        if (PV.IsMine)
            coolDownPanel = GameObject.FindGameObjectWithTag("CoolDown_Timer");

    }


    // Start is called before the first frame update
    void Start()
    {

        RB = this.GetComponent<Rigidbody2D>();
        Anim = this.GetComponent<Animator>();
        Source = this.GetComponent<AudioSource>();
        canShoot = true;
        curTime = cooldownTime;
       
        facingRight = true;
        curShots = 0;
        if (PV.IsMine)
        {
            nameText.text = PhotonNetwork.NickName;
            confiner.m_BoundingShape2D = GameObject.FindGameObjectWithTag("Boundry").GetComponent<PolygonCollider2D>();
            
        }
            
        else
        {
            followCamera.enabled = false;
            confiner.gameObject.GetComponent<CinemachineVirtualCamera>().enabled = false;
            nameText.text = PV.Owner.NickName;
            bulletCounter.gameObject.SetActive(false);
        }
            
    }

    private void Update()
    {
        if(PV.IsMine)

        {
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, GroundLayer);
            PV.RPC("manageUI", RpcTarget.AllBuffered);
            Attack();

            //Jumping
            JumpCheck();
            //Handling all the Animations
            AnimationFlow(X);
            if (Input.GetMouseButtonDown(0))
            {
                if(canShoot)
                {
                    if (curShots < bulletsCount)
                    {

                        Shoot();

                    }
                    else
                    {
                        canShoot = false;
                    }
                }
                
                
               
            }
            Flip();
            
            
            if(!canShoot)
            {
                coolDownPanel.transform.GetChild(0).gameObject.SetActive(true);
                curTime -= Time.deltaTime;
                if(curTime<0.01)
                {
                    canShoot = true;
                    curShots = 0;
                    curTime = cooldownTime;
                    coolDownPanel.transform.GetChild(0).gameObject.SetActive(false);
                    bulletCounter.text = "IIIII";
                }
            }
        }
      
      
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if(photonView.IsMine)
              Move();
        
    }
    #endregion


    #region Move Shoot Jump Animate etc

    void Shoot()
    {

        curShots++;
        Source.PlayOneShot(laserShoot);
       
        GameObject bullet=  PhotonNetwork.Instantiate(bulletPrefab.name, bulletHole.position, Quaternion.identity);
            if (!facingRight)
                bullet.GetComponent<PhotonView>().RPC("Reverse", RpcTarget.AllBuffered);
        ManageBulletCounts();
    
    }

   
    void ManageBulletCounts()
    {
        bulletCounter.text = bulletCounter.text.Substring(0, bulletCounter.text.Length - 1);
    }
    
    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Source.PlayOneShot(knifeSFX);
            PV.RPC("RemoveBlock", RpcTarget.AllBuffered, lightSaber.GetComponent<PhotonView>().ViewID,true);
        }
      
    }
   
    void Move()
    {
       
       
            X = Input.GetAxis("Horizontal");
        if(facingRight)
              transform.Translate(new Vector3(X * Time.deltaTime * Speed, 0, 0));
        else
            transform.Translate(new Vector3(-X * Time.deltaTime * Speed, 0, 0));

    }


   

    void JumpCheck()
    {
        if (isGrounded)
        {
            Jump();

        }
        else
            Anim.SetBool("isJumping", false);

    }
    void Jump()
    {

        if (Input.GetButton("Jump"))
        {
            Source.PlayOneShot(jumpSFX);
            RB.velocity = Vector2.up * JumpForce;
            Anim.SetBool("isJumping", true);
        }
    }


    
    
    void Flip()
    {
        if (X < 0)
        {
            facingRight = false;
            transform.localEulerAngles = new Vector3(0, 180f, 0);
        }
        else if (X > 0)
        {
            facingRight = true;
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }

    }

    void AnimationFlow(float X)
    {
        if(GetComponent<Health>().getHealth()>1)
        {
            Anim.SetFloat("isRunning", Mathf.Abs(X));
            Anim.SetBool("isShooting", Input.GetMouseButtonDown(0));

            if (isGrounded)
                Anim.SetBool("inAir", false);
            else
                Anim.SetBool("inAir", true);
        }
       
        
            
    }

    #endregion

    #region Remote Pun Calls
    [PunRPC]
    void manageUI()
    {
        Vector3 v = followCamera.transform.position - nameText.transform.position;
        v.x = v.z = 0.0f;
        nameText.transform.LookAt(followCamera.transform.position - v);
        nameText.transform.Rotate(0, 180, 0);
    }
    [PunRPC]
    void RemoveBlock(int BlockToRemove, bool setActive)
    {
        PhotonView Disable = PhotonView.Find(BlockToRemove);
        Disable.transform.gameObject.SetActive(setActive);
    }

    #endregion


}
