/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    PropertyDisplayOnly
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-12 15:02:11
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 属性在面板上只显示但不可修改
    /// </summary>
    public class PropertyDisplayOnly : PropertyAttribute
    {
    }

#if UNITY_EDITOR

    [UnityEditor.CustomPropertyDrawer(typeof(PropertyDisplayOnly))]
    public class ReadOnlyDrawer : UnityEditor.PropertyDrawer
    {
        public override float GetPropertyHeight(UnityEditor.SerializedProperty property, GUIContent label)
        {
            return UnityEditor.EditorGUI.GetPropertyHeight(property, label, true);
        }


        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            UnityEditor.EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;

        }
    }

#endif
}

