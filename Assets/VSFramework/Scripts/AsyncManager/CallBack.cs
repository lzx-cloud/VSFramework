/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    CallBack
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:30:29
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
    /// 回调结果
    /// </summary>
    public class Callback : ICallBackHandler
    {
        private IAsyncHandler result;

        private Action<IAsyncHandler> callback;

        public Callback(IAsyncHandler result)
        {
            this.result = result;
        }

        public void RaiseOnCallback()
        {
            try
            {
                if (this.callback == null)
                    return;

                var list = this.callback.GetInvocationList();
                this.callback = null;

                foreach (Action<IAsyncHandler> action in list)
                {
                    try
                    {
                        action(this.result);
                    }
                    catch (Exception e)
                    {
                        string log = string.Format("Class[{0}] callback exception.Error:{1}", this.GetType(), e);
                        Debug.LogWarning(log);
                    }
                }
            }
            catch (Exception e)
            {

                string log = string.Format("Class[{0}] callback exception.Error:{1}", this.GetType(), e);
                Debug.LogWarning(log);
            }
        }

        public void OnCallback(Action<IAsyncHandler> callback)
        {
            if (callback == null)
                return;

            if (this.result.IsDone)
            {
                try
                {
                    callback(this.result);
                }
                catch (Exception e)
                {
                    string log = string.Format("Class[{0}] callback exception.Error:{1}", this.GetType(), e);
                    Debug.LogWarning(log);
                }
                return;
            }

            this.callback += callback;
        }

    }

    /// <summary>
    /// 携带结果回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Callback<T> : ICallBackHandler<T>
    {
        private IAsyncHandler<T> result;

        private Action<IAsyncHandler<T>> callback;

        public Callback(IAsyncHandler<T> result)
        {
            this.result = result;
        }

        public void RaiseOnCallback()
        {
            try
            {
                if (this.callback == null)
                    return;

                var list = this.callback.GetInvocationList();
                this.callback = null;

                foreach (Action<IAsyncHandler<T>> action in list)
                {
                    try
                    {
                        action(this.result);
                    }
                    catch (Exception e)
                    {
                        string log = string.Format("Class[{0}] callback exception.Error:{1}", this.GetType(), e);
                        Debug.LogWarning(log);
                    }
                }
            }
            catch (Exception e)
            {

                string log = string.Format("Class[{0}] callback exception.Error:{1}", this.GetType(), e);
                Debug.LogWarning(log);
            }
        }


        public void OnCallback(Action<IAsyncHandler<T>> callback)
        {
            if (callback == null)
                return;

            if (this.result.IsDone)
            {
                try
                {
                    callback(this.result);
                }
                catch (Exception e)
                {
                    string log = string.Format("Class[{0}] callback exception.Error:{1}", this.GetType(), e);
                    Debug.LogWarning(log);
                }
                return;
            }

            this.callback += callback;
        }

    }

    /// <summary>
    /// 携带进度的回调
    /// </summary>
    public class ProgressCallback : IPorgressCallBackHandler
    {
        protected Action<float> callback;

        public void RaiseOnCallback(float progress)
        {
            try
            {
                if (this.callback == null)
                    return;

                var list = this.callback.GetInvocationList();

                foreach (Action<float> action in list)
                {
                    try
                    {
                        action(progress);
                    }
                    catch (Exception e)
                    {
                        string log = string.Format("Class[{0}] callback exception.Error:{1}", this.GetType(), e);
                        Debug.LogWarning(log);
                    }
                }
            }
            catch (Exception e)
            {

                string log = string.Format("Class[{0}] callback exception.Error:{1}", this.GetType(), e);
                Debug.LogWarning(log);
            }
        }

        public void OnCallback(Action<float> callback)
        {
            if (callback == null)
                return;

            this.callback += callback;
        }

    }

}

