using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClubRegistration
{
    public partial class FrmUpdateMember : Form
    {

        private ClubRegistrationQuery clubRegistrationQuery;
        private int Age;
        private string Program;
        private long StudentId;
        private int count = 0;
        private string FirstName, MiddleName, LastName, Gender;
       

        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        private void button_Confirm_Click(object sender, EventArgs e)
        {
            
            StudentId = Convert.ToInt64(comboxID.Text);
            Age = Convert.ToInt32(Agetxt.Text);
            Program = cbPrograms.Text;
            FirstName = FirstNametxt.Text;
            MiddleName = Middletxt.Text;
            LastName = LastNametxt.Text;
            Gender = cbGender.Text;
            clubRegistrationQuery.UpdateStudent(StudentId, Age, Program, FirstName, MiddleName, LastName, Gender);
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            this.clubMembersTableAdapter.Fill(this.clubDBDataSet.ClubMembers);
            clubRegistrationQuery = new ClubRegistrationQuery();
            clubRegistrationQuery.DisplayID(comboxID);
            
        }

        private void comboxID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxID.SelectedItem != null)
            {
                long selectedStudentID = Convert.ToInt64(comboxID.SelectedItem);
               // clubRegistrationQuery.GetStudentData(selectedStudentID); // This method should retrieve the data of the selected student ID from the database
                clubRegistrationQuery.GetStudentData(selectedStudentID);
                TextFill();
            }
        }

        public void Fill()
        {
            string ClubDBConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\deane\source\repos\ClubRegistration\ClubDB.mdf; Integrated Security = True";
            SqlConnection sqlConnect = new SqlConnection(ClubDBConnectionString);
            string getId = "SELECT * FROM ClubMembers";
            SqlCommand sqlCommand = new SqlCommand(getId, sqlConnect);
            sqlConnect.Open();
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                comboxID.Items.Add(sqlReader["StudentId"]);
            }
            sqlConnect.Close();
        }
        public void TextFill()
        {
            LastNametxt.Text = clubRegistrationQuery._LastName;
            FirstNametxt.Text = clubRegistrationQuery._FirstName;
            Middletxt.Text = clubRegistrationQuery._MiddleName;
            Agetxt.Text = clubRegistrationQuery._Age.ToString();
            cbGender.Text = clubRegistrationQuery._Gender;
            cbPrograms.Text = clubRegistrationQuery._Program;
        }
        public void cbFill()
        {
            clubRegistrationQuery.DisplayID(comboxID);
        }

    }
}
