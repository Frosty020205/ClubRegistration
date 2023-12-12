using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery  clubRegistrationQuery =  new ClubRegistrationQuery();
        private int ID, Age;
        private int count= 0;
        private string FirstName, MiddleName, LastName, Gender, Program;
        private long StudentId;
        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {

        }

        private void button_Register_Click(object sender, EventArgs e)
        {
            ID = RegistrationID();
            StudentId = Convert.ToInt64(txtStudent.Text);
            FirstName = txt1FirstName.Text;
            MiddleName = txt1MiddleName.Text;
            LastName = txt1LastName.Text;
            Age = Convert.ToInt32(txt1Age.Text);
            Gender = cbGender.Text;
            Program = cboProgram.Text;
            clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName,
            MiddleName, LastName, Age, Gender, Program);

        }
        private void button_Refresh_Click(object sender, EventArgs e)
        {
            RefreshListofClubMembers();
        }
        private void button_Update_Click(object sender, EventArgs e)
        {
            FrmUpdateMember woah = new FrmUpdateMember();
            woah.Show();
        }
        public void RefreshListofClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }
        public int RegistrationID()
        {
            return ++count;
        }
    }
}
