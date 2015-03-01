using System;

namespace AdminLibrary.Model
{
	/// <summary>
	/// 操作状态信息
	/// </summary>
    public class OperationStatus
    {
		/// <summary>
		/// 构造函数
		/// </summary>
        public OperationStatus() { }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="status"></param>
		/// <param name="message"></param>
		/// <param name="exp"></param>
        public OperationStatus(OptStatus status, string message, Exception exp)
            : this(status, message, exp, 0)
        {
        }

		/// <summary>
		/// 异常时候构造
		/// </summary>
		/// <param name="exp">异常</param>
        public OperationStatus(Exception exp)
            : this()
        {
            Status = OptStatus.Fail;
            Flag = 0;
            Message = exp.Message;
            Exception = exp.ToString();
        }

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="status"></param>
		/// <param name="message"></param>
		/// <param name="exp"></param>
		/// <param name="flag"></param>
        public OperationStatus(OptStatus status, string message, Exception exp, long flag)
        {
            Status = status;
            Flag = flag;
            Message = message;
            Exception = exp.ToString();
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public OptStatus Status { get; set; }

        /// <summary>
        /// 操作数字标记:当执行INSERT时为新记录编号
        /// </summary>
        public long Flag { get; set; }

        /// <summary>
        /// 执行时出现错误的相关信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 异常信息对象
        /// </summary>
        public string Exception { get; set; }

    }
}
