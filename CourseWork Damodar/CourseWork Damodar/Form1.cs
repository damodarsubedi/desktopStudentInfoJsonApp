using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork_Damodar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BindGrid();
            lblId.Visible = false;
            txtId.Visible = false;
            //txtId.Visible = false;
            btnUpdate.Visible = false;
            dataGridViewReport.Visible = false;
        }
        private void BindGrid()
        {
            Student obj = new Student();
            List<Student> listOfStudent = obj.List();
            DataTable datatable = Utility.ConvertToDataTable(listOfStudent);
            dataGridStudentInfo.DataSource = datatable;
            BindChart(listOfStudent);
        }
        
        private void GridRow_DbClick(object sender, DataGridViewRowEventArgs e)
        {
            int id = 0; //getting the Id of clicked Grid Data
            string myValue = dataGridStudentInfo[e.Row.Index, 0].Value.ToString();
            Student obj = new Student();
            List<Student> listStudents = obj.List();
            Student s = listStudents.Where(x => x.Id == id).FirstOrDefault();

        }

        private void BindChart(List<Student> listOfStudent)
        {
            if (listOfStudent != null)
            {
                var result = listOfStudent
                    .GroupBy(l => l.ProgramEnrolled)
                    .Select(cl => new
                    {
                        Program = cl.First().ProgramEnrolled,
                        Count = cl.Count().ToString()
                    }).ToList();
                DataTable dt = Utility.ConvertToDataTable(result);
                chart1.DataSource = dt;
                chart1.Name = "Program";
                chart1.Series["Series1"].XValueMember = "Program";
                chart1.Series["Series1"].YValueMembers = "Count";
                this.chart1.Titles.Remove(this.chart1.Titles.FirstOrDefault());
                this.chart1.Titles.Add("Weekly Enrollment Chart");
                chart1.Series["Series1"].IsValueShownAsLabel = true;
            }
            else
            {
                MessageBox.Show("Empty list of student");
            }
        }
       
        
 
        private void Clear()
        {
            txtId.Text = "";
            txtAddress.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtContactNo.Text = "";
            dtpRegistrationDate.Value = DateTime.Today;
            cbGender.SelectedItem = null;
            cbProgram.SelectedItem = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            Regex ex = new Regex("^[0-9]{10}");
            obj.Name = txtName.Text;
            obj.Address = txtAddress.Text;
            obj.Email = txtEmail.Text;
            obj.ContactNumber = txtContactNo.Text;
            
            if (cbGender.SelectedItem == null)
            {
                MessageBox.Show("select Gender");
            }
            else
            {
                obj.Gender = cbGender.SelectedItem.ToString();
            }

            obj.RegistrationDate = dtpRegistrationDate.Value;

            if (cbProgram.SelectedItem == null)
            {
                MessageBox.Show("Select Program");
            }
            else
            {
                obj.ProgramEnrolled = cbProgram.SelectedItem.ToString();
            }
            
            
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                //errorProvider1.SetError(txtEmail, "Email Should not be blank!");
                MessageBox.Show(txtEmail, "Email is empty");
            }
            else if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(txtName, "Name should be filled");
            }
            else if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show(txtAddress, "Address should be filled"); 
            }
            else if (string.IsNullOrWhiteSpace(txtContactNo.Text) || !ex.IsMatch(txtContactNo.Text))
            {
                //errorProvider1.SetError(txtContactNo, "Contact No Should not be blank!");
                MessageBox.Show(txtContactNo, "Contact is empty");
            }
            else if(cbGender.SelectedItem == null)
            {
                
            }
            else if(cbProgram.SelectedItem == null)
            {
                MessageBox.Show("Select Program");
            }
            else
            {
                obj.Create(obj);
                Clear();
                BindGrid();
            }
            
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Id = int.Parse(txtId.Text);
            obj.Name = txtName.Text;
            obj.Address = txtAddress.Text;
            obj.Email = txtEmail.Text;
            obj.ContactNumber = txtContactNo.Text;
            obj.Gender = cbGender.SelectedItem.ToString();
            obj.ProgramEnrolled = cbProgram.SelectedItem.ToString();
            obj.RegistrationDate = dtpRegistrationDate.Value;
            obj.Update(obj);
            BindGrid();
            Clear();
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            lblId.Visible = false;
            txtId.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridStudentInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Student student = new Student();
            if(e.ColumnIndex == 0)
            {
                string value = dataGridStudentInfo[2, e.RowIndex].Value.ToString();
                int id = 0;
                if (String.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Invalid Data");
                }
                else
                {
                    id = int.Parse(value);
                    Student s = student.List().Where(x => x.Id == id).FirstOrDefault();
                    txtId.Text = s.Id.ToString();
                    txtName.Text = s.Name;
                    txtAddress.Text = s.Address;
                    txtEmail.Text = s.Email;
                    txtContactNo.Text = s.ContactNumber;
                    dtpRegistrationDate.Value = s.RegistrationDate;
                    cbGender.SelectedItem = s.Gender;
                    cbProgram.SelectedItem = s.ProgramEnrolled;
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    lblId.Visible = true;
                    txtId.Visible = true;
                }
            }
            else if (e.ColumnIndex == 1)
            {
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                DialogResult result = MessageBox.Show("Do you want to delete this student", "Conform you Delete", buttons);
                if(result == DialogResult.OK)
                {
                    string value = dataGridStudentInfo[2, e.RowIndex].Value.ToString();
                    student.Delete(int.Parse(value));
                    BindGrid();
                    MessageBox.Show("student Record Deleted sucessfully");
                }
            }

        }

        private void NameSort_Click(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (cbSort.SelectedItem != null)
           { 
                if(cbSort.SelectedItem.ToString() == "Name")
                {
                    Student obj = new Student();
                    // Initial Student List
                    List<Student> listStudents = obj.List();
                    //Sorted List
                    List<Student> lst = obj.Sort(listStudents, "Name");
                    // Adding sorted List
                    DataTable dataTable = Utility.ConvertToDataTable(lst);
                    dataGridStudentInfo.DataSource = dataTable ;

                }
                else
                {
                    Student obj = new Student();
                    List<Student> listStudents = obj.List();
                    List<Student> lst = obj.Sort(listStudents, "Date of Registration ");
                    DataTable dataTable = Utility.ConvertToDataTable(lst);
                    dataGridStudentInfo.DataSource = dataTable;

                }
            }
            else
            {
                MessageBox.Show("You havent selected from option to sort");
            }
        }
    }
}
