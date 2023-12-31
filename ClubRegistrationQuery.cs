﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ComboBox;

namespace ClubRegistration
{
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;
        public DataTable dataTable;
        public BindingSource bindingSource;
        private string connectionString;
        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age;

        public ClubRegistrationQuery()
        {
            connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\deane\source\repos\ClubRegistration\ClubDB.mdf; Integrated Security = True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }
        public bool DisplayList()
        {
            string ViewClubMembers = "SELECT StudentId, FirstName, MiddleName,LastName, Age, Gender, Program FROM ClubMembers";
            sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);
            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;
            return true;
        }

        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            sqlCommand = new SqlCommand("Insert Into ClubMembers VALUES(@ID, @StudentID, @FirstName, " + "@MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.Add("@RegistrationID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("StudentID", SqlDbType.VarChar).Value = StudentID;
            sqlCommand.Parameters.Add("FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.Add("MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.Add("LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.Add("Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.Add("Program", SqlDbType.VarChar).Value = Program;
            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }

        public bool UpdateStudent(long StudentID, int Age, string Program,string FirstName, string LastName,string MiddleName, string Gender)
        {
            sqlCommand = new SqlCommand("UPDATE ClubMembers SET Age = @Age, Program = @Program, FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, Gender = @Gender  WHERE StudentId = @StudentId", sqlConnect);
           
            sqlCommand.Parameters.AddWithValue("@StudentId", StudentID);
            sqlCommand.Parameters.AddWithValue("@Age", Age);
            sqlCommand.Parameters.AddWithValue("@Program", Program);
            sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", LastName);
            sqlCommand.Parameters.AddWithValue("@MiddleName", MiddleName);
            sqlCommand.Parameters.AddWithValue("@Gender", Gender);

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }
        public void DisplayID(ComboBox comboBox)
        {
            string getId = "SELECT * FROM ClubMembers";
            sqlCommand = new SqlCommand(getId, sqlConnect);
            sqlConnect.Open();
            sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox.Items.Add(sqlReader["StudentId"]);
            }
            sqlConnect.Close();
        }
        public void DisplayText(string id)
        {
            string getId = "SELECT * FROM ClubMembers WHERE StudentId = @Id";
            sqlCommand = new SqlCommand(getId, sqlConnect);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlConnect.Open();
            sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                _FirstName = sqlReader.GetString(2);
                _MiddleName = sqlReader.GetString(3);
                _LastName = sqlReader.GetString(4);
                _Age = sqlReader.GetInt32(5);
                _Gender = sqlReader.GetString(6);
                _Program = sqlReader.GetString(7);
            }
            sqlConnect.Close();
        }
        

            public void GetStudentData(long studentId)
            {
                string ClubDBConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\deane\source\repos\ClubRegistration\ClubDB.mdf;Integrated Security=True";

                using (SqlConnection sqlConnect = new SqlConnection(ClubDBConnectionString))
                {
                    string query = "SELECT * FROM ClubMembers WHERE StudentId = @StudentId";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnect);
                    sqlCommand.Parameters.AddWithValue("@StudentId", studentId);

                    sqlConnect.Open();

                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                    if (sqlReader.Read())
                    {
                        _FirstName = sqlReader["FirstName"].ToString();
                        _MiddleName = sqlReader["MiddleName"].ToString();
                        _LastName = sqlReader["LastName"].ToString();
                        _Age = Convert.ToInt32(sqlReader["Age"]);
                        _Gender = sqlReader["Gender"].ToString();
                        _Program = sqlReader["Program"].ToString();
                    }
                    else
                    {
                    MessageBox.Show("StudentID not found!");
                    }

                    sqlConnect.Close();
                }
            }
        

    }
}

    

