using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabConnectAndroid: PlayFabConnect 
{
    public override void SendLogin(){

    //  Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
            PlayFabSettings.staticSettings.TitleId = c_TitleId; 
        }
        var request = new LoginWithAndroidDeviceIDRequest {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier, // Settings.Secure.ANDROID_ID
            AndroidDevice = SystemInfo.deviceModel,
            TitleId = PlayFabSettings.staticSettings.TitleId
        };

        PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLoginSuccess, OnLoginFailure);
    }
}
