/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ConfigHelperAttribute
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-10 16:21:34
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
    /// 配置文档帮助
    /// </summary>
	public class ConfigHelperAttribute : Attribute
	{
		/// <summary>
		/// 存放的Config资源路径，以Assets开头的路径
		/// </summary>
		public string path { get; set; }
	}
}

