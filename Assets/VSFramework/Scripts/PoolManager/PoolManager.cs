/*******************************************************************************
 *Copyright(C) 2017 by 八点
 *FileName:    PoolManager
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-05 18:19:23
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 资源池
    /// </summary>
	public class PoolManager : ManagerSingleton<PoolManager>
	{
        #region 字段和属性

        private int _uniqueNum = 0;

        private List<ObjectPool> pools = new List<ObjectPool>();

        /// <summary>
        /// 唯一Num
        /// </summary>
        public int uniqueNum
        {
            get
            {
                this._uniqueNum++;
                return this._uniqueNum;
            }
        }

        #endregion

        #region VSF方法

        public int VSFAdd<T>(T obj)
        {
            if (obj == null)
            {
                Debug.LogError("添加到对象池的对象不能为空");
            }
            int uniqueNum = this.uniqueNum;
            ObjectPool pool = new ObjectPool(uniqueNum);
            pool.Set(obj) ;
            this.pools.Add(pool);
            return pool.id;
        }

        public T VSFGet<T>(int uniqueNum) where T : class
        {
            ObjectPool pool = this.pools.Find(item=> 
            {
                return item.id == uniqueNum;
            });
            if (pool == null)
            {
                return null;
            }
            else
            {
                return pool.Get<T>();
            }
        }

        public void VSFDel(int uniqueNum)
        {
            this.pools.RemoveAll(item=> 
            {
                return item.id == uniqueNum;
            });
        }

#endregion

    }
}

