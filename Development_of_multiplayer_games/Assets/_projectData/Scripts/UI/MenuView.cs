using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField] private Button _playFabConnectButton;
    [SerializeField] private Button _playFabDisconnectButton;
    [SerializeField] private Button _photonConnectButton;
    [SerializeField] private Button _photonDisconnectButton;
    [SerializeField] private TMP_Text _debagText;

    public Button PlayFabConnectButton => _playFabConnectButton;
    public Button PlayFabDisconnectButton => _playFabDisconnectButton;
    public Button PhotonConnectButton => _photonConnectButton;
    public Button PhotonDisconnectButton => _photonDisconnectButton;
    public TMP_Text DebagText => _debagText;
}
