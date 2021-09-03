/*******************************************************************************
 *Copyright(C) 2017 by 八点 
 *FileName:    OpaModel
 *Author:       李志兴
 *Version:      V1.0
 *UnityVersion：2019.3.8f1 
 *Date:         2021-08-12 10:58:07
 *Description:    
 *History: 
******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VSFramework
{
    /// <summary>
    /// 操作基类
    /// </summary>
	public abstract class OpaModel : MonoBehaviour
	{
		#region 字段和属性

		[SerializeField]
		[PropertyDisplayOnly]
		[Header("操作ID(唯一)")]
		protected int _opaID = -1;

		[SerializeField]
		[Header("初始化是否打开")]
		protected bool _initOpen = true;

		[SerializeField]
		[PropertyDisplayOnly]
		[Header("存入对象池之后，动态生成的唯一Id")]
		protected int _uniqueId;

		/// <summary>
		/// 操作ID，需要保持唯一
		/// </summary>
		public int opaID
		{
			get
			{
				return _opaID;
			}
		}

		/// <summary>
		/// 初始化是否打开
		/// </summary>
		public bool initOpen
		{
			get
			{
				return _initOpen;
			}
		}

		/// <summary>
		/// 存入对象池之后，动态生成的唯一Id
		/// </summary>
		public int uniqueId
		{
			get
			{
				return _uniqueId;
			}
		}

        #endregion

        #region 内置方法

        private void Awake()
        {
			this.VSFAwake();
        }

        private void Start()
        {
			this.VSFStart();
        }

        protected virtual void OnDestroy()
		{
			this.VSFDestroy();
		}

		protected virtual void Reset()
		{
			this.VSFReset();
		}

		#endregion

		#region VSF方法

		public virtual void VSFAwake()
		{
			this._uniqueId = PoolManager.Instance.VSFAdd(this);
		}

		public virtual void VSFStart() { }

		public virtual void VSFOpen() { }

		public virtual void VSFHidden() { }

		public virtual void VSFTip(bool tip) { }

		public virtual void VSFTip() { }

		protected virtual void VSFDestroy()
		{
			PoolManager.Instance.VSFDel(this.uniqueId);
		}

		protected virtual void VSFReset()
		{
			
		}

		protected virtual void ResetOpaID(int opaID)
		{
			this._opaID = opaID;
		}

		#endregion


	}
}

