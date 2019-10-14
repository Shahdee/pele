using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArea : UIObject, IUpdatable
{
    public List<UIEnemy> m_Enemies;

    public void SetEnemies(int count){

        if (m_Enemies == null) return;

        for (int i=0; i<m_Enemies.Count; i++){
            m_Enemies[i].Show(i < count);
        }
    }
    
    public void UpdateMe(float deltaTime){

    }
}
