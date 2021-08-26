/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    ReadExcel
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-11 11:13:29
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;

namespace VSFramework
{
    /// <summary>
    /// 读取表格数据类
    /// </summary>
    public abstract class ReadExcel : IReadHandler
    {
        /// <summary>
        /// Excel路径
        /// </summary>
        public abstract string path { get; }

        public abstract void Read();

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="columnNum">列数</param>
        /// <param name="rowNum">行数</param>
        /// <returns></returns>
        protected virtual DataTableCollection ReadTables()
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError(GetType().Name + "路径为空");
                return null;
            }
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelReader.AsDataSet();
            return result.Tables;

        }

    }

}

