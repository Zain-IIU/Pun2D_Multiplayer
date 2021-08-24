using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;
public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    #region Variables
    [Header("Panels Section")]
    [SerializeField]
    Image moonProp;
    [SerializeField]
    RectTransform MainPanel;
    [SerializeField]
    RectTransform CharacterPanel;
    [SerializeField]
    RectTransform LoadingPanel;
    [SerializeField]
    RectTransform RoomPanel;
    [SerializeField]
    TMP_InputField joinName;
    [SerializeField]
    TMP_InputField createName;
    RoomOptions roomOptions;
    [Header("SOUND")]
    [SerializeField]
    AudioSource Source;
    [SerializeField]
    AudioClip buttonPress;
    #endregion


    #region Unity Functions
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainPanel.DOAnchorPos(Vector2.zero, 0.25f);
        
        
    }
    #endregion



    #region public Methods
    public void onPressPlayButton()
    {
        MainPanel.DOAnchorPos(new Vector2(-1919, 0), 0.25f);
        CharacterPanel.DOAnchorPos(Vector2.zero, 0.25f);
        Source.PlayOneShot(buttonPress);
    }
    public void onPressQuitButton()
    {
        Source.PlayOneShot(buttonPress);
        Application.Quit();       
        
    }
    public void returnToMenu()
    {
        Source.PlayOneShot(buttonPress);
        MainPanel.DOAnchorPos(Vector2.zero, 0.25f);
        CharacterPanel.DOAnchorPos(new Vector2(1919, 0), 0.25f);
    }


    public void gotoRoomPanel()
    {
        Source.PlayOneShot(buttonPress);
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            CharacterPanel.DOAnchorPos(new Vector2(1919, 0), 0.25f);
            LoadingPanel.DOAnchorPos(Vector2.zero, 0.25f);


        }
    }
   

    public void CreateRoom()
    {
        Source.PlayOneShot(buttonPress);
        roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;
        
        PhotonNetwork.CreateRoom(createName.text, roomOptions, TypedLobby.Default);
        if (photonView.IsMine)
        {
            PlayerPrefs.SetInt("Index", CharacterPanel.GetComponent<CharacterSelect>().getIndex());
            Debug.Log(PlayerPrefs.GetInt("Index"));
        }
    }
    public void JoinRoom()
    {
        Source.PlayOneShot(buttonPress);
        PhotonNetwork.JoinRoom(joinName.text);
        if (photonView.IsMine)
        {
            PlayerPrefs.SetInt("Index", CharacterPanel.GetComponent<CharacterSelect>().getIndex());
            Debug.Log(PlayerPrefs.GetInt("Index"));
        }
    }
    #endregion



    #region Pun Callbacks
    public override void OnConnected()
    {
        Debug.Log("has Internet Connection");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + " Connected to Master Server");
        LoadingPanel.DOAnchorPos(new Vector2(0, -1435), 0.25f);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(moonProp.gameObject.transform.DOMove(Vector2.zero, 0.5f)).Append(moonProp.gameObject.transform.DOScale(new Vector2(5, 5), 0.5f)).Append(moonProp.DOFade(0, 0.5f));
        RoomPanel.DOAnchorPos(Vector2.zero, 0.25f);

    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " Joined the Room " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("Game");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player Enterd the Room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " has Left the Room ");
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Leaving the Room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateRoom(roomOptions);
    }


   
    void CreateRoom(RoomOptions roomOptions)
    {

        PhotonNetwork.CreateRoom(createName.text, roomOptions, TypedLobby.Default);
    }
    #endregion
}
