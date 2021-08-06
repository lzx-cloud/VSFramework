/*******************************************************************************
 *Copyright(C) 2017 by LGui 
 *All rights reserved. 
 *FileName:    ScriptTemplateConfig
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2018.3.14f1 
 *Date:         2020-06-15 14:41:44
 *Description:    
 *History: 
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace LGui.LGame
{
    public class ScriptTemplateConfig
    {
        /// <summary>
        /// 作者
        /// </summary>
        [Header("作者")]
        public string Author = "李志兴";

        /// <summary>
        /// 公司姓名
        /// </summary>
        [Header("公司")]
        public string CompanyName = "8Point";

        /// <summary>
        /// 命名空间
        /// </summary>
        [Header("名称空间")]
        public string NameSpace = "VSFramework";

        /// <summary>
        /// 版本
        /// </summary>
        [Header("版本")]
        public string Version = "V1.0";


        /// <summary>
        /// 代码模板路径
        /// </summary>
        private static string _getScriptTemplatePath = "Assets/VSFramework/Editor/AppData";
        public static string getScriptTemplateCsPath
        {
            get
            {
                string path = Path.Combine(_getScriptTemplatePath,"ScriptTemplate.txt");
                if (!File.Exists(path))
                {
                    Debug.LogError("不存在路径：" + path);
                }
                return path;
            }
        }

    }

}