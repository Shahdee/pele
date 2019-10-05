using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBorder : UIObject
{
    public BoxCollider2D m_BoxCollider;
    Vector2 m_VecSize;

   public void Awake(){

    //    Debug.Log("RTrs border" + m_RectTransform.sizeDelta.x + " / " + m_RectTransform.sizeDelta.y);
    //    Debug.Log("RTrs anchor min/max" + m_RectTransform.anchorMin + " / " + m_RectTransform.anchorMax);
    //    Debug.Log("RTrs offset min/max" + m_RectTransform.offsetMin + " / " + m_RectTransform.offsetMax);
    //    Debug.Log("RTrs rect" + m_RectTransform.rect);

       m_VecSize.x = m_RectTransform.rect.width;
       m_VecSize.y = m_RectTransform.rect.height;

       m_BoxCollider.size = m_VecSize;

   }
}
