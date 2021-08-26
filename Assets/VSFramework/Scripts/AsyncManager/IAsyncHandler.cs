/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    IAsyncHandler
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:28:48
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
    /// 异步等待
    /// </summary>
    public interface IAsyncHandler
    {
        /// <summary>
        /// 是否加载完成
        /// </summary>
        bool IsDone { get; }

        /// <summary>
        /// 抛出异常
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        ///设置异常
        /// </summary>
        /// <param name="ex"></param>
        void SetException(Exception ex);

        /// <summary>
        /// 设置异常
        /// </summary>
        /// <param name="error"></param>
        void SetException(string error);

        /// <summary>
        /// 回调
        /// </summary>
        /// <returns></returns>
        ICallBackHandler Callback();

        /// <summary>
        /// 等待完成
        /// </summary>
        /// <returns></returns>
        object WaitForDone();

        /// <summary>
        /// 设置结果
        /// </summary>
        void SetResult();

    }

    /// <summary>
    /// 携带结果的异步等待
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public interface IAsyncHandler<T> : IAsyncHandler
    {
        /// <summary>
        /// 返回的结果
        /// </summary>
        T Result { get; }

        /// <summary>
        /// 设置返回结果
        /// </summary>
        /// <param name="result"></param>
        void SetResult(T result = default);

        new ICallBackHandler<T> Callback();

    }
}

