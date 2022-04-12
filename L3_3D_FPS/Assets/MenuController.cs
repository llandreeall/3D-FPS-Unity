using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuController : MonoBehaviourPunCallbacks
{
    /*
    [SerializeField] private string VersionName = "0.1";
    [SerializeField] private GameObject UsernameMenu;
    [SerializeField] private GameObject ConnectPanel;

    [SerializeField] private InputField UsernameInput;

    [SerializeField] private GameObject StartButton;

    private void Awake()
    {
        //PhotonNetwork.ConnectUsingSettings(VersionName);
    }
    // Start is called before the first frame update
    void Start()
    {
        UsernameMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected");
    }

    public void ChangeUsernameInput()
    {
        if(UsernameInput.text.Length >= 3)
        {
            StartButton.SetActive(true);
        } else
        {
            StartButton.SetActive(false);
        }
    }

    public void SetUsername()
    {
        UsernameMenu.SetActive(false);
        //PhotonNetwork.playerName = UsernameInput.text;
    }

    public void CreateGame()
    {
        PhotonNetwork.CreateRoom(CreateInput.text, new RoomOptions() { MaxPlayers = 3 }, null);
    }

    public void JoinGame()
    {
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = 3 };
        PhotonNetwork.JoinOrCreateRoom(JoinInput.text, roomOptions, TypedLobby.Default);

    }

    private void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }
    */

    [SerializeField] private InputField CreateInput;
    [SerializeField] private InputField JoinInput;

    public void CreateGame()
    {
        //if (string.IsNullOrEmpty(CreateInput.text))
            //return;
        PhotonNetwork.CreateRoom(CreateInput.text);
    }

    public void JoinGame()
    {
        //if (string.IsNullOrEmpty(JoinInput.text))
            //return;
        PhotonNetwork.JoinRoom(JoinInput.text);

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }
}
