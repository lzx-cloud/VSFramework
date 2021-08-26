/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    IReadExcelHandler
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 10:52:11
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 读取Excel接口
    /// </summary>
	public interface IReadExcelHandler 
	{
        /// <summary>
        /// Excel路径
        /// </summary>
        string path { get; }

        /// <summary>
        /// 读取Excel表格数据
        /// </summary>
        void Read();

	}
}

