using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomButtonView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameRoomText;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private Button _roomButton;

    public TMP_Text NameRoomText => _nameRoomText;
    public TMP_Text CountRoomText => _countText;
    public Button RoomButton => _roomButton;
}
