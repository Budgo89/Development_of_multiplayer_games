using Profile;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

public class MainController : BaseController
{
    private ProfilePlayers _profilePlayer;
    private Transform _placeForUi;
    private Authorization _authorization;

    private ConnectController _connectController;
    private LobbiController _lobbiController;
    private RoomController _roomController;

    public MainController(ProfilePlayers profilePlayer, Transform placeForUi, Authorization authorization)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _authorization = authorization;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    private void OnChangeGameState(GameState state)
    {
        DisposeControllers();
        switch (state)
        {
            case GameState.Connect:
                _connectController = new ConnectController(_placeForUi, _profilePlayer, _authorization);
                break;
            case GameState.Lobbi:
                _lobbiController = new LobbiController(_placeForUi, _profilePlayer, _authorization);
                break;
            case GameState.Room:
                _roomController = new RoomController(_placeForUi, _profilePlayer, _authorization);
                break;
        }
    }

    private void DisposeControllers()
    {
        _connectController?.Dispose();
        _lobbiController?.Dispose();
        _roomController?.Dispose();
    }
}
