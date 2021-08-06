/*******************************************************************************
 *Copyright(C) 2017 by 8Point 
 *All rights reserved. 
 *FileName:    SceneControllerSingleton
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-06 11:22:52
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 场景管理类单例类
    /// </summary>
	public abstract class SceneControllerSingleton<T> : SceneController where T : SceneControllerSingleton<T>
	{
        /// <summary>
        /// 单例实例
        /// </summary>
        public static T Instance;

        protected override void VFSAwake()
        {
            base.VFSAwake();
            Instance = this as T;
        }

    }
}

