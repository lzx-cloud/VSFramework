/*******************************************************************************
 *Copyright(C) 2017 by 八点 
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
        protected UIPanel _panel;

        protected UIManager _uiManager;

        /// <summary>
        /// UI层级
        /// </summary>
        public abstract UILayer layer { get; }

        /// <summary>
        /// UI面板
        /// </summary>
        public UIPanel panel
        {
            get
            {
                if (this._panel == null)
                {
                    Debug.LogError("请设置" + typeof(UIPanel).Name);
                }
                return this._panel;
            }
        }

        /// <summary>
        /// 对应的UIManager
        /// </summary>
        public UIManager uiManger
        {
            get
            {
                return _uiManager;
            }
        }

        #endregion

        #region 内置函数

        protected virtual void OnDestroy()
        {
            if (this._uiManager)
            {
                this.uiManger.VSFReleasePanel(this);
            }
        }

        #endregion

        #region VSF方法

        /// <summary>
        /// Panel的Awake函数，UIManger执行Awake会调用此函数
        /// </summary>
        /// <param name="uiManger"></param>
        public virtual void VSFAwake(UIManager uiManger)
        {
            this._uiManager = uiManger;
        }

        /// <summary>
        /// Panel的Start函数，UIManger执行Start会调用此函数
        /// </summary>
        public virtual void VSFStart() { }

        /// <summary>
        /// 打开UI
        /// </summary>
        public virtual void VSFOpen()
        {
            if (this.panel)
            {
                this.panel.VSFOpen();
            }
            else
            {
                string log = string.Format("请设置{0}UIPanel", this.name);
                Debug.LogError(log);
            }
        }

        /// <summary>
        /// 打开UI
        /// </summary>
        /// <param name="content"></param>
        public virtual void VSFOpen(string content){}

        /// <summary>
        /// 关闭UI
        /// </summary>
        public virtual void VSFHidden()
        {
            if (this.panel)
            {
                this.panel.VSFClose();
            }
            else
            {
                string log = string.Format("请设置{0}UIPanel", this.name);
                Debug.LogError(log);
            }
        }

        /// <summary>
        /// 当关闭时，调用此方法打开
        /// 当打开时，调用此方法关闭
        /// </summary>
        public virtual void VSFToggle()
        {
            if (this.panel)
            {
                this.panel.VSFToggle();
            }
            else
            {
                string log = string.Format("请设置{0}UIPanel", this.name);
                Debug.LogError(log);
            }
        }


        #endregion



#if UNITY_EDITOR

        protected virtual void Reset()
        {
            this.VSFReset();
        }

        [ContextMenu("VSFReset")]
        protected virtual void VSFReset()
        {
            this.name = this.GetType().Name;

            if (this._panel == null)
            {
                GameObject go = new GameObject("UIPanel", typeof(UIPanel), typeof(UnityEngine.UI.Image));
                go.transform.SetParent(transform);

                RectTransform rt = go.GetComponent<RectTransform>();
                rt.localScale = Vector3.one;
                rt.localPosition = Vector3.zero;
                rt.sizeDelta = Vector2.zero;

                UnityEngine.UI.Image image = go.GetComponent<UnityEngine.UI.Image>();
                if (image.sprite == null)
                {
                    image.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
                    image.type = UnityEngine.UI.Image.Type.Sliced;
                    rt.sizeDelta = new Vector2(500, 300);
                }

            }

        }


#endif


    }

    /// <summary>
    /// UI面板泛型类
    /// </summary>
    public abstract class BasePanel<T> : BasePanel where T : Msg
    {
        #region 字段和属性

        protected T _data;

        /// <summary>
        /// Panel数据
        /// </summary>
        public T data
        {
            get
            {
                return _data;
            }
        }

        #endregion

        #region VSF方法

       /// <summary>
       /// 更新Msg数据
       /// </summary>
       /// <param name="data"></param>
        public virtual void VSFUpdateMsg(T data)
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
    public abstract class BasePanel<T,U> : BasePanel<T> where T : Msg where U : Config
    {
        #region 字段和属性

        protected U _config;
        /// <summary>
        /// 该Panel对应的配置数据
        /// </summary>
        public U config
        {
            get
            {
                return _config;
            }
        }

        #endregion

        #region 方法

        public virtual void VSFUpdateMsg(U config)
        {
            this._config = config;
        }

        #endregion

    }

}

