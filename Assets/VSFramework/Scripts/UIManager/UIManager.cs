/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    UIManager
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-05 18:20:56
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace VSFramework
{
    /// <summary>
    /// UI管理类
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class UIManager : MonoBehaviour
    {
        #region 字段属性

        [SerializeField]
        [Header("该UIManager加载的Panel的路径(相对Resources路径)")]
        protected string _panelPath = "UIPanels";
        public string panelPath
        {
            get
            {
                return _panelPath;
            }
        }

        [SerializeField]
        protected List<BasePanel> _panels = new List<BasePanel>();

        #endregion

        #region 内置方法|私有方法

        private void Awake() 
        {
            this.VSFAwake();
        }

        private void Start() 
        {
            this.VSFStart();
        }

        #endregion

        #region VSF方法

        /// <summary>
        /// Awake调用
        /// </summary>
        protected virtual void VSFAwake() 
        {
            this._panels.ForEach(bp =>
            {
                bp.VSFAwake(this);
            });
        }

        /// <summary>
        /// Start调用
        /// </summary>
        protected virtual void VSFStart() 
        {
            this._panels.ForEach(bp =>
            {
                bp.VSFStart();
            });
        }


        public T VSFGetPanel<T>() where T : BasePanel
        {
            string typeName = typeof(T).Name;
            BasePanel panel = this._panels.Find(item =>
            {
                return item.name == typeName;
            });

            if (panel == null)
            {
                string path = string.Format("{0}/{1}", panelPath, typeName);
                T temp = Resources.Load<T>(path);
                T t = Instantiate<T>(temp);
                t.transform.SetParent(this.transform);
                RectTransform rt1 = t.GetComponent<RectTransform>();
                RectTransform rt2 = temp.GetComponent<RectTransform>();

                rt1.anchoredPosition = temp.transform.position;   //设置位置
                t.transform.localScale = temp.transform.localScale;
                rt1.offsetMin = rt2.offsetMin;
                rt1.offsetMax = rt2.offsetMax;  //设置锚点
                t.name = typeName;
                t.VSFAwake(this);
                t.VSFStart();

                this._panels.Add(t);

                this.UpdateLayer();  //更新UI层级关系

            }
            return panel as T;
        }

        /// <summary>
        /// 销毁Panel
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="panel"></param>
        public void VSFReleasePanel<T>(T panel) where T : BasePanel
        {
            this._panels.Remove(panel);
        }

        /// <summary>
        /// 更新UI层级
        /// </summary>
        protected virtual void UpdateLayer()
        {
            for (int i = 0; i < _panels.Count; i++)
            {
                for (int j = 0; j < _panels.Count - i - 1; j++)
                {
                    if (this._panels[j].layer > _panels[j + 1].layer)
                    {
                        BasePanel bp = this._panels[j];
                        this._panels[j] = this._panels[j + 1];
                        this._panels[j + 1] = bp;
                    }
                }
            }

            for (int i = 0; i < this._panels.Count; i++)
            {
                this._panels[i].transform.SetSiblingIndex(i);
            }

        }

        #endregion

#if UNITY_EDITOR
        void Reset()
        {
            this.name = GetType().Name;
            CanvasScaler cs = GetComponent<CanvasScaler>();
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;  //自动设置模式
            cs.referenceResolution = new Vector2(1920, 1080);    //自动设置分辨率
            SummaryUIPanels();
        }

        [ContextMenu("SummaryUIPanels")]
        protected virtual void SummaryUIPanels()
        {
            this._panels.Clear();
            BasePanel[] panels = GetComponentsInChildren<BasePanel>();
            for (int i = 0; i < panels.Length; i++)
            {
                BasePanel bp = panels[i];
                this._panels.Add(bp);
            }
            this.UpdateLayer();  //更新层级关系
        }

#endif

    }
}

