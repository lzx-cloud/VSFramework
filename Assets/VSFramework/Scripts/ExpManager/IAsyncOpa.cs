/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    IAsyncOpa
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:40:18
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
    /// 异步操作
    /// </summary>
	public class IAsyncOpa : IAsyncHandler
	{
        #region 字段

        protected bool done;

        protected Exception exception;

        protected Callback callback;

        /// <summary>
        /// 是否完成
        /// </summary>
        public virtual bool IsDone
        {
            get
            {
                return done;
            }
        }

        /// <summary>
        /// 异常
        /// </summary>
        public virtual Exception Exception
        {
            get
            {
                return exception;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <returns></returns>
        public virtual ICallBackHandler Callback()
        {
            return this.callback ?? (this.callback = new Callback(this));
        }

        /// <summary>
        /// 设置异常
        /// </summary>
        /// <param name="ex"></param>
        public virtual void SetException(Exception ex)
        {
            if (this.done)
                return;

            this.exception = ex;
            this.done = true;
            this.RaiseOnCallback();
        }

        /// <summary>
        /// 设置异常
        /// </summary>
        /// <param name="error"></param>
        public virtual void SetException(string error)
        {
            if (this.done)
                return;

            var exception = new Exception(string.IsNullOrEmpty(error) ? "unknown error!" : error);
            SetException(exception);
        }

        public virtual void SetResult()
        {
            if (this.done)
                return;
            this.done = true;
            this.RaiseOnCallback();
        }

        /// <summary>
        /// 等待执行
        /// </summary>
        /// <returns></returns>
        public object WaitForDone()
        {
            WaitWhile result = new WaitWhile(() => !IsDone);
            return result;
        }

        protected virtual void RaiseOnCallback()
        {
            if (this.callback != null)
                this.callback.RaiseOnCallback();
        }

        #endregion

    }

    /// <summary>
    /// 泛型异步操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IAsyncOpa<T> : IAsyncHandler<T>
    {
        #region 字段

        protected T result;

        protected bool done;

        protected Exception exception;

        protected Callback<T> callback;

        /// <summary>
        /// 结果
        /// </summary>
        public virtual T Result
        {
            get
            {
                return result;
            }
        }

        /// <summary>
        /// 完成
        /// </summary>
        public virtual bool IsDone
        {
            get
            {
                return done;
            }
        }

        /// <summary>
        /// 异常
        /// </summary>
        public virtual Exception Exception
        {
            get
            {
                return exception;
            }
        }

        #endregion

        #region 方法

        public IAsyncOpa(T result)
        {
            this.result = result;
        }

        /// <summary>
        /// 回调
        /// </summary>
        /// <returns></returns>
        public virtual ICallBackHandler<T> Callback()
        {
            return this.callback ?? (this.callback = new Callback<T>(this));
        }

        /// <summary>
        /// 设置异常信息
        /// </summary>
        /// <param name="ex"></param>
        public virtual void SetException(Exception ex)
        {
            if (this.done)
                return;

            this.exception = ex;
            this.done = true;
            this.RaiseOnCallback();
        }

        /// <summary>
        /// 设置异常
        /// </summary>
        /// <param name="error"></param>
        public virtual void SetException(string error)
        {
            if (this.done)
                return;

            var exception = new Exception(string.IsNullOrEmpty(error) ? "unknown error!" : error);
            SetException(exception);
        }

        /// <summary>
        /// 设置结果
        /// </summary>
        /// <param name="result"></param>
        public void SetResult(T result = default)
        {
            if (this.done)
                return;
            this.done = true;
            this.result = result;
            this.RaiseOnCallback();
        }

        /// <summary>
        /// 设置结果
        /// </summary>
        public virtual void SetResult()
        {
            if (this.done)
                return;
            this.done = true;
            this.RaiseOnCallback();
        }

        /// <summary>
        /// 等待执行完毕
        /// </summary>
        /// <returns></returns>
        public object WaitForDone()
        {
            WaitWhile result = new WaitWhile(() => !IsDone);
            return result;
        }

        protected virtual void RaiseOnCallback()
        {
            if (this.callback != null)
                this.callback.RaiseOnCallback();
        }

        ICallBackHandler IAsyncHandler.Callback()
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}

