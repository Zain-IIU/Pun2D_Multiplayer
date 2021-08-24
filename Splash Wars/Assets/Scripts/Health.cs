using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


using UnityEngine.UI;
using TMPro;
public class Health : MonoBehaviourPun
{
    [SerializeField]
    int TotalHealth;
    [SerializeField]
    Slider HealthBar;
    int curHealth;

    [SerializeField]
    TextMeshProUGUI deathText;

    Animator Anim;

   
private void Awake()
    {
       if(photonView.IsMine)
        {
            deathText = GameObject.FindGameObjectWithTag("DText").GetComponent<TextMeshProUGUI>();
            deathText.text = PlayerPrefs.GetInt("Points").ToString();
        }
        else
        {
            HealthBar.gameObject.SetActive(false);
        }
          
    }
    // Start is called before the first frame update
    void Start()
    {
        curHealth = TotalHealth;
        if(photonView.IsMine)
        {
            HealthBar.value = HealthBar.maxValue = TotalHealth;
            HealthBar.minValue = 0;
        }
        Anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
      
    }

    [PunRPC]
   public void TakeDamage(int amount)
    {
       
        if(photonView.IsMine)
        { 
            curHealth -= amount;
         
            HealthBar.value = curHealth;
        if (curHealth < 1)
        {
            //DIED
            Anim.SetTrigger("Die");
            myGameManager.instance.PlayDeathSFX();
            GetComponent<CharacterController2D>().enabled = false;
            GetComponent<JumpControl>().enabled = false;
            GetComponent<Health>().enabled = false;
            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") - 5);
            Debug.Log(PhotonNetwork.NickName);
            deathText.text = PlayerPrefs.GetInt("Points").ToString();
            Invoke("KILL", 1.2f);
        }
          
        }
            

    }

    

    void KILL()
    {
     //destroying using Networks   
        PhotonNetwork.Destroy(this.transform.parent.gameObject);
    }
    
   
    public int getHealth()
    {

         return curHealth;
    }

    private void OnDestroy()
    {
        if(photonView.IsMine)
        {
            
            if(GameObject.FindGameObjectWithTag("Manager") != null)
                GameObject.FindGameObjectWithTag("Manager").GetComponent<ReSpawner>().isDead = true;

           
        }

    }

  
}
