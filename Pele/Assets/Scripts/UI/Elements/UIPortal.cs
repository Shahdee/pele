using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPortal : UIObject, IUpdatable
{
    UnityAction m_OnBallCatchCallback;

    public void AddBallCatchListener(UnityAction listener){
        m_OnBallCatchCallback += listener;
    }

    public void RemoveBallCatchListener(UnityAction listener){
        m_OnBallCatchCallback -= listener;
    }

    void OnBallCatch(){
        if (m_OnBallCatchCallback != null)
            m_OnBallCatchCallback();
    }
    
    public void UpdateMe(float deltaTime){
        UpdateCatching(deltaTime);
    }

    void UpdateCatching(float deltaTime){
        if (! m_Catching) return;

         if (m_CurrCatchTime > 0){
            m_CurrCatchTime -= deltaTime;
        }
        else{
            m_Catching = false;
            OnBallCatch();
        }
    }

    const float c_CatchTime = 0.9f;
    bool m_Catching = false;
    float m_CurrCatchTime = 0;

    void SetCatching(){
        m_Catching = true;
        m_CurrCatchTime = c_CatchTime;
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.isTrigger) return; // this way I ignore line helper triggering level change 

        // Debug.Log("OntriggerEnter " + other.gameObject.name);  

        SetCatching();  
    }

    void OnTriggerExit2D(Collider2D other) {
        
    }
}
