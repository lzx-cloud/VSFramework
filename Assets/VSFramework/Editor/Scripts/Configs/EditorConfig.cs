/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    EditorConfig
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-10 11:12:48
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 编辑器Config配置类
    /// </summary>
    [ConfigHelper(path = "Assets/Editor/AppData/Configs")]
    public class EditorConfig : Config
	{
        #region 字段和属性

        [SerializeField]
        [PropertyDisplayOnly]
        private int _uniqueID;

        /// <summary>
        /// 唯一ID，不断递增
        /// </summary>
        public int uniqueID
        {
            get
            {
                return _uniqueID;
            }
        }

        #endregion

        #region 方法

        public int GetUniqueID()
        {
            this._uniqueID++;
            this.SetDirty();
            return this._uniqueID;
        }

        #endregion

    }
}

