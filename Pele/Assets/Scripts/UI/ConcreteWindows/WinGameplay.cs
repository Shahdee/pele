using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class WinGameplay : WinViewBase
{
    // enemies - moving 
    // my ball 
    // in a certain moment I can drag the ball  
    // fields 

    public UIBall m_Ball;

    protected override void InInit(){

        m_MainLogic.GetInputManager().AddTouchBeginListener(TouchBegin);
        m_MainLogic.GetInputManager().AddTouchMoveListener(TouchMove);
        m_MainLogic.GetInputManager().AddTouchEndListener(TouchEnd);
        
        // m_Progress.Init();
        // m_Lock.m_BtnTry.m_OnBtnClickClbck += TryClick;

        m_Ball.m_ButtonHandler.m_OnBtnDownClbck += OnBallDown;
        m_Ball.m_ButtonHandler.m_OnBtnUpClbck += OnBallUp;
    }

    protected override WinControllerBase CreateController(){
        return new WinGameplayController(m_MainLogic, this);;
    }


    // when the level starts 
    protected override void OnShow(){

        ResetUI();

        (m_Controller as WinGameplayController).SendTrackInput(true); // for test here
    }

    void ResetUI(){

        // put ball on initial position 
        // ask gameman to handle swipes 


    }

    bool m_BallTouched = false;
    bool m_TouchBegin = false;

    void OnBallDown(ComplexButton btn){
        // Debug.LogError("Ball down");

        m_BallTouched = true;
    }

    void OnBallUp(ComplexButton btn){
        // Debug.LogError("Ball up");

    }


    void TouchBegin(Vector2 pos){
        // touch input and ui ball 

        // Debug.LogError("TouchBegin");

        m_TouchBegin = true;
    }

    void TouchMove(Vector2 pos){
        // Debug.Log("TouchMove");
    }

    public float c_StrikeMagnitudeMax = 500; // should be % from screen 
    public float c_StrikeMagnitudeMin = 100; // should be % from screen 
    
    float m_StrikeMagnitude = 0; 

    void TouchEnd(Vector2 dir){

        // Debug.LogError("TouchEnd");

        if (m_BallTouched && m_TouchBegin){

            m_StrikeMagnitude = Mathf.Clamp(dir.magnitude, c_StrikeMagnitudeMin, c_StrikeMagnitudeMax);

            m_Ball.Strike(dir.normalized, m_StrikeMagnitude);

            m_BallTouched = false;
            m_TouchBegin = false;
        }

        // if ball is selected => strike 
    }

    protected override void OnHide(){

    }

    void TryClick(GUIButtonBase btn){

        // string pin = m_Lock.GetPin();

        // Debug.Log("Try " + pin);

        // (m_Controller as WinGameplayController).SendTryUnlock(pin);
    }

    // // bag appears on screen
    // void OnWaitBagStart(){

    //     // Debug.Log("OnWaitBagStart");
        
    //     m_CurrBag.Show(true);
    //     m_CurrBag.SetBagState(UIBag.BagState.Locked);
    // }


    // // you have limited time to try to unlock the bag 
    // void OnBagUnlockStart(){

    //     // Debug.Log("OnBagUnlockStart");

    //     ShowQuestion();

    //     m_Progress.Show(true);
    //     m_Lock.Show(true);

    //     SetAttemptTimer(true);
    // }

    // void ShowQuestion(){
    //     // var bag = m_MainLogic.GetLevelLogic().GetCurrentBag();
    //     // var question = m_MainLogic.GetLevelLogic().GetCurrentQuestion();

    //     // m_Question.Show(true);
    //     // m_Question.SetHint(question.m_QuestionHint);
    // }

    bool m_AttemptTimer = false;
    float m_CurrentAtemptTimer = 0;

    void SetAttemptTimer(bool start){
        m_AttemptTimer = start;
        if (start)
            m_CurrentAtemptTimer = LevelLogic.GetUnlockAttempTime();
    }

    static float c_MoveNextTime = 2f;
    float m_CurrMoveNextTime = 0;

    // bool m_MoveNextTimer = false;

    // void SetMoveNextTimer(){
    //     m_MoveNextTimer = true;
    //     m_CurrMoveNextTime = c_MoveNextTime;
    // }

    // // you lost!
    // void OnBagUnlockTimeIsUp(){
    //     m_CurrBag.SetBagState(UIBag.BagState.Blocked);
    //     m_Lock.Show(false);

    //     SetAttemptTimer(false);
    //     SetMoveNextTimer();
    // }

    // // you won!
    // void OnBagUnlocked(){

    //     m_CurrBag.SetBagState(UIBag.BagState.Unlocked);
    //     SetAttemptTimer(false);

    //     SetUnlockedBags();

    //     SetMoveNextTimer();
    // }

    // void OnBagUnlockAttemptFailed(){
    //     m_CurrBag.ShowFail(true);
    // }

    public override void UpdateMe(float deltaTime){

        // UpdateProgress(deltaTime);
    }

    void AskForNext(){
        ResetUI();

        (m_Controller as WinGameplayController).SendMoveNext();
    }

    // void UpdateProgress(float deltaTime){

    //     if (m_AttemptTimer){

    //         m_CurrentAtemptTimer -= deltaTime;

    //         m_Progress.SetProgress(m_CurrentAtemptTimer/LevelLogic.GetUnlockAttempTime());

    //         if (m_CurrentAtemptTimer < 0){

    //             m_AttemptTimer = false;
    //         }
    //     }

    //     if (m_MoveNextTimer){

    //         m_CurrMoveNextTime -= deltaTime;

    //         if (m_CurrMoveNextTime < 0){

    //             m_MoveNextTimer = false;

    //             AskForNext();
    //         }
    //     }

    //     m_CurrBag.UpdateMe(deltaTime);
    // }
}
