using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile
{

    // List<int> m_UnlockedBags;

    string m_PlayerId;

    public PlayerProfile(MainLogic logic){

        // m_UnlockedBags = new List<int>();
    }

    public void SetPlayerId(string id){
        m_PlayerId = id;
    }

   // save / load - TODO future
}
