/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    InterceptableEnumerator
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:29:40
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
    /// 自定义线程
    /// </summary>
    public class InterceptableEnumerator : IEnumerator
    {
        private object current;
        public object Current
        {
            get
            {
                return this.current;
            }
        }

        private Stack<IEnumerator> stack = new Stack<IEnumerator>();

        private Action<Exception> onException;
        private Action onFinally;
        private Func<bool> hasNext;

        public InterceptableEnumerator(IEnumerator routine)
        {
            this.stack.Push(routine);
        }

        public bool MoveNext()
        {
            try
            {
                if (!this.HasNext())
                {
                    this.OnFinally();
                    return false;
                }

                if (stack.Count <= 0)
                {
                    this.OnFinally();
                    return false;
                }

                IEnumerator ie = stack.Peek();
                bool hasNext = ie.MoveNext();
                if (!hasNext)
                {
                    this.stack.Pop();
                    return MoveNext();
                }

                this.current = ie.Current;
                if (this.current is IEnumerator)
                {
                    stack.Push(this.current as IEnumerator);
                    return MoveNext();
                }

                return true;
            }
            catch (Exception e)
            {
                this.OnException(e);
                this.OnFinally();
                return false;
            }
        }

        /// <summary>
        /// 注册条件代码
        /// </summary>
        /// <param name="hasNext"></param>
        public virtual void RegisterConditionBlock(Func<bool> hasNext)
        {
            this.hasNext = hasNext;
        }

        /// <summary>
        ///注册代码，当异常时候执行
        /// </summary>
        /// <param name="onException"></param>
        public virtual void RegisterCatchBlock(Action<Exception> onException)
        {
            this.onException += onException;
        }

        /// <summary>
        /// 注册代码块，当完成执行完成后执行
        /// </summary>
        /// <param name="onFinally"></param>
        public virtual void RegisterFinallyBlock(Action onFinally)
        {
            this.onFinally += onFinally;
        }

        private bool HasNext()
        {
            if (hasNext == null)
                return true;
            return hasNext();
        }

        private void OnFinally()
        {
            try
            {
                if (this.onFinally == null)
                    return;

                foreach (Action action in this.onFinally.GetInvocationList())
                {
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning(ex);
                    }
                }
            }
            catch (Exception) { }
        }

        private void OnException(Exception e)
        {
            try
            {
                if (this.onException == null)
                    return;

                foreach (Action<Exception> action in this.onException.GetInvocationList())
                {
                    try
                    {
                        action(e);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning(ex);
                    }
                }
            }
            catch (Exception) { }
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}

