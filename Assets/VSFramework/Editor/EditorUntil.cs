/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    EditorUntil
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-10 11:35:02
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using System;
using System.IO;

namespace VSFramework
{
    /// <summary>
    /// 编辑器全局
    /// </summary>
	public class EditorUntil
    {
        #region 公共静态方法

        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T GetConfig<T>() where T : Config
        {
           ConfigHelperAttribute attr =  Config.GetAttribute<ConfigHelperAttribute, T>();
            string path = Path.Combine(attr.path, typeof(T).Name + ".asset");
            if (!Directory.Exists(attr.path))
            {
                Directory.CreateDirectory(attr.path);
            }
            T instance = AssetDatabase.LoadAssetAtPath<T>(path);
            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(instance, path);
            }
            return instance;
        }

        /// <summary>
        /// 获取所有继承该类型的子类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetAllTypes<T>() where T : class
        {
            bool isAssetScript = typeof(T).IsSubclassOf(typeof(ScriptableObject));
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var typeName = typeof(T).FullName;
            List<T> result = new List<T>();
            foreach (var type in types)
            {
                var baseType = type.BaseType;  //获取基类
                while (baseType != null)  //获取所有基类
                {
                    if (baseType.FullName == typeName)
                    {
                        object obj = null;
                        if (isAssetScript)
                        {
                            obj = ScriptableObject.CreateInstance(type);
                        }
                        else
                        {
                            obj = Activator.CreateInstance(type);
                        }
                        if (obj != null)
                        {
                            T info = obj as T;
                            result.Add(info);
                        }
                        break;
                    }
                    else
                    {
                        baseType = baseType.BaseType;
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// 获取所有继承该接口的子类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetAllInfs<T>() where T : class
        {
            var typeName = typeof(T).FullName;
            List<T> result = new List<T>();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                Type[] tfs = type.GetInterfaces(); //获取该类型的接口
                foreach (var tf in tfs)
                {
                    //判断全名，是否在一个命名空间下面
                    if (tf.FullName == typeName)
                    {
                        object obj = Activator.CreateInstance(type);
                        if (obj != null)
                        {
                            T info = obj as T;
                            result.Add(info);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取唯一ID
        /// </summary>
        /// <returns></returns>
        public static int GetUniqueID()
        {
            EditorConfig config = GetConfig<EditorConfig>();
            return config.GetUniqueID();
        }

        #endregion

    }

}

