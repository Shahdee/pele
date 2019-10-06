using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabConnect 
{

    static protected string c_TitleId = "F8E7E";

#region Listeners

    // Anonymos login
    UnityAction<string, bool> m_OnLoginSuccCallback;
    UnityAction m_OnLoginFailCallback;

    public void AddLoginSuccessListener(UnityAction<string, bool> listener){
        m_OnLoginSuccCallback += listener;
    }

    public void RemoveLoginSuccessListener(UnityAction<string, bool> listener){
        m_OnLoginSuccCallback -= listener;
    }

    void OnLoginSuccess(string playerId, bool newlyCreated){
        if (m_OnLoginSuccCallback != null)
            m_OnLoginSuccCallback(playerId, newlyCreated);
    }

    public void AddLoginFailListener(UnityAction listener){
        m_OnLoginFailCallback += listener;
    }

    public void RemoveLoginFailListener(UnityAction listener){
        m_OnLoginFailCallback -= listener;
    }

    void OnLoginFailure(){
        if (m_OnLoginFailCallback != null)
            m_OnLoginFailCallback();
    }

    // Registration
    UnityAction m_OnRegisterSuccCallback;
    UnityAction m_OnRegisterFailCallback;

    public void AddRegisterSuccessListener(UnityAction listener){
        m_OnRegisterSuccCallback += listener;
    }

    public void RemoveRegisterSuccessListener(UnityAction listener){
        m_OnRegisterSuccCallback -= listener;
    }

    void OnRegisterSuccess(){
        if (m_OnRegisterSuccCallback != null)
            m_OnRegisterSuccCallback();
    }

    public void AddRegisterFailListener(UnityAction listener){
        m_OnRegisterFailCallback += listener;
    }

    public void RemoveRegisterFailListener(UnityAction listener){
        m_OnRegisterFailCallback -= listener;
    }

    void OnRegisterFailure(){
        if (m_OnRegisterFailCallback != null)
            m_OnRegisterFailCallback();
    }

     // Login with PlayFab
    UnityAction m_OnLoginWithPlayFabSuccCallback;
    UnityAction m_OnLoginWithPlayFabFailCallback;

    public void AddLoginWithPlayFabSuccessListener(UnityAction listener){
        m_OnLoginWithPlayFabSuccCallback += listener;
    }

    public void RemoveLoginWithPlayFabSuccessListener(UnityAction listener){
        m_OnLoginWithPlayFabSuccCallback -= listener;
    }

    void OnLoginWithPlayFabSuccess(){
        if (m_OnLoginWithPlayFabSuccCallback != null)
            m_OnLoginWithPlayFabSuccCallback();
    }

    public void AddLoginWithPlayFabFailListener(UnityAction listener){
        m_OnLoginWithPlayFabFailCallback += listener;
    }

    public void RemoveLoginWithPlayFabFailListener(UnityAction listener){
        m_OnLoginWithPlayFabFailCallback -= listener;
    }

    void OnLoginWithPlayFabFailure(){
        if (m_OnLoginWithPlayFabFailCallback != null)
            m_OnLoginWithPlayFabFailCallback();
    }

    // Add credentioals 
    UnityAction m_OnAddCredentialsSuccCallback; 
    UnityAction m_OnAddCredentialsFailCallback;

    public void AddCredentialsSuccessListener(UnityAction listener){
        m_OnAddCredentialsSuccCallback += listener;
    }

    public void RemoveCredentialsSuccessListener(UnityAction listener){
        m_OnAddCredentialsSuccCallback -= listener;
    }

    void OnAddCredentialsSuccess(){
        if (m_OnAddCredentialsSuccCallback != null)
            m_OnAddCredentialsSuccCallback();
    }

    public void AddCredentialsFailListener(UnityAction listener){
        m_OnAddCredentialsFailCallback += listener;
    }

    public void RemoveCredentialsFailListener(UnityAction listener){
        m_OnAddCredentialsFailCallback -= listener;
    }

    void OnAddCredentialsFailure(){
        if (m_OnAddCredentialsFailCallback != null)
            m_OnAddCredentialsFailCallback();
    }
#endregion


// Anonymos login
    public virtual void SendLogin(){

        Debug.Log("SendLogin ");

        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            PlayFabSettings.staticSettings.TitleId = c_TitleId; 
        }
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true}; // TODO future - don't use LoginWithCustomIDRequest in production 
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    protected void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");

        OnLoginSuccess(result.PlayFabId, result.NewlyCreated);
    }

    protected void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(" + error.ErrorMessage);
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());

        OnLoginFailure();
    }


// Registration 

    static string c_testEmail = "margarita@unnyhog.com";
    static string c_testName = "Shahdee";
    static string c_testPass = "12345678";
    

    public virtual void SendRegister(){

        Debug.Log("SendRegister ");

        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            PlayFabSettings.staticSettings.TitleId = c_TitleId; 
        }
        var request = new RegisterPlayFabUserRequest{
            TitleId = PlayFabSettings.staticSettings.TitleId,
            Password = c_testPass,
            Username = c_testName,
            Email = c_testEmail
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);

    }

    protected void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("OnRegisterSuccess ");

        OnRegisterSuccess();
    }

    protected void OnRegisterFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong " + error.ErrorMessage);
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());

        OnRegisterFailure();
    }

    // Login with PlayFab

    public virtual void SendLoginWithPlayFab(){
         Debug.Log("SendLoginWithPlayFab ");

        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            PlayFabSettings.staticSettings.TitleId = c_TitleId; 
        }
        var request = new LoginWithPlayFabRequest{
            TitleId = PlayFabSettings.staticSettings.TitleId,
            Username = c_testName,
            Password = c_testPass
            // email 
        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginWithPlayFabSuccess, OnLoginWithPlayFabFailure);
    }

    void OnLoginWithPlayFabSuccess(LoginResult result){
        Debug.Log("OnLoginWithPlayFabSuccess ");

        OnLoginWithPlayFabSuccess();
    }

    void OnLoginWithPlayFabFailure(PlayFabError error){
        Debug.LogWarning("Something went wrong " + error.ErrorMessage);
        Debug.LogError("Here's some debug information: ");
        Debug.LogError(error.GenerateErrorReport());

        OnLoginWithPlayFabFailure();
    }

    // Add credentials to bind anonymos account with PlayFab account 
    public virtual void SendAddUsernamePassword(){
         Debug.Log("SendAddUsernamePassword ");

        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            PlayFabSettings.staticSettings.TitleId = c_TitleId; 
        }
        var request = new AddUsernamePasswordRequest{
            Username = c_testName,
            Password = c_testPass,
            Email = c_testEmail
        };
        PlayFabClientAPI.AddUsernamePassword(request, OnAddCredentialsSuccess, OnAddCredentialsFailure);
    }

    void OnAddCredentialsSuccess(AddUsernamePasswordResult result){
        Debug.Log("OnAddCredentialsSuccess ");

        OnAddCredentialsSuccess();
    }

    void OnAddCredentialsFailure(PlayFabError error){
        Debug.LogWarning("Something went wrong " + error.ErrorMessage);
        Debug.LogError("Here's some debug information: ");
        Debug.LogError(error.GenerateErrorReport());

        OnAddCredentialsFailure();
    }

}
