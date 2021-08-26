/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ManagerSingleton
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-05 18:27:20
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 管理类单例 
    /// </summary>
	public class ManagerSingleton<T> : Manager where T : ManagerSingleton<T>
    {
        public static T Instance;

        public override void VSFAwake()
        {
            base.VSFAwake();
            Instance = this as T;
        }

    }
}

