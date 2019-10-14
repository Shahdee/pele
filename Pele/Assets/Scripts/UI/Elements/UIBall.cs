using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBall : UIObject, IUpdatable, IInitable
{
    
    public ComplexButton m_ButtonHandler;
    public Rigidbody2D m_RigidBody;
    public Collider2D m_Collider;
    public Text m_TxtLives;
    public UILineHelper m_LineHelper;
    public Image m_ImgBall;
    public float m_ExplodeTime = 3f;
    public float m_MinVelocityCoeff = 0.1f;
    public float m_BallDeathBlinkSpeed = Mathf.PI*2; // 2pi per sec 

#region Callbacks 

    //Death
    UnityAction m_OnBallDeathCallback;

    public void AddBallDeathListener(UnityAction listener){
        m_OnBallDeathCallback += listener;
    }

    public void RemoveBallDeathListener(UnityAction listener){
        m_OnBallDeathCallback -= listener;
    }

    void OnBallDeath(){
        if (m_OnBallDeathCallback != null)
            m_OnBallDeathCallback();
    }

    //Explode
    UnityAction m_OnBallExplodeCallback;

    public void AddBallExplodeListener(UnityAction listener){
        m_OnBallExplodeCallback += listener;
    }

    public void RemoveBallExplodeListener(UnityAction listener){
        m_OnBallExplodeCallback -= listener;
    }

    void OnBallExplode(){
        if (m_OnBallExplodeCallback != null)
            m_OnBallExplodeCallback();
    }
#endregion

    int m_Lives = 0;

    public void Init(MainLogic logic){
        m_LineHelper.Init(logic);
    }

    public void Strike(Vector2 directionNorm, float magnitude){

        // Debug.Log("Strike directionNorm " + directionNorm + " / magnitude " + magnitude);

        m_RigidBody.AddForce(directionNorm * magnitude, ForceMode2D.Force);
    }

    Vector2 m_TmpPos;

    public void Restore(){
        SetIniPosition();
        SetIniColor();
        StopPhysics();
    }

    void SetIniColor(){
        m_BallColor = m_ImgBall.color;
        m_BallColor.a = 1;
        m_ImgBall.color = m_BallColor;
    }
    
    void SetIniPosition(){
        m_TmpPos.x = 0;
        m_TmpPos.y = 0;

        m_RectTransform.anchoredPosition = m_TmpPos;
    }

    public void SetLives(int lives){
        m_Lives = lives;
        m_TxtLives.text = m_Lives.ToString();
    }

    void TakeLife(){
        if (m_Lives > 0){
            m_Lives--;
            m_TxtLives.text = m_Lives.ToString();

            if (m_Lives == 0){
                m_TxtLives.text = "";
                OnBallDeath();
                Explode();
            }
        }
    }

    bool m_Exploding = false;
    float m_CurrExplodeTime = 0;

    void Explode(){
        MinimizeVelocity();
        SetExploding();
    }

    public bool isDead(){
        return m_Exploding;
    }

    Vector2 m_TmpVelocity;

    void MinimizeVelocity(){
        m_TmpVelocity = m_RigidBody.velocity;
        m_TmpVelocity.x *= m_MinVelocityCoeff;
        m_TmpVelocity.y *= m_MinVelocityCoeff;
        m_RigidBody.velocity = m_TmpVelocity;
    }

    void StopPhysics(){
        m_RigidBody.velocity = Vector2.zero;
    }

    void SetExploding(){
        m_Exploding = true;
        m_CurrExplodeTime = m_ExplodeTime;

        m_BallColor = m_ImgBall.color;
        m_currAngle = 0;
    }

    public void UpdateMe(float deltaTime){
        UpdateExplosion(deltaTime);
    }

    void UpdateExplosion(float deltaTime){
        if (! m_Exploding) return;

        UpdateBlink(deltaTime);

        if (m_CurrExplodeTime > 0)
            m_CurrExplodeTime -= deltaTime;
        else{
            m_Exploding = false;
            OnBallExplode();
        }
    }

    Color m_BallColor; 
    float m_currAngle = 0;

    void UpdateBlink(float deltaTime){
        m_BallColor.a = (Mathf.Cos(m_currAngle*Mathf.PI) + 1) * 0.5f; // sin changes here on [0, 1] interval and I multiply by PI, to inc the frequency
        m_ImgBall.color = m_BallColor;

        m_currAngle += m_BallDeathBlinkSpeed * deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == m_LineHelper.gameObject) return; 

        // Debug.Log("enter " + collision.gameObject.name);

        TakeLife();
    }

    // void OnCollisionExit2D(Collision2D collision)
    // {
    //    Debug.Log("exit");
    // }
}
