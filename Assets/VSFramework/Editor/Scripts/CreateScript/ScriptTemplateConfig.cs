/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ScriptTemplateConfig
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 11:17:27
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace VSFramework
{
    /// <summary>
    /// 代码模板配置
    /// </summary>
    [ConfigHelper(path = "Assets/VSFramework/Editor/AppData/Configs")]
    public class ScriptTemplateConfig : Config
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
        public string CompanyName = "八点";

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
        public static string getScriptTemplateCsPath
        {
            get
            {
                string path = "Assets/VSFramework/Editor/AppData/ScriptTemplate.txt";
                if (!File.Exists(path))
                {
                    Debug.LogError("不存在路径：" + path);
                }
                return path;
            }
        }

    }

}

