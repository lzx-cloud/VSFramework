/*******************************************************************************
 *Copyright(C) 2017 by 8Point 
 *All rights reserved. 
 *FileName:    VSF
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-05 18:16:17
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 框架入口
    /// </summary>
	public class VSF : ManagerSingleton<VSF>
    {
        #region 字段和属性
        #endregion

        #region 方法
        #endregion

        [SerializeField]
        private List<Manager> _managers = new List<Manager>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public new static void MInit()
        {
            string typeName = typeof(VSF).Name;
            GameObject go = new GameObject(typeName);
            _instance = go.AddComponent<VSF>();

            CreateInstance<PoolManager>();
            CreateInstance<ExpManager>();

            Instance._managers.ForEach(manager => { manager.MStart(); });

            DontDestroyOnLoad(Instance.gameObject);
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T CreateInstance<T>() where T : Manager
        {
            string typeName = typeof(T).Name;
            GameObject go = new GameObject(typeName);
            T t = go.AddComponent<T>();
            t.MInit();
            t.transform.SetParent(Instance.transform);
            Instance._managers.Add(t);
            return t;
        }

    }
}

