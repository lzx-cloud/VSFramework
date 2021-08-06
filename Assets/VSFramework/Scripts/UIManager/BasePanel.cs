/*******************************************************************************
 *Copyright(C) 2017 by 8Point 
 *All rights reserved. 
 *FileName:    BasePanel
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-05 18:24:05
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// UI面板基类
    /// </summary>
	public abstract class BasePanel : MonoBehaviour
    {
        #region 字段属性

        [SerializeField]
        protected GameObject _body;
        public GameObject body
        {
            get
            {
                return _body;
            }
        }

        protected UIMaskPanel _uiMask;
        public UIMaskPanel uiMask
        {
            get
            {
                if (this._uiMask == null)
                {
                    this._uiMask = this.body.GetComponent<UIMaskPanel>();
                }
                return this._uiMask;
            }
        }

        protected string _content;
        public string content
        {
            get
            {
                return _content;
            }
        }

        /// <summary>
        /// UI层级
        /// </summary>
        public abstract UILayer layer { get; }

        protected UIManager _uiManager;
        public UIManager uiManger
        {
            get
            {
                return _uiManager;
            }
        }

        #endregion

        #region 方法

        public virtual void MInit(UIManager uiManger)
        {
            this._uiManager = uiManger;
        }

        public virtual void MStart() { }

        public virtual void MOpen()
        {
            if (this.body)
            {
                this.body.gameObject.SetActive(true);
            }
            else
            {
                string log = string.Format("请设置{0}的Body", this.name);
                Debug.LogError(log);
            }
        }

        public virtual void MOpen(string content)
        {
            this._content = content;
            this.MOpen();
        }

        public virtual void MHidden()
        {
            if (this.body)
            {
                this.body.gameObject.SetActive(false);
            }
            else
            {
                string log = string.Format("请设置{0}的Body", this.name);
                Debug.LogError(log);
            }
        }

        protected virtual void OnDestroy()
        {
            if (this._uiManager)
            {
                this.uiManger.ReleasePanel(this);
            }
        }

        #endregion

#if UNITY_EDITOR
        protected virtual void Reset()
        {
            this.RReset();
        }

        [ContextMenu("Init")]
        protected virtual void RReset()
        {
            this.name = this.GetType().Name;

            if (this._body == null)
            {
                this._body = new GameObject("Body", typeof(UIMaskPanel), typeof(Image));
                this.body.transform.SetParent(transform);

                RectTransform rt = this.body.GetComponent<RectTransform>();
                rt.localScale = Vector3.one;
                rt.localPosition = Vector3.zero;
                rt.sizeDelta = Vector2.zero;

                Image image = this.body.GetComponent<Image>();
                if (image.sprite == null)
                {
                    image.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
                    image.type = Image.Type.Sliced;
                    rt.sizeDelta = new Vector2(500, 300);
                }

            }

        }


#endif


    }

    /// <summary>
    /// UI面板泛型类
    /// </summary>
    public abstract class BasePanel<T> : BasePanel where T : UIMsg
    {
        #region 字段和属性

        protected T _data;
        public T data
        {
            get
            {
                return _data;
            }
        }

        #endregion

        #region 方法

        public virtual void MOpen(T data)
        {
            this._data = data;
        }

        public virtual void MUpdateMsg(T data)
        {
            this._data = data;
        }

        #endregion

    }

    /// <summary>
    ///  UI面板泛型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public abstract class BasePanel<T, U> : BasePanel where T : UIMsg where U : Config
    {
        #region 字段和属性

        protected T _data;
        public T data
        {
            get
            {
                return _data;
            }
        }

        protected U _config;
        public U config
        {
            get
            {
                return _config;
            }
        }

        #endregion

        #region 方法

        public virtual void MOpen(T data)
        {
            this._data = data;
        }

        public virtual void MUpdateMsg(T data)
        {
            this._data = data;
        }

        public virtual void MUpdateMsg(U config)
        {
            this._config = config;
        }

        #endregion

    }

}

