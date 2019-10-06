using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabConnectIOS : PlayFabConnect
{
    public override void SendLogin(){
        //  // Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        // if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)){
        //     PlayFabSettings.staticSettings.TitleId = "144"; // Please change this value to your own titleId from PlayFab Game Manager
        // }

        // var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true};
        // PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLoginSuccess, OnLoginFailure);
    }
}
