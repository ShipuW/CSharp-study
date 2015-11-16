using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class connection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "";
        //从web.config配置文件取出数据库连接
        string sqlconnstr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //建立数据库连接对象
        SqlConnection sqlconn = new SqlConnection(sqlconnstr);
        //建立Command对象
        SqlCommand sqlcommand = new SqlCommand();
        //给sqlcommand的Connection属性赋值
        sqlcommand.Connection = sqlconn;
        //打开连接
        sqlconn.Open();
        //把存储过程赋给Command对象
        sqlcommand.CommandText = "exec selePro '朱俊璋'";
        //建立DateReader对象，并返回查询结果
        SqlDataReader sqldatereader = sqlcommand.ExecuteReader();
        
        
        //逐行遍历查询结果
        while(sqldatereader.Read()){
            Label1.Text+=sqldatereader.GetString(0)+"";
            Label1.Text+=sqldatereader.GetString(1)+"";
             Label1.Text+=sqldatereader.GetString(2)+"";
             Label1.Text+=sqldatereader.GetString(3)+"";
             Label1.Text+=sqldatereader.GetString(4)+"";
             Label1.Text+=sqldatereader.GetString(5)+"";

        }
        sqlcommand = null;
        //关闭连接
        sqlconn.Close();

        sqlconn = null;
    }
}