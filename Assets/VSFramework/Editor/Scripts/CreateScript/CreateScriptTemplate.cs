/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    CreateScriptTemplate
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 11:17:59
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Text;

namespace VSFramework
{
    /// <summary>
    /// 
    /// </summary>
    /// <summary>
    /// 创建代码模板
    /// </summary>
    public class CreateScriptTemplate
    {
       
        public static void Create()
        {
            CreateScriptAsset scriptAsset = ScriptableObject.CreateInstance<CreateScriptAsset>();

            //参数为传递给CreateEventCSScriptAsset类action方法的参数  
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, scriptAsset,
               GetSelectPathOrFallback() + "/NewScript.cs", null,
              ScriptTemplateConfig.getScriptTemplateCsPath);
        }

        public static string GetSelectPathOrFallback()
        {
            string path = "Assets";
            //遍历选中的资源以获得路径  
            //Selection.GetFiltered是过滤选择文件或文件夹下的物体，assets表示只返回选择对象本身  
            foreach (
                UnityEngine.Object obj in
                Selection.GetFiltered(typeof(UnityEngine.Object),
                SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }
    }

    class CreateScriptAsset : EndNameEditAction
    {
        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            //创建资源  
            UnityEngine.Object obj = CreateScriptAssetTemplate(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(obj);//高亮显示资源  
        }

        UnityEngine.Object CreateScriptAssetTemplate(string pathName, string resourceFile)
        {
            //获取要创建资源的绝对路径  
            string fullPath = Path.GetFullPath(pathName);

            //读取本地的模板文件  
            StreamReader streamReader = new StreamReader(resourceFile);
            string text = streamReader.ReadToEnd();
            streamReader.Close();

            //获取文件名，不含扩展名  
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);

            ScriptTemplateConfig scriptTemplate = EditorUntil.GetConfig<ScriptTemplateConfig>();

            //将模板类中的类名替换成你创建的文件名  
            text = Regex.Replace(text, "#CompanyName#", scriptTemplate.CompanyName);
            text = Regex.Replace(text, "#ScriptName#", fileNameWithoutExtension);
            text = Regex.Replace(text, "#Author#", scriptTemplate.Author);
            text = Regex.Replace(text, "#Version#", scriptTemplate.Version);
            text = Regex.Replace(text, "#UnityVersion#", Application.unityVersion);
            text = Regex.Replace(text, "#NowTime#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            text = Regex.Replace(text, "#NameSpace#", scriptTemplate.NameSpace);

            //写入配置文件  
            bool encoderShouldEmitUTF8Identifier = true; //参数指定是否提供 Unicode 字节顺序标记  
            bool throwOnInvalidBytes = false;//是否在检测到无效的编码时引发异常  
            bool append = false;
            UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);
            StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
            streamWriter.Write(text);
            streamWriter.Close();

            //刷新资源管理器  
            AssetDatabase.ImportAsset(pathName);
            AssetDatabase.Refresh();
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }
    }
}

