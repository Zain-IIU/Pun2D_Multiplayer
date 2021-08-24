using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class LeaderBoardSystem : MonoBehaviourPun
{
    [SerializeField]
    TextMeshProUGUI playerName;
    [SerializeField]
    TextMeshProUGUI playerKills;
    [SerializeField]
    TextMeshProUGUI playerDeaths;

    [SerializeField]
    GameObject leaderBoardPanel;
    [SerializeField]
    GameObject[] playersList;
    [SerializeField]
    GameObject board;
    int index,playerCount;
    // Start is called before the first frame update
    void Start()
    {
        leaderBoardPanel.SetActive(false);
        playerCount = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            leaderBoardPanel.SetActive(true);
            playerCount = 0;
            GetComponent<PhotonView>().RPC("ListPlayers", RpcTarget.AllBuffered);
        }
        else
        {
            leaderBoardPanel.SetActive(false);
        }
        
    }
    [PunRPC]
    public void ListPlayers()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            board.transform.GetChild(playerCount).GetChild(0).GetComponent<TextMeshProUGUI>().text = player.NickName;
         
            board.transform.GetChild(playerCount).GetChild(1).GetComponent<TextMeshProUGUI>().text = myGameManager.instance.GetComponent<myGameManager>().GetCurrentDeaths().ToString();
           
            if (playerCount < PhotonNetwork.PlayerList.Length - 1)
                playerCount++;
        }
       
    }    
        
}
