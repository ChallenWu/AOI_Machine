using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demo.UserControls
{
    public partial class DataGripSQL : UserControlBase
    {
        public DataGripSQL()
        {
            InitializeComponent();
        }

        public void Insert()
        {
            //b1:
            string path = string.Format(@"Data Source = test.db; Version = 3");
            //b2:Tao ket noi toi database
            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                //B3: Open connect
                conn.Open();
                //b4:Cac cau lenh
                string strQuerry = string.Format("INSERT INTO sanpham (thoigian,sn,kq) VALUES({0},{1},{2});",DateTime.Now.ToString("yyyy-MM-dd"),1111,313);
                //B5: Tao ra command
                SQLiteCommand cmd = new SQLiteCommand(strQuerry, conn);
                //B6: Thuc hien command
                cmd.ExecuteNonQuery();
                //B7 Close
                conn.Close();
            }
        }
        public void Show()
        {
            //b1:
            string path = string.Format(@"Data Source = test.db; Version =3");
            //b2:Tao ket noi toi database
            using (SQLiteConnection conn = new SQLiteConnection(path))
            {
                //B3: Open connect
                conn.Open();
                //b4:Cac cau lenh
                string strQuerry = string.Format("SELECT * from sanpham;");
                //B5: Tao ra command
                SQLiteCommand cmd = new SQLiteCommand(strQuerry, conn);
                //B6: Thuc hien command
                cmd.ExecuteNonQuery();
                //b7: Hung du lieu
                DataTable dt = new DataTable();
                SQLiteDataAdapter adpater = new SQLiteDataAdapter(cmd);
                adpater.Fill(dt);
                dataGridView1.DataSource = dt.DefaultView;
                //B7 Close
                conn.Close();
            }
        }

        public void CreatSample()
        {
           
            using (var conn = Dba.GetConnection())
            {
                var sql = "INSERT INTO sanpham (thoigian, sn, kq) VALUES (@thoigian, @sn, @kq)";
                using (var sqlCmd = conn.CreateCommand())
                {
                    try
                    {
                        sqlCmd.CommandText = sql;
                        sqlCmd.Parameters.AddWithValue("@thoigian", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff"));
                        sqlCmd.Parameters.AddWithValue("@sn", "12134");
                        sqlCmd.Parameters.AddWithValue("@kq", "pass");

                        conn.Open();
                        sqlCmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreatSample();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Show();
        }
    }

    class Dba
    {
        public static SQLiteConnection GetConnection()
        {
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "test.db");
            var dbConnectionString = String.Format("Data Source={0};Mode=ReadWrite;", dbPath);
            var conn = new SQLiteConnection(dbConnectionString);
            conn.DefaultTimeout = 10;
            return conn;
        }
    }
}
