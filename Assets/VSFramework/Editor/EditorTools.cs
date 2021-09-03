/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    EditorTools
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-10 11:35:19
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.Reflection;

namespace VSFramework
{
    /// <summary>
    /// 编辑器工具类
    /// </summary>
    public class EditorTools
    {
        #region Test

        [MenuItem("Test/TestPath")]
        static void TestPath()
        {
            List<Config> configs = EditorUntil.GetAllTypes<Config>();
            configs.AddRange(ToolManager.GetAllTypes<Config>());
            for (int i = 0; i < configs.Count; i++)
            {
                configs[i].Create();
                Type t = configs[i].GetType();
                ConfigHelperAttribute attr = configs[i].GetAttribute<ConfigHelperAttribute>();
                Debug.Log(attr.path);
            }
        }

        #endregion

        #region 创建脚本

        [MenuItem("Assets/VSF/Create/CreateScript", false, 0)]
        static void CreateScript() 
        {
            CreateScriptTemplate.Create();
        }

        #endregion

        #region 读取File

        [MenuItem("Assets/VSF/Read/ReadAllExcels")]
        static void ReadAllExcels()
        {
            List<ReadExcel> handles = EditorUntil.GetAllTypes<ReadExcel>();
            for (int i = 0; i < handles.Count; i++)
            {
                handles[i].Read();
            }
        }

        [MenuItem("Assets/VSF/Read/ReadQuesExcel")]
        static void ReadQuesExcel()
        {
            //ReadQuesExcel handle = new ReadQuesExcel();
            //handle.ReadExcel();
        }

        #region
        [MenuItem("Assets/GameObjes/ddd")]
        #endregion

        #endregion

        #region 上下文

        [MenuItem("CONTEXT/OpaModel/ResetOpaID")]
        public static void ResetOpaID(MenuCommand command)
        {
            OpaModel opaModel = command.context as OpaModel;
            BindingFlags bf = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            Type baseType = opaModel.GetType();
            while (baseType != null)
            {
                string tt = baseType.Name;
                if (tt == typeof(OpaModel).Name)
                {
                    break;
                }

                baseType = baseType.BaseType;
            }
            MethodInfo minfo = baseType.GetMethod("ResetOpaID", bf);
            int opaID = EditorUntil.GetUniqueID();
            minfo.Invoke(opaModel, new object[] { opaID });
        }

        #endregion

    }
}

