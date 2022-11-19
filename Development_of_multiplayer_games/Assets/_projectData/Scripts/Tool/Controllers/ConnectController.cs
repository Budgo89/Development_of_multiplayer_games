using Controllers;
using Profile;
using System.Net;
using Tool;
using UnityEngine;

internal class ConnectController : BaseController
{
    private readonly ResourcePath _resourcePath = new ResourcePath("UI/Menu");

    private Transform _placeForUi;
    private ProfilePlayers _profilePlayer;
    private Authorization _authorization;

    private MenuView _menuView;

    public ConnectController(Transform placeForUi, ProfilePlayers profilePlayer, Authorization authorization)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _authorization = authorization;

        _menuView = LoadView(placeForUi);
        _authorization.StartAuthorization(_menuView, _profilePlayer);
    }

    private MenuView LoadView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<MenuView>();
    }
}