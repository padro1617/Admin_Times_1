using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AdminLibrary.DAL;
using AdminLibrary.Model;

namespace AdminLibrary.BLL
{
    public class TimesAdmin
	{
        private static readonly DAL.TimesAdmin dal = new DAL.TimesAdmin();
        /// <summary>
        /// 获取管理员列表集合
        /// </summary>
        /// <returns></returns>
		public static IList<times_admin> Select() {
			return dal.Select();
		}

        /// <summary>
        /// 获取单独的管理员信息
        /// </summary>
        /// <param name="adId"></param>
        /// <returns></returns>
        public static times_admin GetInfo(byte user_id)
        {
            return dal.GetInfo(user_id);
		}

		/// <summary>
		/// 判断用户名和密码是否正确
		/// 争取就返回对于记录
		/// </summary>
		/// <param name="user_id"></param>
		/// <returns>为NULL代表登录失败</returns>
		public static OperationStatus Login( string user_name, string password ) {
			return dal.Login( user_name, password );
		}
        /// <summary>
        /// 判断用户名和密码是否正确
        /// 争取就返回对于记录
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns>为NULL代表登录失败</returns>
        public static times_admin LoginCheck(string user_name, string password)
        {
            return dal.LoginCheck(user_name,password);
        }
        /// <summary>
        /// 插入管理员信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
		public static OperationStatus Insert( times_admin info ) {
            return dal.Insert(info);
		}

		/// <summary>
		/// 更新管理员信息
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public static OperationStatus Update( times_admin info ) {
            return dal.Update(info);
		}
		/// <summary>
		/// 删除广告位图片信息
		/// </summary>
        /// <param name="user_id">ID</param>
		/// <returns></returns>
		public static OperationStatus Delete( byte user_id ) {
			return dal.Delete( user_id );
		}
	}
}
