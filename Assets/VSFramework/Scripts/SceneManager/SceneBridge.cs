/*******************************************************************************
 *Copyright(C) 2017 by 八点
 *FileName:    SceneBridge
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-06 11:22:27
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 场景桥基类
    /// </summary>
	public abstract class SceneBridge : Msg
	{
        /// <summary>
        /// 需要传递的场景
        /// </summary>
        public abstract SceneType sceneType { get; }

	}
}

