using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetEmployeeApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        string connectionString = "Data Source=localhost;Initial Catalog=employeeDB;Integrated Security=True;";

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                string query = "SELECT * FROM [dbo].[employeesTable]";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //textEmpID.Text = reader.GetInt32(0).ToString();
                    //textEmpName.Text = reader.GetString(1);
                    //textEmpContact.Text = reader.GetInt32(2).ToString();
                    //textEmpCity.Text = reader.GetString(3);

                    textEmpID.Text = reader["empID"].ToString();
                    textEmpName.Text = reader["empName"].ToString();
                    textEmpContact.Text = reader["empContact"].ToString();
                    textEmpCity.Text = reader["empCity"].ToString();

                }
                reader.Close();
                employeeCount();

            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }

        }


        private void btnInsert_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                string query = "INSERT INTO [dbo].[employeesTable] VALUES (@empID,@empName,@empContact,@empCity)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@empID", textEmpID.Text);
                command.Parameters.AddWithValue("@empName", textEmpName.Text);
                command.Parameters.AddWithValue("@empContact", textEmpContact.Text);
                command.Parameters.AddWithValue("@empCity", textEmpCity.Text);

                connection.Open();
                int rowChanged = command.ExecuteNonQuery();
                if (rowChanged > 0)
                {
                    MessageBox.Show("Record Inserted..");
                    employeeCount();

                }
                else{ MessageBox.Show("Record was not Inserted.."); }
            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
            finally{ connection.Close(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                string query = "UPDATE [dbo].[employeesTable] SET [empName] = @empName, [empContact] = @empContact, [empCity] = @empCity WHERE [empID] = @empID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@empID", textEmpID.Text);
                command.Parameters.AddWithValue("@empName", textEmpName.Text);
                command.Parameters.AddWithValue("@empContact", textEmpContact.Text);
                command.Parameters.AddWithValue("@empCity", textEmpCity.Text);

                connection.Open();
                int rowChanged = command.ExecuteNonQuery();
                if (rowChanged > 0)
                {
                    MessageBox.Show("Record Updated..");
                    employeeCount();

                }
                else { MessageBox.Show("Record was not Updated.."); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                string query = "DELETE FROM [dbo].[employeesTable] WHERE [empID] = @empID";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@empID", textEmpID.Text);

                connection.Open();
                int rowChanged = command.ExecuteNonQuery();
                if (rowChanged > 0)
                {
                    MessageBox.Show("Record Deleted..");
                    employeeCount();
                }
                else { MessageBox.Show("Record was not Deleted.."); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
        }

        private void employeeCount()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                string query = "SELECT COUNT(*) FROM [dbo].[employeesTable]";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                int countOfEmployees = Convert.ToInt32(command.ExecuteScalar());
                if (countOfEmployees > 0)
                {
                    //MessageBox.Show("Records Exist..");
                    labelResult.Text = "Count of Employees: " + countOfEmployees;

                }
                else { MessageBox.Show("Records don't exist..");
                    labelResult.Text = "No employee data";

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { connection.Close(); }
    }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM [dbo].[employeesTable]";

            SqlDataAdapter sda = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            sda.Fill(ds, "[dbo].[employeesTable]");
            dataGridView1.DataSource = ds.Tables["[dbo].[employeesTable]"].DefaultView;

            connection.Close();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM [dbo].[employeesTable]";

            SqlDataAdapter sda = new SqlDataAdapter(query, connection);
            DataSet ds = new DataSet();

            //Console.WriteLine(ds.IsInitialized);

            sda.Fill(ds, "[dbo].[employeesTable]");
            dataGridView1.DataSource = ds.Tables["[dbo].[employeesTable]"];

            connection.Close();

        }
    }
}
