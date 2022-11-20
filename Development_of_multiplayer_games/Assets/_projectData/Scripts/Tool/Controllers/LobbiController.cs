using Controllers;
using Photon.Realtime;
using Profile;
using System.Collections.Generic;
using TMPro;
using Tool;
using UnityEngine;
using UnityEngine.UI;

internal class LobbiController : BaseController
{
    private readonly ResourcePath _resourcePath = new ResourcePath("UI/Loddi");
    private readonly ResourcePath _resourcePathRoomButton = new ResourcePath("UI/RoomButton");

    private Transform _placeForUi;
    private ProfilePlayers _profilePlayer;
    private Authorization _authorization;

    private LobbiView _lobbiView;

    private RoomButtonView _buttonView;

    private Button _refreshButton;
    private Transform _content;
    private TMP_InputField _createRoomName;
    private Button _createRoomButton;
    private Button _createRoomFriendsButton;
    private Button _openHiddenRoomButton;

    private List<RoomInfo> _roomList;

    private List<RoomButtonView> _roomButtons;


    public LobbiController(Transform placeForUi, ProfilePlayers profilePlayer, Authorization authorization)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _authorization = authorization;

        _lobbiView = LoadView(placeForUi);
        AddElementsUi();
        Subscribe();
    }

    private LobbiView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<LobbiView>();
    }

    private void AddElementsUi()
    {
        _refreshButton = _lobbiView.RefreshButton;
        _content = _lobbiView.Content;
        _createRoomName = _lobbiView.CreateRoomName;
        _createRoomButton = _lobbiView.CreateRoomButton;
        _createRoomFriendsButton = _lobbiView.CreateRoomFriendsButton;
        _openHiddenRoomButton = _lobbiView.OpenHiddenRoomButton;
    }

    private void Subscribe()
    {
        _refreshButton.onClick.AddListener(RefreshOnClickButton);
        _createRoomButton.onClick.AddListener(CreateRoomButton);
        _createRoomFriendsButton.onClick.AddListener(CreateRoomFriendsOnClickButton);
        _openHiddenRoomButton.onClick.AddListener(OpenHiddenRoomOnClickButton);
    }

    private void OpenHiddenRoomOnClickButton()
    {
        _authorization.ConnectHiddenRoom(_createRoomName.text);
        _profilePlayer.CurrentState.Value = GameState.Room;
    }

    private void CreateRoomFriendsOnClickButton()
    {
        _authorization.CreateRoomFriendsButton(_createRoomName.text);
        _profilePlayer.CurrentState.Value = GameState.Room;
    }

    private void CreateRoomButton()
    {

        _authorization.CreateRoomButton(_createRoomName.text);
        _profilePlayer.CurrentState.Value = GameState.Room;
    }

    private void RefreshOnClickButton()
    {
        _roomList = new List<RoomInfo>();
        _roomList = _authorization.GetRoom();
        AddRoom();
    }

    private void AddRoom()
    {
        if (_roomList == null)
        {
            return;
        }
        if (_roomList.Count == 0) return;
        _roomButtons = new List<RoomButtonView>();
        foreach (var room in _roomList)
        {
            var roomButton = LoadViewRoom(_content);
            roomButton.CountRoomText.text = $"{room.PlayerCount} / {room.MaxPlayers}";
            roomButton.NameRoomText.text = room.Name;
            roomButton.RoomButton.onClick.AddListener(() => ConnectRoom(roomButton));
            _roomButtons.Add(roomButton);
        }
    }

    private void ConnectRoom(RoomButtonView room)
    {
        _authorization.ConnectRoom(room.name);
        _profilePlayer.CurrentState.Value = GameState.Room;
    }

    private RoomButtonView LoadViewRoom(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePathRoomButton);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<RoomButtonView>();
    }

    private void Unsubscribe()
    {
        _refreshButton.onClick.RemoveAllListeners();
    }

    protected override void OnDispose()
    {
        Unsubscribe();
    }
}