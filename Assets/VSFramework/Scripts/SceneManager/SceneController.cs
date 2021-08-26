/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    SceneController
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-06 11:22:03
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 场景基类
    /// </summary>
	public abstract class SceneController : MonoBehaviour
	{

        #region 字段

        /// <summary>
        /// 获取场景类型
        /// </summary>
        public abstract SceneType sceneType
        {
            get;
        }

        #endregion

        #region 内置方法


        private void Awake()
        {
            this.VSFAwake();
        }

        private void Start()
        {
            this.VSFStart();
            StartCoroutine(VSFLoad());
        }

        private void OnDestroy() {
            this.VSFDestory();
        }

        #endregion

        #region VSF扩展方法

        /// <summary>
        /// 初始化Awake调用
        /// </summary>
        protected virtual void VSFAwake() { }

        /// <summary>
        /// 初始化Start调用
        /// </summary>
        protected virtual void VSFStart() { }

        /// <summary>
        /// 异步初始化，在VSFStart之后执行
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator VSFLoad();


        protected virtual void VSFDestory() { }

        #endregion

	}
}

