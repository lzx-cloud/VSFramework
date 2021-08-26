/*******************************************************************************
 *Copyright(C) 2017 by 八点
 *FileName:    Config
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-10 11:09:36
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
    /// 配置类基类
    /// </summary>
	public class Config : ScriptableObject
	{
        /// <summary>
        /// 创建Config
        /// </summary>
        public virtual void Create() 
        {
        }

        /// <summary>
        /// 获取该Config的Attribute属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetAttribute<T>() where T : Attribute
        {
            Type t = this.GetType();
            T attr = (T)Attribute.GetCustomAttribute(t, typeof(T));
            return attr;
        }

        /// <summary>
        /// 获取Config的Attribute属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <returns></returns>
        public static T GetAttribute<T, U>() where T : Attribute where U :  Config
        {
            Type t = typeof(U);
            T attr = (T)Attribute.GetCustomAttribute(t, typeof(T));
            return attr;
        }

        public new virtual void SetDirty()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

    }
}

