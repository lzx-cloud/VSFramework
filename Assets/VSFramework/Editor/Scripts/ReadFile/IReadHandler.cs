/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    IReadHandler
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 11:13:15
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 读取文件接口
    /// </summary>
    public interface IReadHandler
    {
        string path { get; }

        void Read();
    }

}

