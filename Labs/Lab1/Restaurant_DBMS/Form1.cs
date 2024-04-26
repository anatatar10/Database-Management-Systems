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

namespace Restaurant_DBMS
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlDataAdapter daCustomers; //parent table
        SqlDataAdapter daOrders; //child table
        DataSet dSet;
        BindingSource bsCustomers;
        BindingSource bsOrders;

        SqlCommandBuilder cmdBuilder;

        string queryCustomers;
        string queryOrders;

        public Form1()
        {
            InitializeComponent();
            FillData();
        }

        void FillData()
        {
            //SqlConnection
            conn = new SqlConnection(getConnectionString());
            queryCustomers = "SELECT * FROM Customers";
            queryOrders = "SELECT * FROM Orders";

            //Sql DataAdapters for parent and child table, DataSet
            daCustomers = new SqlDataAdapter(queryCustomers, conn);
            daOrders = new SqlDataAdapter(queryOrders, conn);
            dSet = new DataSet();
            daCustomers.Fill(dSet, "Customers");
            daOrders.Fill(dSet, "Orders");

            //fill in insert, update and delete commands
            cmdBuilder = new SqlCommandBuilder(daOrders);

            //DataRelation added to the dset
            dSet.Relations.Add("CutomersOrders1",
                dSet.Tables["Customers"].Columns["CID"],
                dSet.Tables["Orders"].Columns["CID"]);

            this.dataGridView1.DataSource = dSet.Tables["Customers"];
            this.dataGridView2.DataSource = this.dataGridView1.DataSource;
            this.dataGridView2.DataMember = "CutomersOrders1";
            cmdBuilder.GetUpdateCommand();

        }

        string getConnectionString()
        {
            return "Data Source=DESKTOP-VDNALC6\\SQLEXPRESS;" + "Initial Catalog=Restaurant;Integrated Security=true;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            daOrders.Update(dSet, "Orders");
        }

        private void button1_Click(object sender, EventArgs e) // add button
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConnectionString()))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) " +
                                    "VALUES (@FirstName, @LastName, @Email, @Phone, @Address); " +
                                    "SELECT SCOPE_IDENTITY();", conn, transaction);

                    cmd.Parameters.AddWithValue("@FirstName", textBox5.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBox6.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox7.Text);
                    cmd.Parameters.AddWithValue("@Phone", int.Parse(textBox8.Text));
                    cmd.Parameters.AddWithValue("@Address", textBox9.Text);

                    int newCustomerId = Convert.ToInt32(cmd.ExecuteScalar());

                    SqlCommand cmd1 = new SqlCommand("INSERT INTO Orders (CID, OrderDate, TotalAmount, PaymentMethod) " +
                                    "VALUES (@CID, @OrderDate, @TotalAmount, @PaymentMethod);", conn, transaction); 

                    cmd1.Parameters.AddWithValue("@CID", newCustomerId);
                    cmd1.Parameters.AddWithValue("@OrderDate", DateTime.Parse(textBox1.Text));
                    cmd1.Parameters.AddWithValue("@TotalAmount", decimal.Parse(textBox2.Text));
                    cmd1.Parameters.AddWithValue("@PaymentMethod", textBox3.Text);


                    cmd1.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Customer and Order have been added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void refreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                dSet.Tables["Orders"].Clear();
                dSet.Tables["Customers"].Clear();


                daCustomers.Fill(dSet, "Customers");
                daOrders.Fill(dSet, "Orders");


                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = dSet.Tables["Customers"];


                this.dataGridView2.DataSource = null;
                this.dataGridView2.DataSource = this.dataGridView1.DataSource;
                this.dataGridView2.DataMember = "CutomersOrders1";

                MessageBox.Show("Tables refreshed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while refreshing the data: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void updateButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Orders SET OrderDate=@OrderDate, TotalAmount=@TotalAmount, " + 
                        "PaymentMethod=@PaymentMethod WHERE OID=@OID", conn); 

                    cmd.Parameters.AddWithValue("@OID", int.Parse(textBox4.Text));
                    cmd.Parameters.AddWithValue("@OrderDate", DateTime.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@TotalAmount", decimal.Parse(textBox2.Text));
                    cmd.Parameters.AddWithValue("@PaymentMethod", textBox3.Text);

                    cmd.ExecuteNonQuery();
                } 
                MessageBox.Show("Successfully updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConnectionString()))
                {
                    conn.Open();
                    var OID = int.Parse(textBox4.Text);

                    // First, delete related records from OrdersMenuItems
                    SqlCommand cmdRelated = new SqlCommand("DELETE FROM OrdersMenuItems WHERE OID = @OID", conn);
                    cmdRelated.Parameters.AddWithValue("@OID", OID);
                    cmdRelated.ExecuteNonQuery();

                    // Then, delete the Order
                    SqlCommand cmdOrder = new SqlCommand("DELETE FROM Orders WHERE OID = @OID", conn);
                    cmdOrder.Parameters.AddWithValue("@OID", OID);
                    int rowsAffected = cmdOrder.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Order and related menu items deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("The order could not be found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }







        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void addOrderButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(getConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Orders (CID, OrderDate, TotalAmount, PaymentMethod) " +
                                    "VALUES (@CID, @OrderDate, @TotalAmount, @PaymentMethod);", conn);

                    cmd.Parameters.AddWithValue("@CID", int.Parse(textBox10.Text));
                    cmd.Parameters.AddWithValue("@OrderDate", DateTime.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@TotalAmount", decimal.Parse(textBox2.Text));
                    cmd.Parameters.AddWithValue("@PaymentMethod", textBox3.Text);


                    cmd.ExecuteNonQuery();
;
                    MessageBox.Show("Order has been added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }

}

