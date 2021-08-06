/*******************************************************************************
 *Copyright(C) 2017 by 8Point 
 *All rights reserved. 
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
        /// <summary>
        /// 获取场景类型
        /// </summary>
        public abstract SceneType sceneType 
        {
            get;
        }

        #region
        #endregion

        #region
        #endregion

        #region
        #endregion

        private void Awake()
        {
            this.VFSAwake();
        }

        private void Start()
        {
            this.VFSStart();
            StartCoroutine(VFSLoad());
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void VFSAwake() { }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void VFSStart() { }

        /// <summary>
        /// 异步初始化，在VFSStart之后执行
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator VFSLoad();


        protected virtual void OnDestroy() { }

	}
}

