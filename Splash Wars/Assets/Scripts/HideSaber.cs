using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HideSaber : MonoBehaviourPun
{
   
    void Hide()
    {

        photonView.RPC("RemoveBlock", RpcTarget.AllBuffered, this.GetComponent<PhotonView>().ViewID, false);
    }
    [PunRPC]
    void RemoveBlock(int BlockToRemove, bool setActive)
    {
        PhotonView Disable = PhotonView.Find(BlockToRemove);
        Disable.transform.gameObject.SetActive(setActive);
    }
}
