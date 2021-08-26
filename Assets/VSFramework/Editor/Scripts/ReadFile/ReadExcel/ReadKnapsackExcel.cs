/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ReadKnapsackExcel
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 10:53:07
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 读取背包数据
    /// </summary>
	public class ReadKnapsackExcel : IReadExcelHandler
    {
        public string path
        {
            get
            {
                return "ReadKnapsackExcel.xlxs";
            }
        }

        public void Read()
        {
            Debug.Log(path);
        }

    }

}

