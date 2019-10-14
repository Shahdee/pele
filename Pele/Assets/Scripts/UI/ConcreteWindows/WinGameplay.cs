using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 
public class WinGameplay : WinViewBase
{
    public UIBall m_Ball;
    public List<UIBorder> m_Borders;
    public List<UIArea> m_HostileAreas;
    public UIArea m_HomeArea;
    public UIPortal m_Portal;
    public Text m_Level;
    public float m_StrikeMagnitudeMax = 500; // should be % from screen 
    // public float c_StrikeMagnitudeMin = 100; // should be % from screen 
    // float m_StrikeMagnitude = 0;

    protected override void InInit(){

        m_MainLogic.GetInputManager().AddTouchBeginListener(TouchBegin);
        m_MainLogic.GetInputManager().AddTouchMoveListener(TouchMove);
        m_MainLogic.GetInputManager().AddTouchEndListener(TouchEnd);
        m_MainLogic.GetLevelLogic().AddLevelStartListener(LevelStart);

        for (int i=0; i<m_Borders.Count; i++)
            m_Borders[i].Init(m_MainLogic);

        m_Ball.Init(m_MainLogic);

        m_Ball.m_ButtonHandler.m_OnBtnDownClbck += BallDown;
        m_Ball.m_ButtonHandler.m_OnBtnUpClbck += BallUp;
        m_Ball.m_ButtonHandler.m_OnBtnEnterClbck += BallEnter;
        m_Ball.m_ButtonHandler.m_OnBtnExitClbck += BallExit;

        m_Ball.AddBallDeathListener(BallDeath);
        m_Ball.AddBallExplodeListener(BallExplode);

        m_Portal.AddBallCatchListener(BallCatch);
    }

    protected override WinControllerBase CreateController(){
        return new WinGameplayController(m_MainLogic, this);;
    }

    // when the game starts 
    protected override void OnShow(){
        InitUI();
    }

    void LevelStart(int level){
        InitUI();
    }

    void InitUI(){
        int lives = m_MainLogic.GetLevelLogic().GetLives();
        int level = m_MainLogic.GetLevelLogic().GetCurrentLevel();

        SetLevel(level);

        m_Ball.Restore();
        m_Ball.SetLives(lives);

        InitHostileAreas();

        (m_Controller as WinGameplayController).SendTrackInput(true); 
    }

    void SetLevel(int level){
        m_Level.text = (level + 1).ToString();
    }

    void InitHostileAreas(){

        int[] enemies = m_MainLogic.GetLevelLogic().GetEnemies();

        if (enemies.Length == m_HostileAreas.Count){
              for (int i=0; i<m_HostileAreas.Count; i++)
                m_HostileAreas[i].SetEnemies(enemies[i]);
        }
        else{
            Debug.LogError("wrong num " + enemies.Length + " / " + m_HostileAreas.Count);
        }
    }

    bool m_BallDown = false;
    bool m_BallUp = false;
    bool m_BallEnter = false; // TODO check on mobile 
    bool m_TouchBegin = false;
    bool m_TouchEnd = false;
    Vector2 m_CurrTouchPos;

    // triggered when inside the ball 
    void BallDown(ComplexButton btn){

        if (m_Ball.isDead()) return; // TODO future - optimize. I dont like that I invoke it everywhere 

        // Debug.LogError("Ball down");

        m_BallDown = true;
        m_BallUp = false;

        m_Ball.m_LineHelper.Show(true);
    }


    // triggered regardless of pointer position (inside/outside of the ball)  
    // BUT, triggers only after BallDown 
    void BallUp(ComplexButton btn){

        if (m_Ball.isDead() || !m_BallDown) return; // TODO future - optimize. I dont like that I invoke it everywhere 

        // Debug.Log(" m_BallDown " + m_BallDown);

        m_BallDown = false;
        m_BallUp = true;

        // Debug.LogError("Ball up");

        TryToLaunchOrCancelBall();

        m_Ball.m_LineHelper.Show(false);
    }

    //can be triggered regardless of down
    void BallEnter(ComplexButton btn){ // mobile enter? 

        if (m_Ball.isDead()) return; // TODO future - optimize. I dont like that I invoke it everywhere 

        m_BallEnter = true;
    }

    //can be triggered regardless of down
    void BallExit(ComplexButton btn){

        if (m_Ball.isDead()) return; // TODO future - optimize. I dont like that I invoke it everywhere 

        m_BallEnter = false;
    }

