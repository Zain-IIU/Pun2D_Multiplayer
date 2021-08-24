using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NameSelectionMechanism : MonoBehaviour
{
    [SerializeField]
    GameObject nextButton;
    private void Start()
    {
        nextButton.SetActive(false);
    }
    public void SetPlayerName(string name)
    {
        if(string.IsNullOrEmpty(name) ||  name.Length<3)
        {
            nextButton.SetActive(false);
            Debug.Log("Invalid Player Name");
            return;
        }
        else
        {
            nextButton.SetActive(true);
            PhotonNetwork.NickName = name;
        }
        
    }
}
