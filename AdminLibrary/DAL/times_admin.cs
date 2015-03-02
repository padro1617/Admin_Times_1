using System;
using System.Data;
using System.Data.Common;
using AdminLibrary.Model;
using System.Collections.Generic;
namespace AdminLibrary.DAL
{

    internal class TimesAdmin : BaseDB
    {
        internal IList<times_admin> SelectList()
        {
            DbCommand dbCmd = db.GetStoredProcCommand("timesadmin_select");

            IList<times_admin> list = new List<times_admin>();
            //db.AddInParameter(dbCmd, "TradeID", DbType.Byte, tradeID);
            //db.AddInParameter(dbCmd, "CategoryTypeID", DbType.Byte, categoryType);
            //db.AddInParameter(dbCmd, "ParentID", DbType.Int32, parentID);
            //db.AddInParameter(dbCmd, "IsShow", DbType.Boolean, isShow);
            //db.AddInParameter(dbCmd, "IsAll", DbType.Boolean, isAll);

            using (IDataReader dr = db.ExecuteReader(dbCmd))
            {
                while (dr.Read())
                {
                    list.Add(base.CreateEntity<times_admin>(dr));
                }
                dr.Dispose();
            }
            return list;
        }

        internal times_admin GetInfo(int user_id)
        {
            DbCommand dbCmd = db.GetStoredProcCommand("timesadmin_getinfo");

            times_admin info = new times_admin();
            db.AddInParameter(dbCmd, "user_id", DbType.Byte, user_id);
            using (IDataReader dr = db.ExecuteReader(dbCmd))
            {
                if (dr.Read())
                {
                    info = CreateEntity<times_admin>(dr);
                }
                dr.Dispose();
            }
            return info;
        }

        /// <summary>
        /// 登录检测
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal times_admin LoginCheck(string user_name, string password)
        {
            var os = new OperationStatus();
            DbCommand dbCmd = db.GetStoredProcCommand("timesadmin_logincheck");
            db.AddInParameter(dbCmd, "user_name", DbType.String, user_name);
            db.AddInParameter(dbCmd, "password", DbType.String, password);
            //1 代表存在用户名和密码  0 代表登录失败
            db.AddOutParameter(dbCmd, "result", DbType.Int32, 0);

            using (IDataReader dr = db.ExecuteReader(dbCmd))
            {
                if (db.GetParameterValue(dbCmd, "result") != DBNull.Value)
                {
                    int result = Convert.ToInt32(db.GetParameterValue(dbCmd, "result"));
                    if (result>0 && dr.Read())
                    {
                        times_admin info = CreateEntity<times_admin>(dr);
                        return info;
                    }
                }
                dr.Dispose();
            }
            return null;
        }
        /// <summary>
        /// 添加管理员信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal OperationStatus Insert(times_admin info)
        {
            var os = new OperationStatus();
            try
            {
                DbCommand dbCmd = db.GetStoredProcCommand("timesadmin_insert");

                db.AddInParameter(dbCmd, "user_name", DbType.String, info.user_name);
                db.AddInParameter(dbCmd, "password", DbType.String, info.password);
                db.AddInParameter(dbCmd, "email", DbType.String, info.email);
                db.AddInParameter(dbCmd, "last_ip", DbType.String, info.last_ip);

                db.AddOutParameter(dbCmd, "result", DbType.Int32, 0);
                db.ExecuteNonQuery(dbCmd);

                if (db.GetParameterValue(dbCmd, "result") != DBNull.Value)
                {
                    int result = Convert.ToInt32(db.GetParameterValue(dbCmd, "result"));
                    switch (result)
                    {
                        case -2:
                            os.Status = OptStatus.Repeat;
                            os.Message = "[timesadmin_insert]已存在相同名称的信息";
                            break;
                        default:
                            os.Flag = result;
                            os.Status = OptStatus.Success;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                os = new OperationStatus(ex);
            }
            return os;
        }
        /// <summary>
        /// 编辑管理员信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        internal OperationStatus Update(times_admin info)
        {
            var os = new OperationStatus();
            try
            {
                DbCommand dbCmd = db.GetStoredProcCommand("timesadmin_update");

                db.AddInParameter(dbCmd, "user_id", DbType.Byte, info.user_id);
                db.AddInParameter(dbCmd, "user_name", DbType.String, info.user_name);
                db.AddInParameter(dbCmd, "password", DbType.String, info.password);
                db.AddInParameter(dbCmd, "email", DbType.String, info.email);
                db.AddInParameter(dbCmd, "last_ip", DbType.String, info.last_ip);

                db.AddOutParameter(dbCmd, "result", DbType.Int32, 0);
                db.ExecuteNonQuery(dbCmd);

                if (db.GetParameterValue(dbCmd, "result") != DBNull.Value)
                {
                    int result = Convert.ToInt32(db.GetParameterValue(dbCmd, "result"));
                    switch (result)
                    {
                        case -2:
                            os.Status = OptStatus.Repeat;
                            os.Message = "[timesadmin_update]已存在相同名称的信息";
                            break;
                        case 0:
                            os.Status = OptStatus.Fail;
                            os.Message = "[timesadmin_update] " + info.user_id.ToString()+ " 更新失败";
                            break;
                        default:
                            os.Flag = info.user_id;
                            os.Status = OptStatus.Success;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                os = new OperationStatus(ex);
            }
            return os;
        }

    }
}
