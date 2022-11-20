using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _nameRoomText;
    [SerializeField] private TMP_Text _visibleRoomText;

    public Button CloseButton => _closeButton;
    public TMP_Text NameRoomText => _nameRoomText;
    public TMP_Text VisibleRoomText => _visibleRoomText;
}
