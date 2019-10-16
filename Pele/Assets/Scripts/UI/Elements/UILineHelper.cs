using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UILineHelper : UIObject, IUpdatable, IInitable
{
   public BoxCollider2D m_BoxCollider;

   const float c_CastDistance = 3000; 

   const int c_MaxHitCount = 10; // empirical
   RaycastHit2D[] m_HitResults = new RaycastHit2D[c_MaxHitCount];
   int m_CastCount = 0;

   Vector2 m_VecSize;

   public void Init(MainLogic logic){
      m_VecSize = m_RectTransform.sizeDelta;
   }

   public void UpdateMe(float delteTime){
       
   }

   public void Rotate(float angle){
      m_RectTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
   }

   public void CastLine(Vector2 direction){
      m_CastCount = m_BoxCollider.Raycast(direction, m_HitResults, c_CastDistance);

      if (m_CastCount > 0){

         for (int i=0; i<m_HitResults.Length; i++){

            if (m_HitResults[i].collider != null){

               if (m_HitResults[i].collider.gameObject == m_RectTransform.parent.gameObject) continue;

               // Debug.Log("m_HitResults " + m_HitResults[i].collider.gameObject.name + " / " + m_HitResults[i].distance);

               SetLineLength(m_HitResults[i].distance);
               break;
            }
         }
      }
   }

   // TODO future - distance we get from RayCast, has to be scaled by the coeff. which is used in Canvas    
   void SetLineLength(float length){
      m_VecSize.y = length / GUILogic.GetScale();
      m_RectTransform.sizeDelta = m_VecSize;
   }
}
