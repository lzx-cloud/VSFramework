/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ICallBackHandler
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:30:20
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace VSFramework
{
    /// <summary>
    /// 异步返回
    /// </summary>
    public interface ICallBackHandler
    {
        void OnCallback(Action<IAsyncHandler> callback);

    }

    /// <summary>
    /// 携带结果的异步返回
    /// </summary>
    public interface ICallBackHandler<T>
    {
        void OnCallback(Action<IAsyncHandler<T>> callback);

    }

    /// <summary>
    /// 携带进度的异步回调结果
    /// </summary>
    public interface IPorgressCallBackHandler
    {
        void OnCallback(Action<float> callback);

    }
}

