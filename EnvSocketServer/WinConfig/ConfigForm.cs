using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using System.IO;

namespace WinConfig
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();

            ConfigJson json = Config.ReadConfig();         

            this.txtAddress.Text = json.dbAddress;
            this.txtPort.Text = json.dbPort;
            this.txtPwd.Text = json.dbPwd;
            this.txtUserId.Text = json.dbUserid;
            this.txtInstance.Text = json.dbInstance;
            this.txtLimitedIp.Text = json.socketIps;
            this.txtSocketPort.Text = json.socketPort;

            this.txtServiceName.Text = json.serviceName;

            this.txtDebug.Text = json.debugLevel;
        }

        private void btnTestDbConnection_Click(object sender, EventArgs e)
        {
            string res = TestDbConnection();
            MessageBox.Show(res);
        }

        public string GetConnectionString()
        {
            //private string  ConnectionStringLW = "server=192.168.1.1,1430(端口号) ;database=DB;uid=sa;pwd=123456"    //数据库连接字符串

            string connStr = @"server=" + txtAddress.Text.ToString() + "," + txtPort.Text.ToString()
                + ";database=" + txtInstance.Text.ToString() + ";uid=" + txtUserId.Text.ToString() + ";pwd=" + this.txtPwd.Text.ToString();
            return connStr;
        }

        public string TestDbConnection()
        {
            string connString = GetConnectionString();
            string result = "";
            using (SqlConnection Conn = new SqlConnection(connString))
            {
                try
                {
                    //打开数据库连接
                    Conn.Open();
                    if (Conn.State == ConnectionState.Open)
                        result = "连接成功！";
                    else
                        result = "连接失败！";
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    result = "连接失败！" + ex.Message;
                }
                finally
                {
                    Conn.Close();
                    //LogUtil.Log(result);
                }
            }
            return result;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            StringBuilder json = new StringBuilder("");
            //StringWriter sw = new StringWriter();
            //JsonWriter writer = new JsonTextWriter(sw);
            json.Append("{\"serviceName\":\"" + txtServiceName.Text.ToString() + "\",");
            json.Append("\"dbAddress\":\"" + txtAddress.Text.ToString() + "\",");
            json.Append("\"dbPort\":\"" + txtPort.Text.ToString() + "\",");
            json.Append("\"dbInstance\":\"" + txtInstance.Text.ToString() + "\",");
            json.Append("\"dbUserid\":\"" + txtUserId.Text.ToString() + "\",");
            json.Append("\"dbPwd\":\"" + txtPwd.Text.ToString() + "\",");
            json.Append("\"socketIps\":\"" + this.txtLimitedIp.Text.ToString() + "\",");
            json.Append("\"socketPort\":\"" + txtSocketPort.Text.ToString() + "\",");
            json.Append("\"debugLevel\":\"" + this.txtDebug.Text.ToString() + "\"}");
            //string filePath = txtAccessFile.Text.ToString();
            //filePath = filePath.Replace("\\", "\\\\"); //json要用双斜杠
            //json.Append("\"accessfile\":\"" + filePath + "\"}");
            //string dir = System.Environment.CurrentDirectory;
            FileStream fs;
            StreamWriter sw;
            try
            {
                fs = new FileStream(Config.configFile, FileMode.Create);
                sw = new StreamWriter(fs);
                //开始写入
                sw.Write(json.ToString());
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                MessageBox.Show("保存配置成功,请重启服务!");
                //这个方法执行后，又开一个新的进程来进行服务，未能做到对同一个服务进行重启
                //ServiceHelper.StopService("JiaoBanZhanService");
                //ServiceHelper.StartService("JiaoBanZhanService");

                //this.txtDbCfgResutl.Text = "保存配置成功：\r\n文件：" + Config.configFile + "\r\n内容：" + json.ToString();
            }
            catch (Exception ex)
            {
                // this.txtDbCfgResutl.Text = "保存配置失败：" + ex.Message;
                MessageBox.Show("保存配置失败：" + ex.Message);
            }
            finally
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
