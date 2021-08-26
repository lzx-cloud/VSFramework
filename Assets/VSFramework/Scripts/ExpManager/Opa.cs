/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    Opa
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:24:39
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace VSFramework
{
    /// <summary>
    /// 实验操作
    /// </summary>
	public class Opa
    {
        #region 字段

        protected int _id;

        protected AsyncOpaAction<Opa> _beforeOpa;

        protected AsyncOpaAction<Opa> _inOpa;

        protected AsyncOpaAction<Opa> _afterOpa;

        protected AsyncOpaAction<Opa, UnityAction<bool>> _verifyOpa;

        /// <summary>
        /// 步骤ID
        /// </summary>
        public int id
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// 当前提示
        /// </summary>
        public string tipContent;

        /// <summary>
        /// 操作步骤的分数
        /// </summary>
        public float score = 0;

        /// <summary>
        /// 错误次数
        /// </summary>
        public int errNum = 0;

        /// <summary>
        /// 允许错误的最大次数
        /// </summary>
        public int allowErrNum = 3;

        /// <summary>
        /// 操作的ID
        /// </summary>
        public List<int> opaIDs = new List<int>();

        /// <summary>
        /// 高亮闪烁的ID
        /// </summary>
        public List<int> highlighterIDs = new List<int>();

        /// <summary>
        /// 操作之前触发事件
        /// </summary>
        public virtual AsyncOpaAction<Opa> BeforeOpa
        {
            set
            {
                _beforeOpa = value;
            }
        }

        /// <summary>
        /// 操作中触发事件
        /// </summary>
        public virtual AsyncOpaAction<Opa> InOpa
        {
            set
            {
                _inOpa = value;
            }
        }

        /// <summary>
        /// 操作之后触发事件
        /// </summary>
        public virtual AsyncOpaAction<Opa> AfterOpa
        {
            set
            {
                _afterOpa = value;
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        public virtual AsyncOpaAction<Opa, UnityAction<bool>> VerifyOpa
        {
            set
            {
                _verifyOpa = value;
            }
        }

        #endregion

        #region 方法

        public Opa() { }

        public Opa(int id)
        {
            this._id = id;
        }

        public Opa(string tipContent)
        {
            this.tipContent = tipContent;
        }

        public Opa(int id, string tipContent)
        {
            this._id = id;
            this.tipContent = tipContent;
        }

        /// <summary>
        /// 添加操作ID
        /// </summary>
        /// <param name="opaIDs">操作ID</param>
        public void AddOpaIDs(params int[] opaIDs)
        {
            for (int i = 0; i < opaIDs.Length; i++)
            {
                this.opaIDs.Add(opaIDs[i]);
            }
        }

        /// <summary>
        /// 添加高亮闪烁ID
        /// </summary>
        /// <param name="highlighterIDs"></param>
        public void AddHighlighterIDs(params int[] highlighterIDs)
        {
            for (int i = 0; i < highlighterIDs.Length; i++)
            {
                this.highlighterIDs.Add(highlighterIDs[i]);
            }
        }

        /// <summary>
        /// 执行操作之前的事件
        /// </summary>
        /// <returns>异步返回，也可以返回空</returns>
        public virtual IAsyncOpa<Opa> ExecuteBeforeOpa()
        {
            if (this._beforeOpa != null)
            {
                return this._beforeOpa(this);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 执行操作当中
        /// </summary>
        /// <returns>异步返回，也可以返回空</returns>
        public virtual IAsyncOpa<Opa> ExecuteInOpa()
        {
            if (this._inOpa != null)
            {
                return this._inOpa(this);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 执行操作之后的事件
        /// </summary>
        /// <returns>异步返回，也可以返回空</returns>
        public virtual IAsyncOpa<Opa> ExecuteAfterOpa()
        {
            if (this._afterOpa != null)
            {
                return this._afterOpa(this);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 执行验证
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="opaModel"></param>
        /// <returns></returns>
        public virtual IAsyncOpa<Opa> ExecuteVerifyOpa(UnityAction<bool> backcall, params OpaModel[] opaModels)
        {
            IAsyncOpa<Opa> result = null;
            if (opaModels.Length > 0)
            {
                bool isContain = true;
                for (int i = 0; i < opaModels.Length; i++)
                {
                    int opaID = opaModels[i].opaID;
                    if (!this.opaIDs.Contains(opaID))
                    {
                        isContain = false;
                        break;
                    }
                }

                if (isContain)
                {
                    if (this._verifyOpa != null)
                    {
                        result = this._verifyOpa(this, backcall);
                    }
                    else
                    {
                        backcall(true);     //不需要验证
                    }
                }
                else
                {
                    this.errNum++;
                    backcall(false);     //错误
                }
            }
            else
            {
                if (this._verifyOpa != null)
                {
                    result = this._verifyOpa(this, backcall);
                }
                else
                {
                    backcall(true);     //不需要验证
                }
            }

            return result;
        }

        #endregion

    }
}

