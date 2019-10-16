using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIFingerHelper : UIObject, IUpdatable, IInitable
{
   Vector2 m_VecSize;
   public void Init(MainLogic logic){
      m_VecSize = m_RectTransform.sizeDelta;
   }

   public void UpdateMe(float delteTime){
       
   }

   // TODO future - distance we get from RayCast, has to be scaled by the coeff. which is used in Canvas    
   public void SetLine(float angle, float length){
      m_RectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

      m_VecSize.y = length / GUILogic.GetScale();
      m_RectTransform.sizeDelta = m_VecSize;
   }
}
