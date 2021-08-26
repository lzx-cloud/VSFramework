/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    IOpaAction
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:42:30
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 异步操作事件
    /// </summary>
    public delegate IAsyncOpa AsyncOpaAction();

    /// <summary>
    /// 异步操作事件
    /// </summary>
    public delegate IAsyncOpa<T> AsyncOpaAction<T>(T arg);

    /// <summary>
    /// 异步操作事件
    /// </summary>
    public delegate IAsyncOpa<T0> AsyncOpaAction<T0, T1>(T0 arg0, T1 arg1);

    /// <summary>
    /// 异步操作事件
    /// </summary>
    public delegate IAsyncOpa<T> AsyncOpaAction<T, T0, T1>(T0 arg0, T1 arg1);

}

