/*******************************************************************************
 *Copyright(C) 2017 by 8Point 
 *All rights reserved. 
 *FileName:    ToolManager
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-06 11:20:14
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace VSFramework
{
    /// <summary>
    /// 
    /// </summary>
	public class ToolManager : ManagerSingleton<ToolManager>
	{
        /// <summary>
        /// 获取所有继承该类型的子类
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetAllTypes<T>() where T : class
        {

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
                        object obj = Activator.CreateInstance(type);
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
            Debug.Log(typeName);
            List<T> result = new List<T>();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                Type[] tfs = type.GetInterfaces();  //获取该类型的接口
                foreach (var tf in tfs)
                {
                    Debug.Log(tf.FullName + "  " + typeName);
                    if (tf.FullName == typeName)  //判断全名，是否在一个命名空间下面
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
        /// 固定长度添加换行符，自动换行
        /// </summary>
        /// <param name="num"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetNstring(int num, string str)
        {
            if (num <= 0)
            {
                return str;
            }
            string result = "";
            int point = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (point == num)
                {
                    result += '\n';
                    point = 0;
                }
                result += str[i];
                point++;
            }
            return result;
        }
    }
}

