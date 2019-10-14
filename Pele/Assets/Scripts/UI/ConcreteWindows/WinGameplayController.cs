using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameplayController : WinControllerBase
{
    public WinGameplayController(MainLogic logic, WinViewBase view) : base(logic, view){
       
    }

    public void SendMoveNext(){
        m_MainLogic.MoveNext();
    }

    public void SendRestartLevel(){
        m_MainLogic.RestartLevel();
    }

    public void SendTrackInput(bool track){
        m_MainLogic.TrackInput(track);
    }
}
