
/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    OpaModelHelper
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-09-03 16:16:11
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace VSFramework
{
	/// <summary>
	/// 
	/// </summary>
	[CustomEditor(typeof(OpaModel), true)]
	[CanEditMultipleObjects]
	public class OpaModelHelper : Editor
    {
        private void OnEnable()
        {
            OpaModel model = target as OpaModel;
            if (model.opaID == -1)
            {
                MenuCommand command = new MenuCommand(target);
                EditorTools.ResetOpaID(command);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(5);
            GUI.color = Color.green;
            if (GUILayout.Button("复制OpaID", GUILayout.Height(25)))
            {
                OpaModel model = target as OpaModel;
                GUIUtility.systemCopyBuffer = model.opaID.ToString();
            }
            GUI.color = Color.white;
        }

    }
}

