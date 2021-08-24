using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Camera myCam;


  
    // Update is called once per frame
    void Update()
    {
     
        if (photonView.IsMine && myCam!=null)
        {
            myCam.enabled = false;
        }
        else  if(!photonView.IsMine && myCam!=null) 
        {
            myCam.enabled = true;

        }
    }
}
