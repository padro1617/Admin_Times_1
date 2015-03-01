using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace AdminLibrary.DAL
{
    /// <summary>
    /// 数据交互基类
    /// </summary>
    internal class BaseDB
    {

        protected Database db = DatabaseFactory.CreateDatabase("Admin_Times_1Connection");


        /// <summary>
        /// 创建实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected T CreateEntity<T>(IDataReader dr) where T : new() {
            T info = new T();
            for (int i = 0; i < dr.FieldCount; i++) {
                if (dr[i].Equals(DBNull.Value)) continue;
                if (typeof(T).GetProperty(dr.GetName(i)) != null)
                    typeof(T).GetProperty(dr.GetName(i)).SetValue(info, dr[i], null);
            }
            return info;
        }

        /// <summary>
        /// 泛型转table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        protected static DataTable ToDataTable<T>(List<T> entitys) {

            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1) {
                throw new Exception("需转换的集合为空");
            }

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            var entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            var dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++) {
                dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys) {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType) {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                var entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++) {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                } dt.Rows.Add(entityValues);
            }
            return dt;
        }

        /// <summary>
        /// 将泛型类转换成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objlist"></param>
        /// <returns></returns>
        protected static DataTable Fill<T>(IList<T> objlist) where T : class {
            if (objlist == null || objlist.Count <= 0) {
                return null;
            }
            var dt = new DataTable(typeof(T).Name);


            PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (T t in objlist) {


                if (t == null) {
                    continue;
                }

                DataRow row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++) {
                    var pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null) {
                        var column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// DataSet转泛型  使用泛型类List，不用ArrayList，比下面Fill方法的性能提高 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected static IList<T> FillModel<T>(DataTable dt) where T : new() {
            var l = new List<T>();
            if (dt != null) {
                T model = new T();
                foreach (DataRow dr in dt.Rows) {
                    model = Activator.CreateInstance<T>();
                    foreach (DataColumn dc in dr.Table.Columns) {

                        var pi = model.GetType().GetProperty(dc.ColumnName);
                        if (dr[dc.ColumnName] != DBNull.Value)
                            pi.SetValue(model, dr[dc.ColumnName], null);
                        else
                            pi.SetValue(model, null, null);

                    }
                    l.Add(model);
                }
            }

            return l;
        }
    }
}
