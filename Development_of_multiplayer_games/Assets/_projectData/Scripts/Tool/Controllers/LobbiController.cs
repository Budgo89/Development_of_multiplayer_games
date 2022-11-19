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
    }

    private void Subscribe()
    {
        _refreshButton.onClick.AddListener(RefreshOnClickButton);
        _createRoomButton.onClick.AddListener(CreateRoomButton);
    }

    private void CreateRoomButton()
    {

        _authorization.CreateRoomButton(_createRoomName.text);
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
            roomButton.RoomButton.onClick.AddListener(ConnectRoom);
            _roomButtons.Add(roomButton);
        }
    }

    private void ConnectRoom()
    {
        Debug.Log("Есть контакт");
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
}