/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    Exp
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 16:18:23
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 实验模块管理
    /// </summary>
	public abstract class Exp
	{
        #region 字段和属性

        protected int _index = 0;

        /// <summary>
        /// 获取当前模块的分数
        /// </summary>
        public abstract float score { get; }

        /// <summary>
        ///当前步骤索引
        /// </summary>
        public int index
        {
            get
            {
                return _index;
            }
        }

        /// <summary>
        /// 当前实验是否完成
        /// </summary>
        public bool finish
        {
            get
            {
                return this._index >= this.opas.Count;
            }
            set
            {
                if (value)
                {
                    this._index = opas.Count;
                }
            }
        }

        /// <summary>
        /// 实验模块
        /// </summary>
        public abstract ExpModule expModule { get; }

        private List<Opa> opas = new List<Opa>();

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void VSFAwake();

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void VSFStart();

        /// <summary>
        /// 获取当前操作
        /// </summary>
        /// <returns></returns>
        public abstract Opa GetCurOpa();

        /// <summary>
        /// 移动到下一个操作
        /// </summary>
        public virtual void NextOpa()
        {
            if (index >= this.opas.Count)
            {
                return;
            }
            else
            {
                this._index++;
            }
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="Opa"></param>
        public abstract void AddOpa<T>(T opa) where T : Opa;

        /// <summary>
        /// 更新分数
        /// </summary>
        /// <param name="step"></param>
        public abstract void UpdateScore<T>(T opa) where T : Opa;

        /// <summary>
        /// 完成实验
        /// </summary>
        public abstract void Finish();

        /// <summary>
        /// 重新走流程
        /// </summary>
        public virtual void Reset()
        {
            this._index = 0;
        }

        #endregion

    }

}

