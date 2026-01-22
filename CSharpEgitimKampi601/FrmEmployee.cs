using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;Password=Sevcanasel23.";
        void EmployeeList()
        {
            var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Employees";
            var command = new Npgsql.NpgsqlCommand(query, connection);
            var adapter = new Npgsql.NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
        void DepartmentList()
        {
            var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Departments";
            var command = new Npgsql.NpgsqlCommand(query, connection);
            var adapter = new Npgsql.NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeeDepartment.ValueMember = "DepartmentId";
            cmbEmployeeDepartment.DataSource = dataTable;
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeeList();

        }
        

        private void FrmEmployee_Load(object sender, EventArgs e)
        {
            EmployeeList();
            DepartmentList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string employeeName = txtEmployeeName.Text;
            string employeeSurname = txtEmployeeSurname.Text;
            decimal employeeSalary = decimal.Parse(txtEmployeeSalary.Text);
            int employeeDepartment = int.Parse(cmbEmployeeDepartment.SelectedValue.ToString());

            var connection = new Npgsql.NpgsqlConnection(connectionString);
            connection.Open();

            string query = "insert into Employees (EmployeeName,EmployeeSurname,EmployeeSalary,DepartmentId) " +
                           "values (@employeeName,@employeeSurname,@employeeSalary,@DepartmentId)";

            var command = new Npgsql.NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeeName", employeeName);
            command.Parameters.AddWithValue("@employeeSurname", employeeSurname);
            command.Parameters.AddWithValue("@employeeSalary", employeeSalary);

            // 🔴 BURASI HATALIYDI
            // command.Parameters.AddWithValue("@employeeDepartmentId", employeeDepartment);

            // ✅ DOĞRUSU
            command.Parameters.AddWithValue("@DepartmentId", employeeDepartment);

            command.ExecuteNonQuery();
            MessageBox.Show("Ekleme işlemi başarılı");

            connection.Close();
            EmployeeList();
        }
    }
}
