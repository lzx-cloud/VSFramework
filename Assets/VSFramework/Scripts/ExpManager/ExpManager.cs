/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ExpManager
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-05 18:19:41
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 步骤管理
    /// </summary>
	public sealed class ExpManager : ManagerSingleton<ExpManager>
    {
        #region 字段和属性

        /// <summary>
        /// 实验模式
        /// </summary>
        public ExpMode expMode
        {
            get;
            private set;
        }

        /// <summary>
        /// 所有实验模块数据
        /// </summary>
        public List<Exp> exps
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前实验模块
        /// </summary>
        private Exp exp;

        #endregion

        #region VSF方法

        public override void VSFAwake()
        {
            base.VSFAwake();

            this.exps = ToolManager.GetAllTypes<Exp>();  //获取所有的Exp

            this.exps.ForEach(exp =>
            {
                exp.VSFAwake();  //初始化
            });

        }

        public override void VSFStart()
        {
            base.VSFStart();

            this.exps.ForEach(exp =>
            {
                exp.VSFAwake();  //初始化
            });
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取当前步骤
        /// </summary>
        /// <returns></returns>
        public Opa GetCurOpa()
        {
            if (this.exp == null)
            {
#if UNITY_EDITOR
                string log = string.Format("不存在{0}该类型的操作步骤数据", expMode);
                Debug.LogWarning(log);
#endif
                return null;
            }
            else
            {
                Opa opa = this.exp.GetCurOpa();
                if (opa == null)
                {
                    this.exp.Finish();
                }
                return opa;
            }
        }

        /// <summary>
        /// 移动到下一步骤
        /// </summary>
        public void NextOpa()
        {
            #region 获取变量
            if (this.exp == null)
            {
#if UNITY_EDITOR
                string log = string.Format("当前的操作模块不存在");
                Debug.LogWarning(log);
#endif
                return;
            }

            Opa opa = exp.GetCurOpa();
            if (opa == null)
            {
#if UNITY_EDITOR
                string log = string.Format("当前操作步骤不存在");
                Debug.LogWarning(log);
#endif
                return;
            }


            #endregion

            #region 验证

            IAsyncOpa<Opa> verify = opa.ExecuteVerifyOpa(isPass =>
            {
            });

            #endregion

            #region 执行操作之后事件
            #endregion

            #region
            #endregion

            IAsyncOpa<Opa> async = opa.ExecuteAfterOpa();
            if (async == null)
            {
                async = new IAsyncOpa<Opa>(opa);
                async.SetResult();
            }

            async.Callback().OnCallback(result =>
            {
                exp.NextOpa();
                opa = exp.GetCurOpa();  //提示
                if (opa != null)
                {
                    opa.ExecuteBeforeOpa();
                }
                else
                {
                    exp.Finish();
                }
            });

        }

        #endregion

        //考核、分数
        //关闭之前的提示
        //异步移动视野
        //提示 闪烁 文字提示
        //动画

        #region 实验数据模块

        /// <summary>
        /// 根据类型获取实验模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetExp<T>() where T : Exp
        {
            string typeName = typeof(T).FullName;
            for (int i = 0; i < this.exps.Count; i++)
            {
                Exp exp = this.exps[i];
                if (exp.GetType().FullName.Equals(typeName))
                {
                    T result = exp as T;
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 更新当前实验数据模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void UpdateCurExp<T>() where T : Exp
        {
            T exp = GetExp<T>();
            if (exp == null)
            {
#if UNITY_EDITOR
                string log = string.Format("不存在{0}模块数据,无法更新", typeof(T).FullName);
                Debug.LogError(log);
#endif
            }
            else
            {
                this.exp = exp;
            }
        }

        /// <summary>
        /// 获取当前实验模块数据
        /// 首先需要先设置，否则为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetCurExp<T>() where T : Exp
        {
            if (this.exp == null)
            {
#if UNITY_EDITOR
                string log = string.Format("请先调用UpdateCurExp<T>方法，设置当前Exp");
                Debug.LogError(log);
#endif
                return null;
            }
            else
            {
                return this.exp as T;
            }
        }

        /// <summary>
        /// 重新设置Exp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ResetExp<T>() where T : Exp
        {
            //T exp = GetExpModule<T>();
            //if (exp != null)
            //{
            //    exp.Reset();
            //}
        }

        /// <summary>
        /// 重新设置所有的Exp
        /// </summary>
        public void ResetExps()
        {
            exps.ForEach(exp =>
            {
                exp.Reset();
            });
        }

        #endregion

    }
}

