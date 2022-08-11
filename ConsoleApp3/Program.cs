using System.Data;
using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Collections;

namespace ConsoleForDb0701
{
   
    class Program
    {
        static void Main(string[] args)
        {
            String board = "";
            while (board != "5")
            {
                Console.WriteLine("輸入您的操作");
                Console.WriteLine("新增按1");
                Console.WriteLine("查詢按2");
                Console.WriteLine("修改按3");
                Console.WriteLine("刪除按4");
                Console.WriteLine("結束按5");
                board = Console.ReadLine();

                switch (board)
                {
                    case "1":
                        Create();
                        break;
                    case "2":
                        Read();
                        break;
                    case "3":
                        Update();
                        break;
                    case "4":
                        Delete();
                        break;
                }
            }
        }
        public static void Read()//查詢
        {
            using(SqlConnection db =
            new SqlConnection("Data Source=(//YOURCOMPUTER);Initial Catalog=tempdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                db.Open();
                try
                {
                    Console.WriteLine("db can open!!!");
                    SqlCommand queryMembers = new SqlCommand("select * from dbo.Users", db);
                    //queryMembers.ExecuteNonQuery();
                    //Console.WriteLine(queryMembers.ToString());
                    SqlDataReader Reader = queryMembers.ExecuteReader();
                    int counter = 1;//紀錄user人數
                    while (Reader.Read())
                    {
                        if (!Reader.Equals(DBNull.Value))//是否為空值
                        {
                            Console.WriteLine("第" + counter + "位:");
                            Console.Write("ID:");
                            Console.WriteLine(Reader[0].ToString());
                            Console.Write("Account:");
                            Console.WriteLine(Reader[1].ToString());
                            Console.Write("First Name:");
                            Console.WriteLine(Reader[4].ToString());
                            Console.Write("Last Name:");
                            Console.WriteLine(Reader[5].ToString());
                            Console.WriteLine(" ");
                            Console.WriteLine(" ");
                            counter++;
                        }

                    }
                    Reader.Close();
                    counter = 0;
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine("\nMessage ---\n{0}", e.Message);
                    Console.WriteLine("error db could not open ");
                }
                db.Close();
         
            }
        }
        public static void Create()//新增
        {
            using (SqlConnection db =
            new SqlConnection("Data Source=DESKTOP-V3D7KIT;Initial Catalog=tempdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                db.Open();
                try
                {
                    //Guid ID = Guid.NewGuid();
                    Console.Write("請輸入Account:");
                    String Account = Console.ReadLine();
                    Console.Write("請輸入Password:");
                    String Password = Console.ReadLine();
                    String HashKey = Password.GetHashCode().ToString();
                    Console.Write("請輸入FirstName:");
                    String FirstName = Console.ReadLine();
                    Console.Write("請輸入LastName:");
                    String LastName = Console.ReadLine();
                    Console.Write("請輸入Email:");
                    String Email = Console.ReadLine();
                    Console.Write("請輸入Title:");
                    String Title = Console.ReadLine();
                    String b_empno="";
                    //查詢管理員id 
                    SqlCommand Admin = new SqlCommand("Select [ID] from[dbo].[Users]where[Account] = 'Admin' ", db);
                    SqlDataReader Reader = Admin.ExecuteReader();
                    while (Reader.Read())
                    {
                        if (!Reader.Equals(DBNull.Value))//是否為空值
                        {
                            b_empno= Reader[0].ToString(); 
                        }

                    }
                    Reader.Close();
                    //String b_empno ="F9C2F7F4 - CF1A - 41C0 - B767 - 0D5217E8863F";
                    Guid b_Guid = Guid.Parse(b_empno);
                    var b_date = DateTime.Now ;
                    //2021-12-10 11:29:28.013
                    //.ToString("yyyy-MM-dd HH:mm:ss.FFF");
                    String bempnoString = "CAST("+"'"+b_empno + "'" + " " + "AS UniqueIdentifier)";
                    String bdateString = "CAST(" + "'" + b_date + "'" + " " + "AS datetime)";
                    String commands =
                        @"INSERT INTO[dbo].[Users]( [ID],[Account],[Password],[HashKey],[FirstName],[LastName],[Email],[Title],[is_available],[IsDeleted],[b_empno],[b_date])
                        VALUES( NEWID() , @Account ,@Password, @HashKey ,@FirstName ,@LastName ,@Email,@Title ,@is_available 
                        ,@isDeleted ,@b_Guid,@b_date)";
                    Console.WriteLine(commands);
                    using (SqlCommand CreateMembers = new SqlCommand(commands, db))
                    {
                        CreateMembers.Parameters.AddWithValue("@Account", Account);
                        CreateMembers.Parameters.AddWithValue("@Password", Password);
                        CreateMembers.Parameters.AddWithValue("@HashKey", HashKey);
                        CreateMembers.Parameters.AddWithValue("@FirstName", FirstName);
                        CreateMembers.Parameters.AddWithValue("@LastName", LastName);
                        CreateMembers.Parameters.AddWithValue("@Email", Email);
                        CreateMembers.Parameters.AddWithValue("@Title", Title);
                        CreateMembers.Parameters.AddWithValue("@is_available", 1);
                        CreateMembers.Parameters.AddWithValue("@isDeleted", 0);
                        CreateMembers.Parameters.AddWithValue("@b_Guid", b_Guid);
                        CreateMembers.Parameters.AddWithValue("@b_date ", b_date);
                        
                       
                        CreateMembers.ExecuteNonQuery();
                        Console.WriteLine("新增成功");
                       
                    }
                    


                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine("\nMessage ---\n{0}", e.Message);
                    Console.WriteLine("error db could not open ");
                }
                db.Close();
            }
        }
        public static void Update()//修改
        {
            using (SqlConnection db =
            new SqlConnection("Data Source=DESKTOP-V3D7KIT;Initial Catalog=tempdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                db.Open();
                try
                {
                    Console.WriteLine("請問您要改甚麼帳戶呢?");
                    String Account = Console.ReadLine();
                    Console.WriteLine("請問您要改甚麼欄位呢?");
                    var Column = Console.ReadLine();
                    if(Column=="ID"|| Column == "is_available" || Column == "IsDeleted"||Column== "b_empno"||Column== "b_date")
                    {
                        Console.WriteLine("請不要亂改重要欄位");
                    }
                    else
                    {
                        Console.WriteLine("修改成?");
                        String updateColumn = Console.ReadLine();
                        Console.WriteLine(Account);
                        Console.WriteLine(Column);
                        Console.WriteLine(updateColumn);
                        String upcommands = "UPDATE [dbo].[Users] SET LastName = @updateColumn Where[Account] = @Account ";
                        using (SqlCommand UpdateMembers = new SqlCommand(upcommands, db))
                        {
                            UpdateMembers.Parameters.AddWithValue("@Column", Column);
                            UpdateMembers.Parameters.AddWithValue("@updateColumn", updateColumn);
                            UpdateMembers.Parameters.AddWithValue("@Account", Account);
                            UpdateMembers.ExecuteNonQuery();
                            Console.WriteLine("修改成功");
                        }
                            
                        
                    }
                    
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine("\nMessage ---\n{0}", e.Message);
                    Console.WriteLine("error db could not open ");
                }
                
                db.Close();
            }
        }
        public static void Delete()//刪除
        {
            using (SqlConnection db =
           new SqlConnection("Data Source=DESKTOP-V3D7KIT;Initial Catalog=tempdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                db.Open();
                try
                {
                    Console.WriteLine("請問您要刪除哪位使用者呢?");
                    String Account = Console.ReadLine();
                    SqlCommand DeleteMembers = new SqlCommand("DELETE FROM[dbo].[Users]Where[Account] = @Account", db);
                    DeleteMembers.Parameters.AddWithValue("@Account", Account);
                    DeleteMembers.ExecuteNonQuery();
                    Console.WriteLine("刪除成功");
                    //軟刪除String upcommands = "UPDATE [dbo].[Users] SET  d_empno= 1 Where[Account] = @Account ";
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine("\nMessage ---\n{0}", e.Message);
                    Console.WriteLine("error db could not open ");
                }

                db.Close();
            }
        }
    }
}
