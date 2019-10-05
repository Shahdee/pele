using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBall : UIObject
{
    // show arrow whenever dragged 
    public ComplexButton m_ButtonHandler;
    public Rigidbody2D m_RigidBody;

    public void Strike(Vector2 directionNorm, float magnitude){

        Debug.LogError("Strike directionNorm " + directionNorm + " / magnitude " + magnitude);

        m_RigidBody.AddForce(directionNorm * magnitude, ForceMode2D.Force);
    }
}
