using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CommonLib
{
  public static class DbHelper
  {
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
      private static readonly string connString = Connection.connectionString;  //数据库连接字符

    /// <summary>
    /// 根据id查询对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="id">对象实例的Id(泛型：类型int或string)</param>
    /// <param name="idName">条件的字段名称（主键名）</param>
    /// <returns></returns>
    public static T QueryById<T, I>(I id, string idName = "Id")
    {
      Type type = typeof(T);
      string columnString = string.Join(",", type.GetProperties().Select(p => string.Format("[{0}]", p.Name)));
      string sqlString = string.Format("select {0} from [{1}] where {2}={3}", columnString, type.Name, idName, id.GetType().Name.ToString() == "String" ? ("'" + id.ToString() + "'") : id.ToString());
      var t = Activator.CreateInstance(type);
      using (SqlConnection conn = new SqlConnection(connString))
      {
        conn.Open();
        SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
        SqlDataReader reader = sqlCommand.ExecuteReader();
        reader.Read();
        SetValueByProperties(type, reader, t);
      }
      return (T)t;
    }

    /// <summary>
    /// 获取数据列表
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <returns></returns>
    public static List<T> QueryAll<T>()
    {
      Type type = typeof(T);
      string columnString = string.Join(",", type.GetProperties().Select(p => string.Format("[{0}]", p.Name)));
      string sqlString = string.Format("select {0} from [{1}]", columnString, type.Name);
      List<T> dataList = new List<T>();
      using (SqlConnection conn=new SqlConnection(connString))
      {
        conn.Open();
        SqlCommand sqlCommand = new SqlCommand(sqlString,conn);
        SqlDataReader reader = sqlCommand.ExecuteReader();
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            var t = Activator.CreateInstance(type);
            SetValueByProperties(type, reader, t);
            dataList.Add((T)t);
          }
        }
        else
        {
          return null;
        }
      }
      return dataList;
    }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public static List<T> Query<T, I>(I col, string colName = "Id")
        {
            Type type = typeof(T);
            string columnString = string.Join(",", type.GetProperties().Select(p => string.Format("[{0}]", p.Name)));
            string sqlString = string.Format("select {0} from [{1}] where {2}={3}", columnString, type.Name, colName, colName.GetType().Name.ToString()=="String" ? ("'" + col.ToString() + "'") : col.ToString());
            List<T> dataList = new List<T>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var t = Activator.CreateInstance(type);
                        SetValueByProperties(type, reader, t);
                        dataList.Add((T)t);
                    }
                }
                else
                {
                    return null;
                }
            }
            return dataList;
        }

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象实例</param>
        /// <param name="idName">不插入的字段（自增键名）</param>
        /// <returns></returns>
        public static bool Insert<T>(T t, string idName = "Id")
    {
      Type type = typeof(T);
      string sqlString = "insert [{0}] ({1}) values ({2})";
      string columnString = string.Join(",", type.GetProperties().Where(p => p.Name != idName).Select(p => string.Format("[{0}]", p.Name)));
      string valueString = string.Join(",", type.GetProperties().Where(p => p.Name != idName).Select(p => string.Format("@{0}", p.Name)));
      sqlString = string.Format(sqlString, type.Name, columnString, valueString);
      using (SqlConnection conn = new SqlConnection(connString))
      {
        conn.Open();
        SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
        SqlParameter[] sqlParameter = type.GetProperties().Where(p => p.Name != idName).Select(p=>new SqlParameter(string.Format("@{0}",p.Name),p.GetValue(t,null)??DBNull.Value)).ToArray();
        sqlCommand.Parameters.AddRange(sqlParameter);
        return sqlCommand.ExecuteNonQuery() > 0;
      }
    }

    /// <summary>
    /// 修改对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="t">对象实例</param>
    /// <param name="idName">自增键名或条件名</param>
    /// <returns></returns>
    public static bool Update<T>(T t, string idName = "Id")
    {
      Type type = typeof(T);
      string sqlString = "update [{0}] set {1} where {2}={3}";
      string setString = string.Join(",", type.GetProperties().Where(p => p.Name != idName).Select(p => string.Format("[{0}]=@{0}", p.Name)));
      sqlString = string.Format(sqlString, type.Name, setString, idName,"@"+idName);
      using (SqlConnection conn = new SqlConnection(connString))
      {
        conn.Open();
        SqlCommand sqlCommand = new SqlCommand(sqlString, conn);
        SqlParameter[] sqlParameter = type.GetProperties().Select(p => new SqlParameter(string.Format("@{0}", p.Name), p.GetValue(t, null) ?? DBNull.Value)).ToArray();
        sqlCommand.Parameters.AddRange(sqlParameter);
        return sqlCommand.ExecuteNonQuery() > 0;
      }
    }

    /// <summary>
    /// 设置值by属性（SQLreader）
    /// </summary>
    /// <param name="type">对象类型</param>
    /// <param name="reader">sqlreader</param>
    /// <param name="t">对象</param>
    private static void SetValueByProperties(Type type, SqlDataReader reader, object t)
    {
      foreach (var item in type.GetProperties())
      {
        if (reader[item.Name] is DBNull) //判空
        {
          item.SetValue(t, null,null);
        }
        else
        {
          item.SetValue(t, reader[item.Name],null);
        }
      }
    }
  }
}