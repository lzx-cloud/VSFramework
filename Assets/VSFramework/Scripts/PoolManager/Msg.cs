/*******************************************************************************
 *Copyright(C) 2017 by 8Point 
 *All rights reserved. 
 *FileName:    Msg
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-06 15:51:11
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 传递消息类
    /// </summary>
	public abstract class Msg
	{
        /// <summary>
        /// 传递的消息类型
        /// </summary>
        public MsgType msgType { get; }
	}
}

