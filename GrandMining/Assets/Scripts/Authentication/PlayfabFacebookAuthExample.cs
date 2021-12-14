// Import statements introduce all the necessary classes for this example.
//using Facebook.Unity;
using PlayFab;
using System.Collections;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LoginResult = PlayFab.ClientModels.LoginResult;
using System;

public class PlayfabFacebookAuthExample : MonoBehaviour
{
    // holds the latest message to be displayed on the screen
    private string _message;
    public Slider sliderr;
    public GameObject playButton, guestPlayButton, loadingPanel;
    public static string id;
    public void Start()
    {
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.Log("logged in");
            loadingPanel.SetActive(true);
            playButton.SetActive(false);
            guestPlayButton.SetActive(false);
            StartCoroutine(loadSceneAsync(1));
        }
        else
        {
            Debug.Log("Did not log in");
            guestLogIn();
        }
        //SetMessage("Initializing Facebook..."); // logs the given message and displays it on the screen using OnGUI method

        // This call is required before any other calls to the Facebook API. We pass in the callback to be invoked once initialization is finished
        //FB.Init(OnFacebookInitialized);

    }
    void Awake()
    {
        /*if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
            //OnFacebookInitialized();
        }*/
    }
    
    IEnumerator loadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        DataStore.data.GetStatistics();
        while (!operation.isDone && DataStore.data.done != 8) 
        {
            float progress = Mathf.Clamp01(operation.progress / 0.5f);
            sliderr.value = progress;
            //yield return new WaitForSeconds(0.5f);
            yield return null;
        }
    }
    /*private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            if (FB.IsLoggedIn)
            {
                Debug.Log("Logged in!");
                //FB.Mobile.RefreshCurrentAccessToken();
                loadingPanel.SetActive(true);
                playButton.SetActive(false);
                guestPlayButton.SetActive(false);
                PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString },
    OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
                //StartCoroutine(loadSceneAsync(1));
                //SceneManager.LoadScene(1);
            }
            //OnFacebookInitialized();    
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }*/

    /*public void logIN()
    {
        OnFacebookInitialized();
    }*/

    public void guestLogIn()
    {
#if UNITY_ANDROID
        var requestAndroidID = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = ReturnMobileID(), CreateAccount = true };
        PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroidID, OnLoginMobileSuccess, OnLoginMobileFailure);
#endif
#if UNITY_IOS
                var requestIOSid = new LoginWithIOSDeviceIDRequest { DeviceId = ReturnMobileID(), CreateAccount = true };
        PlayFabClientAPI.LoginWithIOSDeviceID(requestIOSid, OnLoginMobileSuccess, OnLoginMobileFailure);
        
#endif
    }

    private void OnLoginMobileSuccess(LoginResult result)
    {
        id = result.PlayFabId;
        Debug.Log(id);
        loadingPanel.SetActive(true);
        playButton.SetActive(false);
        guestPlayButton.SetActive(false);
        StartCoroutine(loadSceneAsync(1));
    }
    private void OnLoginMobileFailure(PlayFabError error)
    {
        Debug.Log("Mobile Log in Fail!");
    }

    public static string ReturnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }

    /*private void OnFacebookInitialized()
    {
        SetMessage("Logging into Facebook...");

        // Once Facebook SDK is initialized, if we are logged in, we log out to demonstrate the entire authentication cycle.

        // We invoke basic login procedure and pass in the callback to process the result
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, OnFacebookLoggedIn);
    }*/

    //private void OnFacebookLoggedIn(ILoginResult result)
    //{
        // If result has no errors, it means we have authenticated in Facebook successfully
        //if (result == null || string.IsNullOrEmpty(result.Error))
        //{
            //SetMessage("Facebook Auth Complete! Access Token: " + AccessToken.CurrentAccessToken.TokenString + "\nLogging into PlayFab...");

            /*
             * We proceed with making a call to PlayFab API. We pass in current Facebook AccessToken and let it create
             * and account using CreateAccount flag set to true. We also pass the callback for Success and Failure results
             */
            //PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest { CreateAccount = true, AccessToken = AccessToken.CurrentAccessToken.TokenString },
                //OnPlayfabFacebookAuthComplete, OnPlayfabFacebookAuthFailed);
        //}
        //else
        //{
            // If Facebook authentication failed, we stop the cycle with the message
            //SetMessage("Facebook Auth Failed: " + result.Error + "\n" + result.RawResult, true);
        //}
    //}


    // When processing both results, we just set the message, explaining what's going on.
    /*private void OnPlayfabFacebookAuthComplete(LoginResult result)
    {
        SetMessage("PlayFab Facebook Auth Complete. Session ticket: " + result.SessionTicket);
        //DataStore.data.setData();
        //DataStore.data.GetStatistics();
        StartCoroutine(loadSceneAsync(1));
        //SceneManager.LoadScene(1);
    }*/

    /*private void OnPlayfabFacebookAuthFailed(PlayFabError error)
    {
        SetMessage("PlayFab Facebook Auth Failed: " + error.GenerateErrorReport(), true);
    }*/

    public void SetMessage(string message, bool error = false)
    {
        _message = message;
        if (error)
            Debug.LogError(_message);
        else
            Debug.Log(_message);
    }

    public void OnGUI()
    {
        var style = new GUIStyle { fontSize = 40, normal = new GUIStyleState { textColor = Color.white }, alignment = TextAnchor.MiddleCenter, wordWrap = true };
        var area = new Rect(0, 0, Screen.width, Screen.height);
        GUI.Label(area, _message, style);
    }
}