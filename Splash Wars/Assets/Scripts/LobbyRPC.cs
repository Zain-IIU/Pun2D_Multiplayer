using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
public class LobbyRPC : MonoBehaviourPun
{
    [PunRPC]
    void RemoveBlock(int BlockToRemove, bool setActive)
    {
        PhotonView Disable = PhotonView.Find(BlockToRemove);
        Disable.transform.gameObject.SetActive(setActive);
    }

}
