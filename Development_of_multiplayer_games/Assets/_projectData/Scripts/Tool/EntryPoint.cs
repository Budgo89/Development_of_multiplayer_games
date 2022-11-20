using System.Collections;
using System.Collections.Generic;
using Profile;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [Header("Scene Objects")]
    [SerializeField] private Transform _placeForUi;

    [SerializeField] private Authorization _authorization;

    private MainController _mainController;
    private ProfilePlayers _profilePlayer;

    private void Awake()
    {
        _profilePlayer = new ProfilePlayers(GameState.Connect);
        _mainController = new MainController(_profilePlayer, _placeForUi, _authorization);
    }
    private void OnDestroy()
    {
        _profilePlayer = new ProfilePlayers(GameState.Connect);
        _mainController.Dispose();
    }
}
