using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoginStateView : GameStateView
{
    [Header("LoginScreen")]
    [SerializeField] private GameObject _loginScreen;
    [SerializeField] private TMP_InputField _emailField;
    [SerializeField] private TMP_InputField _passwordField;
    [SerializeField] private Button _loginButton;

    [Header("RegisterScreen")]
    [SerializeField] private GameObject _registerScreen;
    [SerializeField] private TMP_InputField _registerUsernameField;
    [SerializeField] private TMP_InputField _registerEmailField;
    [SerializeField] private TMP_InputField _registerPasswordField;
    [SerializeField] private Button _registerButton;

    [Header("ErrorScreen")]
    [SerializeField] private GameObject _errorMessageContainer;

    public Action<LoginRequestData> LoginButtonClicked;
    public Action<RegisterRequestData> RegisterButtonClicked;

    void Awake()
    {
        RegisterButtonListneners();
        SetInitialDisplay();
    }

    private void RegisterButtonListneners()
    {
        // Should I move this to a separate method in order to be able to unregister 
        // duting on destroy?
        _loginButton.onClick.AddListener(() =>
        {
            LoginButtonClicked?
                .Invoke(new LoginRequestData
                {
                    Email = _emailField.text,
                    Password = _passwordField.text
                });
        });

        _registerButton.onClick.AddListener(() =>
        {
            RegisterButtonClicked?
                .Invoke(new RegisterRequestData
                {
                    RegisterUsername = _registerUsernameField.text,
                    RegisterEmail = _registerEmailField.text,
                    RegisterPassword = _registerPasswordField.text
                });
        });
    }

    private void SetInitialDisplay()
    {
        DisplayErrorMessage(false, null);
        OpenLoginScreen(true);
        OpenRegisterScreen(false);
    }

    public void DisplayErrorMessage(bool display, string message)
    {
        var textSlot = _errorMessageContainer.GetComponentInChildren<TextMeshProUGUI>();

        if (textSlot == null) throw new ArgumentNullException();
        else
        {
            textSlot.text = message;
            _errorMessageContainer.SetActive(display);
        }
    }

    public void OpenLoginScreen(bool open)
    {
        _loginScreen.SetActive(open);
    }

    public void OpenRegisterScreen(bool open)
    {
        _registerScreen.SetActive(open);
    }
}
