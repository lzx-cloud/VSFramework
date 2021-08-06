/*******************************************************************************
 *Copyright(C) 2017 by 8Point 
 *All rights reserved. 
 *FileName:    UIManagerSingleton
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-05 18:22:12
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// UI管理类单例模式
    /// </summary>
	public class UIManagerSingleton<T> : UIManager where T : UIManagerSingleton<T>
    {
        #region 字段和属性

        public static T Instance;

        #endregion

        #region 方法

        protected override void Awake()
        {
            base.Awake();
        }

        #endregion

    }
}