    void BallDeath(){
        (m_Controller as WinGameplayController).SendTrackInput(false);
    }

    void BallExplode(){
        (m_Controller as WinGameplayController).SendRestartLevel();
    }

    void BallCatch(){
        (m_Controller as WinGameplayController).SendMoveNext();
    }

    void TouchBegin(Vector2 pos){
        // Debug.LogError("tb");

        if (m_Ball.isDead()) return; // TODO future - optimize. I dont like that I invoke it everywhere 

        m_CurrTouchPos = pos;

        m_TouchBegin = true;
        m_TouchEnd = false;

        TryToAdjustBallHelpers();
    }

    const float c_ScreenDelta = 0.001f; //percent

    bool isMousePosChanged(Vector2 pos){
        return ((Mathf.Abs(m_CurrTouchPos.x - pos.x) > Screen.width * c_ScreenDelta) ||
                (Mathf.Abs(m_CurrTouchPos.y - pos.y) > Screen.height * c_ScreenDelta));
    }

    void TouchMove(Vector2 pos){

        if (m_Ball.isDead()) return; // TODO future - optimize. I dont like that I invoke it everywhere 

        // Debug.Log("tm " + pos);

        // if (isMousePosChanged(pos)){
            m_CurrTouchPos = pos;
            TryToAdjustBallHelpers();
        // }
    }

    Vector2 m_Dir;
    float m_Angle;
    float c_AngleInitialDisplace = 90; // because initial pos for line is upward, rather than aligned with X axis 

    // when canvas is screenspace overlay,
    // m_RectTransform.position is also in screenspace like mouse 
    // so no need to convert positions 
    void TryToAdjustBallHelpers(){

        if (m_BallDown && m_TouchBegin){

            m_Dir.x = m_Ball.m_RectTransform.position.x - m_CurrTouchPos.x;
            m_Dir.y = m_Ball.m_RectTransform.position.y - m_CurrTouchPos.y;

            m_Angle = Mathf.Atan2(m_Dir.y, m_Dir.x) * Mathf.Rad2Deg - c_AngleInitialDisplace; 

            m_Ball.m_LineHelper.Rotate(m_Angle);
            m_Ball.m_LineHelper.CastLine(m_Dir);
        }
    }

    // if ball is selected => strike 
    void TouchEnd(Vector2 pos){

        // Debug.LogError("te");

        if (m_Ball.isDead() || !m_TouchBegin) return; // TODO future - optimize. I dont like that I invoke it everywhere 

        // Debug.Log(" m_TouchBegin " + m_TouchBegin);

        m_CurrTouchPos = pos;

        m_TouchBegin = false;
        m_TouchEnd = true;

        TryToAdjustBallHelpers();
        TryToLaunchOrCancelBall();
    }

    // allow to launch ball if:
    // finger is outside the ball 
    // we initially clicked on the ball 
    // I assume that exit/enter happens before up/end 
    void TryToLaunchOrCancelBall(){
        if (m_BallUp && m_TouchEnd){
            // cancel
            if (m_BallEnter){
                // Debug.LogError("Cancel strike");

            } //strike 
            else{
                m_Dir.x = m_Ball.m_RectTransform.position.x - m_CurrTouchPos.x;
                m_Dir.y = m_Ball.m_RectTransform.position.y - m_CurrTouchPos.y;

                // m_StrikeMagnitude = Mathf.Clamp(dir.magnitude, c_StrikeMagnitudeMin, c_StrikeMagnitudeMax);
                m_Ball.Strike(m_Dir.normalized, m_StrikeMagnitudeMax);
            }
            ResetKeys();
        }
        // Debug.Log("m_BallDown " + m_BallDown);
        // Debug.Log("m_TouchBegin " + m_TouchBegin);
        // Debug.Log("m_BallEnter " + m_BallEnter);

        // Debug.Log("m_BallUp " + m_BallUp);
        // Debug.Log("m_TouchEnd " + m_TouchEnd);
    }

    void ResetKeys(){
        m_BallUp = false;
        m_BallDown = false;
        m_TouchBegin = false;
        m_TouchEnd = false;
    }

    protected override void OnHide(){

    }

    public override void UpdateMe(float deltaTime){
        m_Ball.UpdateMe(deltaTime);
        m_Portal.UpdateMe(deltaTime);
    }

    void AskForNext(){
        (m_Controller as WinGameplayController).SendMoveNext();
    }
}
