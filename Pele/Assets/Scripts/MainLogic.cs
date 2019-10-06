﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// model 

public class MainLogic : MonoBehaviour
{
    public GUILogic m_GUILogic;
    public EntityManager m_EntityManager;
    public InputManager m_InputManager;

    DataLoader m_DataLoader;
    LevelLogic m_LevelLogic;
    PlayerProfile m_Profile;

    PlayFabConnect m_PlayfabConnect;

    public DataLoader GetDataLoader(){
        return m_DataLoader;
    }

    public EntityManager GetEntityManager(){
        return m_EntityManager;
    }

    public InputManager GetInputManager(){
        return m_InputManager;
    }

    public LevelLogic GetLevelLogic(){
        return m_LevelLogic;
    }

    public PlayerProfile GetPlayerProfile(){
        return m_Profile;
    }

    void Start()
    {
        m_DataLoader = new DataLoader(this);
        m_LevelLogic = new LevelLogic(this);
        m_Profile = new PlayerProfile(this);

        InitBackend();

        m_InputManager.Init(this);
        m_EntityManager.Init(this);
        
        m_GUILogic.Init(this);
    }

#region Backend 
    void InitBackend(){

#if UNITY_EDITOR || UNITY_STANDALONE
        m_PlayfabConnect = new PlayFabConnect();
#elif UNITY_IOS
        m_PlayfabConnect = new PlayFabConnectIOS();
#elif UNITY_ANDROID
        m_PlayfabConnect = new PlayFabConnectAndroid();       
#endif
        m_PlayfabConnect.AddLoginSuccessListener(OnLoginSuccess);
        m_PlayfabConnect.AddLoginFailListener(OnLoginFail);

        m_PlayfabConnect.SendLogin();
    }

    void OnLoginSuccess(string playerId, bool newlyCreated){

        Debug.LogError("OnLoginSuccess " + playerId + " / " + newlyCreated);

        if (newlyCreated){
            m_PlayfabConnect.SendRegister();

        }
        else{
            m_PlayfabConnect.SendLoginWithPlayFab();
        }
    }

    void OnLoginFail(){
        // TODO should retry several times before giving up 
    }

#endregion

    public void StartGame(){

        // TODO - set level according to players progress 
        // playfab or player prefs 

        m_LevelLogic.StartLevel(0);
    }

    public void TrackInput(bool track){
        m_InputManager.TrackInput(track);
    }

    public void MoveNext(){

        m_LevelLogic.MoveNext(); 
    }

    float deltaTime = 0;

     void Update(){
        deltaTime = Time.deltaTime;

        m_InputManager.UpdateMe(deltaTime);
        m_LevelLogic.UpdateMe(deltaTime);
        m_GUILogic.UpdateMe(deltaTime);
    }
}
