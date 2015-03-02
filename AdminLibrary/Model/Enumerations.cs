using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AdminLibrary.Model
{
    /// <summary>
    /// 操作状态
    /// </summary>
    public enum OptStatus : byte
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 1,
        /// <summary>
        /// 重复
        /// </summary>
        Repeat = 2,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 3,
        /// <summary>
        /// 不运行添加
        /// </summary>
        NotAllow = 4,

    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 新增
        /// </summary>
        Insert = 1,
        /// <summary>
        /// 更新
        /// </summary>
        Update = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3
    }
}
