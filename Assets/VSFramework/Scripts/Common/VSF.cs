/*******************************************************************************
 *Copyright(C) 2017 by 八点 
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

        [SerializeField]
        private List<Manager> _managers = new List<Manager>();

        #endregion

        #region 方法

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public new static void VSFAwake()
        {
            string typeName = typeof(VSF).Name;
            GameObject go = new GameObject(typeName);
            Instance = go.AddComponent<VSF>();

            CreateInstance<AsyncManager>();
            CreateInstance<ToolManager>();
            CreateInstance<PoolManager>();
            CreateInstance<ExpManager>();

            Instance._managers.ForEach(manager => { manager.VSFStart(); });

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
            t.VSFAwake();
            t.transform.SetParent(Instance.transform);
            Instance._managers.Add(t);
            return t;
        }

        #endregion

    }
}

