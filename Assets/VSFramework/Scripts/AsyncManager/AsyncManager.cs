/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    AsyncManager
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:30:01
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
    /// 异步执行管理
    /// </summary>
    public sealed class AsyncManager : ManagerSingleton<AsyncManager>
    {
        #region 字段

        public bool useFixedUpdate = false;

        private List<object> pendingQueue = new List<object>();

        private List<object> stopingQueue = new List<object>();

        private List<object> runningQueue = new List<object>();

        private List<object> stopingTempQueue = new List<object>();

        #endregion

        #region 私有函数

        private void Update()
        {
            if (useFixedUpdate)
                return;

            if (pendingQueue.Count <= 0 && stopingQueue.Count <= 0)
                return;

            this.DoStopingQueue();

            this.DoPendingQueue();

        }

        private void FixedUpdate()
        {
            if (!useFixedUpdate)
                return;

            if (pendingQueue.Count <= 0 && stopingQueue.Count <= 0)
                return;

            this.DoStopingQueue();

            this.DoPendingQueue();
        }

        private void DoStopingQueue()
        {
            lock (stopingQueue)
            {
                if (stopingQueue.Count <= 0)
                    return;

                stopingTempQueue.Clear();
                stopingTempQueue.AddRange(stopingQueue);
                stopingQueue.Clear();
            }

            for (int i = 0; i < stopingTempQueue.Count; i++)
            {
                try
                {
                    object task = stopingTempQueue[i];
                    if (task is IEnumerator)
                    {
                        this.StopCoroutine((IEnumerator)task);
                        continue;
                    }

                    if (task is Coroutine)
                    {
                        this.StopCoroutine((Coroutine)task);
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
            }
            stopingTempQueue.Clear();
        }
        private void DoPendingQueue()
        {
            if (pendingQueue.Count <= 0)
                return;

            runningQueue.Clear();
            runningQueue.AddRange(pendingQueue);
            pendingQueue.Clear();

            for (int i = 0; i < runningQueue.Count; i++)
            {
                try
                {
                    object task = runningQueue[i];
                    if (task is Action)
                    {
                        ((Action)task)();
                        continue;
                    }

                    if (task is IEnumerator)
                    {
                        this.StartCoroutine((IEnumerator)task);
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
            }
            runningQueue.Clear();
        }

        private InterceptableEnumerator WrapEnumerator(IEnumerator routine, IAsyncHandler result)
        {
            InterceptableEnumerator enumerator = routine is InterceptableEnumerator ? (InterceptableEnumerator)routine : new InterceptableEnumerator(routine);
            if (result != null)
            {
                enumerator.RegisterConditionBlock(() => !(result.IsDone));
                enumerator.RegisterCatchBlock(e =>
                {
                    if (result != null)
                        result.SetException(e);
                });
                enumerator.RegisterFinallyBlock(() =>
                {
                    if (result != null && !result.IsDone)
                    {
                        result.SetResult();
                    }
                });
            }
            return enumerator;
        }

        void OnApplicationQuit()
        {
            this.StopAllCoroutines();
        }

        #endregion

        #region 公共函数

        /// <summary>
        /// 开启携程
        /// </summary>
        /// <param name="routine"></param>
        /// <param name="result"></param>
        public static void StartCoroutine(IEnumerator routine, IAsyncHandler result)
        {
            Execute(Instance.WrapEnumerator(routine, result));
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="action"></param>
        public static void Execute(Action action)
        {
            if (action == null)
                return;
            Instance.pendingQueue.Add(action);
        }

        /// <summary>
        /// 执行携程
        /// </summary>
        /// <param name="routine"></param>
        public static void Execute(IEnumerator routine)
        {
            if (routine == null)
                return;
            Instance.pendingQueue.Add(routine);
        }

        /// <summary>
        /// 停止 Coroutine
        /// </summary>
        /// <param name="routine"></param>
        public static void Stop(IEnumerator routine)
        {
            if (routine == null)
                return;

            if (Instance.pendingQueue.Contains(routine))
            {
                Instance.pendingQueue.Remove(routine);
                return;
            }

            Instance.stopingQueue.Add(routine);

        }

        /// <summary>
        /// 停止 Coroutine
        /// </summary>
        /// <param name="routine"></param>
        public static void Stop(Coroutine routine)
        {
            if (routine == null)
                return;

            Instance.stopingQueue.Add(routine);
        }

        #endregion

    }

}

