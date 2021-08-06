/*******************************************************************************
 *Copyright(C) 2017 by 8Point 
 *All rights reserved. 
 *FileName:    UIMaskPanel
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-05 18:51:44
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace VSFramework
{
    /// <summary>
    /// UI遮罩
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UIMaskPanel : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region 属性

        public UnityAction<PointerEventData> ClickAction; 

        public UnityAction<PointerEventData> BeginDragAction;

        public UnityAction<PointerEventData> DragAction;

        public UnityAction<PointerEventData> EndDragAction;

        protected CanvasGroup mask
        {
            get
            {
                return GetComponent<CanvasGroup>();
            }
        }

        public bool isMask
        {
            set
            {
                mask.blocksRaycasts = value;
            }
            get
            {
                return mask.blocksRaycasts;

            }
        }

        #endregion

        #region 方法

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ClickAction != null)
            {
                ClickAction(eventData);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (BeginDragAction != null)
            {
                BeginDragAction(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (DragAction != null)
            {
                DragAction(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (EndDragAction != null)
            {
                EndDragAction(eventData);
            }
        }

        //public static UIPanelMask Create(Transform trans)
        //{
        //    Transform parent = trans;
        //    Canvas canvas = null;
        //    while (parent != null && canvas == null)
        //    {
        //        canvas = parent.GetComponent<Canvas>();
        //        parent = parent.parent;
        //    }

        //    string p = path + typeof(UIPanelMask).Name;
        //    UIPanelMask go = Resources.Load<UIPanelMask>(p);
        //    UIPanelMask result = Instantiate<UIPanelMask>(go);
        //    result.transform.SetParent(canvas.transform);
        //    result.transform.localScale = Vector3.one;
        //    RectTransform rt = result.GetComponent<RectTransform>();
        //    RectTransform rt2 = go.GetComponent<RectTransform>();
        //    rt.transform.position = Vector3.zero;
        //    rt.offsetMin = rt2.offsetMin;
        //    rt.offsetMax = rt2.offsetMax;
        //    rt.name = typeof(UIPanelMask).Name;
        //    result.transform.SetParent(trans);
        //    return result;
        //}

        #endregion
    }
}

