/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ReadQuesExcel
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 10:52:30
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 读取QuesExcel
    /// </summary>
	public class ReadQuesExcel : IReadExcelHandler
    {
        public string path
        {
            get
            {
                return "ReadQuesExcel.xlxs";
            }
        }

        public void Read()
        {
            Debug.Log(path);
        }

    }
}

