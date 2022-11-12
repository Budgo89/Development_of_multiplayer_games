using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationPlayFab : MonoBehaviour
{
    [SerializeField] private InputField _userNameField;
    [SerializeField] private InputField _userPasswordField;
    [SerializeField] private Button _registrationButton;
    [SerializeField] private Text _errorText;
    [SerializeField] private Image _loading;
    [SerializeField] private Text _lobbiUsername;
    [SerializeField] private Text _lobbiEmailname;
    [SerializeField] private Text _lobbiId;

    private string _userName;
    private string _userPassword;
    private bool _isLoading = false;

    private void Awake()
    {
        _userNameField.onValueChanged.AddListener(SetUserName);
        _userPasswordField.onValueChanged.AddListener(SetUserPassword);
        _registrationButton.onClick.AddListener(Submit);
    }

    private void Update()
    {
        if (_isLoading)
        {
            _loading.gameObject.transform.Rotate(0, 10 * Time.deltaTime, 0);
        }
    }

    private void SetUserName(string value)
    {
        _userName = value;
    }
    private void SetUserPassword(string value)
    {
        _userPassword = value;
    }

    private void Submit()
    {
        LoadingTrue();
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
        {
            Username = _userName,
            Password = _userPassword,
        }, result =>
        {
            _errorText.gameObject.SetActive(false);
            Debug.Log(result.LastLoginTime);
            LoadingFalse();
            Lobbi();
        }, error =>
        {
            _errorText.gameObject.SetActive(true);
            _errorText.text = error.ErrorMessage;
            Debug.LogError(error);
            LoadingFalse();
        });
    }

    private void LoadingFalse()
    {
        _isLoading = false;
        _loading.gameObject.SetActive(false);
    }
    private void LoadingTrue()
    {
        _isLoading = true;
        _loading.gameObject.SetActive(true);
    }

    private void Lobbi()
    {
        _userNameField.gameObject.SetActive(false);
        _userPasswordField.gameObject.SetActive(false);
        _registrationButton.gameObject.SetActive(false);
        _errorText.gameObject.SetActive(false);
        _lobbiUsername.gameObject.SetActive(true);
        _lobbiEmailname.gameObject.SetActive(true);
        _lobbiId.gameObject.SetActive(true);
        GetPlayerInfo();
    }


    private void GetPlayerInfo()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest
        {

        }, result =>
        {
            _lobbiUsername.text = $"Ваш логин: {result.AccountInfo.Username}";
            _lobbiEmailname.text = $"Ваш Email: {result.AccountInfo.PrivateInfo.Email}";
            _lobbiId.text = $"Ващ ID: {result.AccountInfo.PlayFabId}";
        }, error =>
        {

        });
    }
}
