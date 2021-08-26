/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ObjectPool
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-12 14:35:07
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    ///  对象池对应的
    /// </summary>
	public class ObjectPool
	{
        protected int _id;

        protected object obj;

        /// <summary>
        /// 该对象Id
        /// </summary>
        public int id
        {
            get
            {
                return _id;
            }
        }

        public ObjectPool(int id) 
        {
            this._id = id;
        }

        /// <summary>
        /// 设置保存的对象
        /// </summary>
        /// <param name="obj"></param>
        public void Set(object obj) 
        {
            this.obj = obj;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() 
        {
            return (T)this.obj;
        }
        
	}

}

