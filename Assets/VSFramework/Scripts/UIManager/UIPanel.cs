/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    UIPanel
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
    /// UI面板
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region 属性

        /// <summary>
        /// 点击事件
        /// </summary>
        public UnityAction<PointerEventData> ClickAction;

        /// <summary>
        /// 拖拽事件
        /// </summary>
        public UnityAction<PointerEventData> BeginDragAction;

        /// <summary>
        /// 拖拽事件
        /// </summary>
        public UnityAction<PointerEventData> DragAction;

        /// <summary>
        /// 结束拖拽事件
        /// </summary>
        public UnityAction<PointerEventData> EndDragAction;

        protected CanvasGroup mask
        {
            get
            {
                return this.GetComponent<CanvasGroup>();
            }
        }

        /// <summary>
        /// 是否遮罩
        /// </summary>
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

        /// <summary>
        /// 打开UIPanel
        /// </summary>
        public virtual void VSFOpen() 
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 关闭Panel
        /// </summary>
        public virtual void VSFClose() 
        {
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// 当关闭时，调用此方法打开
        /// 当打开时，调用此方法关闭
        /// </summary>
        public virtual void VSFToggle() 
        {
            bool activeSelf = this.gameObject.activeSelf;
            this.gameObject.SetActive(!activeSelf);
        }

        #endregion

        #region 接口继承

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

        #endregion

    }

}