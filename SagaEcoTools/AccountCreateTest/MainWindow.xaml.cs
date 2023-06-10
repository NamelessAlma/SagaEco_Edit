// こっちは邪魔なら消してください

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using MySql.Data.MySqlClient;

namespace AccountManagerTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class AccountCreateInfo
        {
            // 接続情報生成
            public string server = "";
            public string database = "";
            public string portNo = "";
            public string user = "";
            public string pass = "";
            public string charset = "utf8";


            // アカウント情報...
            public string loginId = "";
            public string password = "";
            public string deleteKey = "";
            public string gmAdminLevel = "";
        }

        public void writeLog(string text)
        {
            LogTextBox.AppendText(text + "\n");
        }

        // アカウント作成
        private void AccountCreateButton_Click(object sender, RoutedEventArgs e)
        {
            // ログエリア削除
            LogTextBox.Text = "";

            AccountCreateInfo accountInfo = new AccountCreateInfo();
            // 接続情報生成
            accountInfo.server = ServerIpAddressTextBox.Text;
            accountInfo.database = DbNameTextBox.Text;
            accountInfo.portNo = ServerPortNumberTextBox.Text;
            accountInfo.user = DbUserIdTextBox.Text;
            accountInfo.pass = DbUserPassTextBox.Text;
            accountInfo.charset = "utf8";


            // アカウント情報...
            accountInfo.loginId = loginIdTextBox.Text;
            accountInfo.password = PasswordTextBox.Text;
            accountInfo.deleteKey = CharDeleteKeyTextBox.Text;
            accountInfo.gmAdminLevel = GMAdminLevelTextBox.Text;

            // 入力チェックを行いOKならキャラクター作成に進む
            if(enterCheck(accountInfo))
            {
                string connectionString = string.Format($"Server={accountInfo.server};Port={accountInfo.portNo};Database={accountInfo.database};Uid={accountInfo.user};Pwd={accountInfo.pass};Charset={accountInfo.charset}");
                writeLog($"アカウント作成開始～");
                writeLog($"接続文字列 : {connectionString}");
                try
                {
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();
                    writeLog("接続成功！");

                    // アカウント作成テスト
                    DataTable tbl = new DataTable();

                    // id重複確認
                    // SELECT COUNTで楽々だと思ったら何故か必ずカウント数が1になる不具合がでたので
                    // しかたなく、SELECTでデータ引っ張ってきてrawがあるかどうかで重複判定しています
                    MySqlCommand dataAdapter = new MySqlCommand($"SELECT username FROM `login` WHERE username = '{accountInfo.loginId}'", connection);
                    var reader = dataAdapter.ExecuteReader();

                    if (reader.HasRows)
                    {
                        writeLog($"{accountInfo.loginId}さんは既に存在しているようです！");
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        writeLog($"アカウントを登録します!");
                        MySqlTransaction transaction;
                        
                        string sqlCmdStr = $"INSERT INTO `login`(`username`,`password`,`deletepass`,`gmlevel`) VALUES ('{accountInfo.loginId}','{accountInfo.password}','{accountInfo.deleteKey}',{accountInfo.gmAdminLevel})";
                        MySqlCommand sqlCmd = new MySqlCommand(sqlCmdStr, connection);
                        transaction = sqlCmd.Connection.BeginTransaction(IsolationLevel.ReadCommitted);
                        try
                        {
                            sqlCmd.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch(MySqlException sqlEx)
                        {
                            writeLog(sqlEx.ToString());
                            transaction.Rollback();
                        }
                        
                    }

                    connection.Close();
                    writeLog("接続を終了します");
                }
                catch (Exception ex)
                {
                    writeLog("例外が発生しました… : " + ex.ToString());
                }
            }


            
            
        }

        private void AccountDeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        // 入力チェック
        private bool enterCheck(AccountCreateInfo accountInfo)
        {
            bool result = true;
            int checkInt;
            if (String.IsNullOrEmpty(accountInfo.server))
            {
                writeLog("IPアドレス名を入力してください!");
                result = false;
            }
            if(String.IsNullOrEmpty(accountInfo.database))
            {
                writeLog("データベース名を入力してください!");
                result = false;
            }
            if(String.IsNullOrEmpty(accountInfo.portNo) || !int.TryParse(accountInfo.portNo,out checkInt))
            {
                writeLog("ポート番号を数字で入力してください!");
                result= false;
            }
            if(String.IsNullOrEmpty(accountInfo.user))
            {
                writeLog("DBユーザー名を入力してください！");
                result = false;
            }
            if(String.IsNullOrEmpty(accountInfo.pass))
            {
                writeLog("DBユーザーのパスワードを入力してください！");
                result = false;
            }

            // アカウント情報チェック
            if(String.IsNullOrEmpty(accountInfo.loginId))
            {
                writeLog("ログインIDを入力してください！");
                result = false;
            }
            if(String.IsNullOrEmpty(accountInfo.password))
            {
                writeLog("ログインパスワードを入力してください！");
                result = false;
            }
            if(String.IsNullOrEmpty(accountInfo.deleteKey))
            {
                writeLog("キャラクター削除キーを入力してください！");
                result = false;
            }
            // GM権限レベルの下限と上限間違っている場合修正してください
            if(String.IsNullOrEmpty(accountInfo.gmAdminLevel)|| !int.TryParse(accountInfo.gmAdminLevel,out checkInt) || checkInt < 200 || checkInt < 0)
            {
                writeLog("GM権限レベルを0～200の間で入力してください");
                result = false;
            }

            return result;
        }
    }
}
