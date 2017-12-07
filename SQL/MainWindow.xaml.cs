using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQL
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            createNewDatabase();
            //connectToDatabase();
            createTable();
            fillTable();
            printHighscores();
        }

        SQLiteConnection m_dbConnection; 
        
        //创建一个空的数据库
        void createNewDatabase()
        {
            try
            {
                Debug.WriteLine("OldDatabase");
                m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                m_dbConnection.Open();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NewDatabase");
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
                m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                m_dbConnection.Open();
            }
          
        }

        ////创建一个连接到指定数据库
        //void connectToDatabase()
        //{
        //    m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
        //    m_dbConnection.Open();
        //}

        //在指定数据库中创建一个table
        void createTable()
        {
            string sqlSelect = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sqlSelect, m_dbConnection);         
            try
            {
                command.ExecuteScalar();
                Debug.WriteLine("exist");

            }
            catch
            {
                string sql = "create table highscores (name varchar(20), score int)";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                Debug.WriteLine("not exist");
            }
        }

        //插入一些数据
        void fillTable()
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        DataView dataView;
        //使用sql查询语句，并显示结果
        void printHighscores()
        {
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataView = dataTable.DefaultView;
            datagrid.ItemsSource = dataView;
            //while (reader.Read())
            //{

            //    Debug.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);

            //}

            //Debug.ReadLine();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            {
                string sql = "insert into highscores (name, score) values ('NEW', 1111)";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                dataView.Table.Rows.Add("NEW2", 11112);
            }
            //{
            //    string sql = "select * from highscores order by score desc";
            //    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            //    SQLiteDataReader reader = command.ExecuteReader();
            //    DataTable dataTable = new DataTable();
            //    dataTable.Load(reader);
            //    datagrid.ItemsSource = dataTable.DefaultView;
            //}
        }

        private void RemoveAll()
        {
            
        }

    
    }
}
