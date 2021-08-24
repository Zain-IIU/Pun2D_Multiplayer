using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;
public class myGameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Variables
    public static myGameManager instance;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject[] playerPrefab;
    [SerializeField]
    GameObject followCamera;
    [SerializeField]
    float _timeforeachRound;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    GameObject startButton;
    [SerializeField]
    GameObject LobbyPanel;
    float curTime;
    AudioSource Source;

    [SerializeField]
    AudioClip dieSFX;
    int playerIndex;
    int RandomPos;
    bool hasStarted = false;
    [SerializeField]
    string MainMenuScene;
    [SerializeField]
    GameObject endingPanel;

    
    //testing pourpose
    GameObject playerData;
    #endregion

    #region Unity Functions
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();

        if (playerPrefab != null)
        {
            RandomPos = 5;
            playerIndex = PlayerPrefs.GetInt("Index");
            Debug.Log(PlayerPrefs.GetInt("Index"));

            PlayerPrefs.SetInt("Points", 100);
            Debug.Log(PhotonNetwork.NickName);

            playerData = PhotonNetwork.Instantiate(playerPrefab[playerIndex].name, spawnPoints[RandomPos].position, Quaternion.identity);

            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().enabled = false;
        }



        if (PhotonNetwork.IsMasterClient)
            startButton.SetActive(true);
        else

            startButton.SetActive(false);
        curTime = _timeforeachRound;
    }

    private void Update()
    {
        if (hasStarted)
        {
            curTime -= Time.deltaTime;
            timerText.text = curTime.ToString("f0");
            if (curTime <= 0f)
            {

                endingPanel.SetActive(true);
            }

        }

    }
    #endregion

    #region Public Functions
    public void StartGame()
    {

        GetComponent<PhotonView>().RPC("TimetoNormal", RpcTarget.AllBuffered);
    }

    public void PlayDeathSFX()
    {
        Source.PlayOneShot(dieSFX);
    }
    //Respawn Same Player Randomly
    public void Respawn()
    {
        RandomPos = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate(playerPrefab[playerIndex].name, spawnPoints[RandomPos].position, Quaternion.identity);
    }

    //function to leave the room
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


    public int GetCurrentDeaths()
    {
        return PlayerPrefs.GetInt("Deaths");
    }

    #endregion

    #region Remote Photon Calls(RPCs)
    [PunRPC]
    private  void TimetoNormal()
    {
        hasStarted = true;
        LobbyPanel.GetComponent<PhotonView>().RPC("RemoveBlock", RpcTarget.AllBuffered,LobbyPanel.GetComponent<PhotonView>().ViewID, false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>().enabled = true;
    }


    #endregion

    #region PhotonCallBacks

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " has Joined the Room");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " has Entered the room " + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(LobbyPanel.activeSelf);
            stream.SendNext(LobbyPanel.activeSelf);
            stream.SendNext(LobbyPanel.activeSelf);
        }
        else
        {
            LobbyPanel.SetActive((bool)stream.ReceiveNext());
            LobbyPanel.SetActive((bool)stream.ReceiveNext());
            LobbyPanel.SetActive((bool)stream.ReceiveNext());
        }
    }
    #endregion

}
