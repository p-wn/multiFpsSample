using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class UserNamePw : MonoBehaviour
{
    [SerializeField] TMP_InputField inputID;
    [SerializeField] TMP_InputField inputPW;
    [SerializeField] Button loginBtn;
    [SerializeField] Button siginUpBtn;
    [SerializeField] TextMeshProUGUI debugLine;
    [SerializeField] GameObject loginPanel;

    private void Start()
    {
        if(loginBtn !=null)
        {
            loginBtn.onClick.AddListener(OnLogin);
        }
        if (siginUpBtn != null)
        {
            siginUpBtn.onClick.AddListener(OnSignUp);
        }
    }

    // 플레이어 상태에 대한 업데이트를 받기 원할경우 다음에 등록
    void SetupEvents()
    {
        AuthenticationService.Instance.SignedIn += () => {
            // Shows how to get a playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

            // Shows how to get an access token
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }


    public void OnLogin()
    {
        loginBtn.interactable = false;
        SignInWithUsernamePasswordAsync(inputID.text, inputPW.text);
        debugLine.text = "Try Login";

    }

    public void OnSignUp()
    {
        loginBtn.interactable = false;
        SignUpWithUsernamePasswordAsync(inputID.text, inputPW.text);
        debugLine.text = "Try SignUp";

    }
    //가입
    async Task SignUpWithUsernamePasswordAsync(string username, string password) 
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
            debugLine.text = "SignUp is successful";
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            debugLine.text = "SignUp error";
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            debugLine.text = "Request fail";
        }
    }
    //로그인
    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
            debugLine.text = "login Success";
            loginPanel.SetActive(false);
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            debugLine.text = "id or pw fail";
            loginBtn.interactable = true;
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
            debugLine.text = "Request fail";
            loginBtn.interactable = true;
        }
    }
}
