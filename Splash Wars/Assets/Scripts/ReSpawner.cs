using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class ReSpawner : MonoBehaviourPun
{ 

     [SerializeField]
    float timetoRespawn;

    float curTime;


    [SerializeField]
    TextMeshProUGUI respawnCounter;

    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        curTime = timetoRespawn;
        respawnCounter.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            respawnCounter.gameObject.SetActive(true);
            respawnCounter.text = "Respawning In "+ curTime.ToString("f0");

            curTime -= Time.deltaTime;
            if(curTime<0.01f)
            {
                respawnCounter.gameObject.SetActive(false);
                isDead = false;
                curTime = timetoRespawn;
                if (GameObject.FindGameObjectWithTag("Manager") != null)
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<myGameManager>().Respawn();

             }
        }
    }
}
