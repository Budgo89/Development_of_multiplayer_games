using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LobbiView : MonoBehaviour
{
    [SerializeField] private Button _refreshButton;
    [SerializeField] private Transform _content;
    [SerializeField] private TMP_InputField _createRoomName;
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private Button _createRoomFriendsButton;
    [SerializeField] private Button _openHiddenRoomButton;


    public Button RefreshButton => _refreshButton;
    public Transform Content => _content;
    public TMP_InputField CreateRoomName => _createRoomName;
    public Button CreateRoomButton => _createRoomButton;
    public Button CreateRoomFriendsButton => _createRoomFriendsButton;
    public Button OpenHiddenRoomButton => _openHiddenRoomButton;
}
