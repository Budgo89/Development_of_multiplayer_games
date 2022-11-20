using Controllers;
using Profile;
using Tool;
using UnityEngine;
using UnityEngine.UI;

internal class RoomController: BaseController
{
    private readonly ResourcePath _resourcePath = new ResourcePath("UI/Room");

    private Transform _placeForUi;
    private ProfilePlayers _profilePlayer;
    private Authorization _authorization;

    private RoomView _roomView;
    private Button _closeButton;

    public RoomController(Transform placeForUi, ProfilePlayers profilePlayer, Authorization authorization)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _authorization = authorization;

        _roomView = LoadView(placeForUi);
        AddElementsUi();
        Subscribe();
    }

    private RoomView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<RoomView>();
    }

    private void AddElementsUi()
    {
        _closeButton = _roomView.CloseButton;
    }

    private void Subscribe()
    {
        _closeButton.onClick.AddListener(CloseOnClickButton);
    }

    private void CloseOnClickButton()
    {
        _authorization.CloseRoom();
    }

    private void Unsubscribe()
    {
        _closeButton.onClick.RemoveAllListeners();
    }

    protected override void OnDispose()
    {
        Unsubscribe();
    }
}