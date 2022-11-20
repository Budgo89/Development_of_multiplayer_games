using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using Profile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Authorization : MonoBehaviourPunCallbacks, IDisposable
{
    [SerializeField] private string _playFabTitle;
    [SerializeField] private string _gameVersion = "dev";
    [SerializeField] private string _authentificationKey = "AUTHENTIFICATION_KEY";
    private List<RoomInfo> _roomList;
    private MenuView _menuView;
    private ProfilePlayers _profilePlayer;

    private Button _playFabConnectButton;
    private Button _playFabDisconnectButton;
    private Button _photonConnectButton;
    private Button _photonDisconnectButton;
    private Button _playFabDeleteAccButton;
    private TMP_Text _debagText;
    private string _roomName;

    private RoomOptions _roomOptions;

    public void StartAuthorization(MenuView menuView, ProfilePlayers profilePlayer)
    {
        _menuView = menuView;
        _profilePlayer = profilePlayer;
        AddElementsUi();
        Subscribe();
        _playFabDisconnectButton.gameObject.SetActive(false);
        _photonDisconnectButton.gameObject.SetActive(false);
    }
    

    private void AddElementsUi()
    {
        _playFabConnectButton = _menuView.PlayFabConnectButton;
        _playFabDisconnectButton = _menuView.PlayFabDisconnectButton;
        _photonConnectButton = _menuView.PhotonConnectButton;
        _photonDisconnectButton = _menuView.PhotonDisconnectButton;
        _playFabDeleteAccButton = _menuView.PlayFabDeleteAccButton;
        _debagText = _menuView.DebagText;
    }

    private void Subscribe()
    {
        _playFabConnectButton.onClick.AddListener(PlayFabConnectOnClickButton);
        _playFabDisconnectButton.onClick.AddListener(PlayFabDisconnectOnClickButton);
        _photonConnectButton.onClick.AddListener(PhotonConnectOnClickButton);
        _photonDisconnectButton.onClick.AddListener(PhotonDisconnectOnClickButton);
        _playFabDeleteAccButton.onClick.AddListener(PlayFabDeleteAccOcClickButton);
    }

    private void PlayFabDeleteAccOcClickButton()
    {
        PlayerPrefs.DeleteKey(_authentificationKey);
    }

    private void PhotonDisconnectOnClickButton()
    {
        PhotonNetwork.Disconnect();
        _debagText.text = $"�� �����������";
        Debug.Log($"�� ����������� {PhotonNetwork.IsConnected}");
        _photonDisconnectButton.gameObject.SetActive(false);
        _playFabConnectButton.gameObject.SetActive(true);
    }

    private void PhotonConnectOnClickButton()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = _playFabTitle;

        var needCreation = !PlayerPrefs.HasKey(_authentificationKey);
        var id = PlayerPrefs.GetString(_authentificationKey, Guid.NewGuid().ToString());

        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = true
        };
        
        PhotonNetwork.AuthValues = new AuthenticationValues(request.TitleId);
        PhotonNetwork.NickName = request.CustomId;
        //Connect();
        _photonDisconnectButton.gameObject.SetActive(true);
        _playFabConnectButton.gameObject.SetActive(false);
        _profilePlayer.CurrentState.Value = GameState.Lobbi;
    }

    private void PlayFabDisconnectOnClickButton()
    {

        _playFabDisconnectButton.gameObject.SetActive(false);
        _playFabConnectButton.gameObject.SetActive(true);
    }

    private void PlayFabConnectOnClickButton()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
            PlayFabSettings.staticSettings.TitleId = _playFabTitle;
        var needCreation = !PlayerPrefs.HasKey(_authentificationKey);
        var id = PlayerPrefs.GetString(_authentificationKey, Guid.NewGuid().ToString());

        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                PlayerPrefs.SetString(_authentificationKey, id);
                Debug.Log(result.PlayFabId);
                _debagText.text = $"����������� ������� \n {result.PlayFabId}";
                _playFabDisconnectButton.gameObject.SetActive(true);
                _playFabConnectButton.gameObject.SetActive(false);
                
            },
            error => Debug.LogError(error));

        
    }

    private void Unsubscribe()
    {
        _playFabConnectButton.onClick.RemoveAllListeners();
        _playFabDisconnectButton.onClick.RemoveAllListeners();
    }

    private void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomOrCreateRoom(roomName: $"Room N{Random.Range(0,10)}");
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster");
        if(!PhotonNetwork.InRoom)
            PhotonNetwork.JoinRandomOrCreateRoom(roomName: _roomName);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("OnCreatedRoom");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        _debagText.text = $"\n������� ������������ � {PhotonNetwork.CurrentRoom.Name}";
        Debug.Log($"OnJoinedRoom {PhotonNetwork.CurrentRoom.Name} ");
    }

    public void Dispose()
    {
        Unsubscribe();
    }

    public List<RoomInfo> GetRoom()
    {
        return _roomList;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = new List<RoomInfo>();
        _roomList = roomList;
    }

    public void CreateRoomButton(string text)
    {
        _roomName = text;
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsConnected)
        {
            _roomOptions = new RoomOptions();
            _roomOptions.MaxPlayers = 10;
            //PhotonNetwork.CreateRoom(text, roomOptions, TypedLobby.Default);
            PhotonNetwork.JoinRandomOrCreateRoom(roomName: text, roomOptions: _roomOptions);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    }

    public void CloseRoom()
    {
        if (_roomOptions == null)
            _roomOptions = new RoomOptions();
        _roomOptions.IsOpen = false;
        
    }

    public void CreateRoomFriendsButton(string text)
    {
        _roomName = text;
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsConnected)
        {
            _roomOptions = new RoomOptions();
            _roomOptions.MaxPlayers = 10;
            _roomOptions.IsVisible = false;
            PhotonNetwork.JoinRandomOrCreateRoom(roomName: text, roomOptions: _roomOptions);
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = PhotonNetwork.AppVersion;
        }
    }

    public void ConnectRoom(string nameRoom)
    {
        PhotonNetwork.JoinRoom(nameRoom);
    }

    public void ConnectHiddenRoom(string nameRoom)
    {
        PhotonNetwork.JoinRoom(nameRoom);
    }

    public (string, bool) GetInfoRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.CurrentRoom != null)
                return (PhotonNetwork.CurrentRoom.Name, PhotonNetwork.CurrentRoom.IsVisible);
            else
                return ("", false);
        }
            

        else
            return ("", false);
    }
}
