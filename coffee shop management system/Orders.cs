using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace coffee_shop_management_system
{
    public partial class Orders : Form
    {
        public Orders()
        {
            InitializeComponent();
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Orders_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            con.Open();
            SqlCommand cnn = new SqlCommand("select * from ordertb", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            con.Open();

            SqlCommand cnn = new SqlCommand("select * from ordertb", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            try
            {
                con.Open();

                // Update the record where OrderId matches
                SqlCommand updateCommand = new SqlCommand(
                    "UPDATE ordertb SET CoffeeName = @CoffeeName, Type = @Type, Quantity = @Quantity, Payment = @Payment WHERE OrderId = @OrderId", con);

                updateCommand.Parameters.AddWithValue("@OrderId", txtOrderId.Text);
                updateCommand.Parameters.AddWithValue("@CoffeeName", comboBox1.Text);
                updateCommand.Parameters.AddWithValue("@Type", comboBox3.Text);
                updateCommand.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                updateCommand.Parameters.AddWithValue("@Payment", txtPayment.Text);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("OrderId not found. Update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            try
            {
                con.Open();

                // Delete the record where OrderId matches
                SqlCommand deleteCommand = new SqlCommand("DELETE FROM ordertb WHERE OrderId = @OrderId", con);
                deleteCommand.Parameters.AddWithValue("@OrderId", txtOrderId.Text);

                int rowsAffected = deleteCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reset the fields after deletion
                    btnReset_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("OrderId not found. Deletion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtOrderId.Text = "";
            comboBox1.Text = "";
            comboBox3.Text = "";
            txtQuantity.Text = "";
            txtPayment.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            try
            {
                con.Open();

                // Generate the new OrderId
                SqlCommand getMaxIdCommand = new SqlCommand("SELECT ISNULL(MAX(OrderId), 0) + 1 FROM ordertb", con);
                int newOrderId = (int)getMaxIdCommand.ExecuteScalar();

                // Insert the new order with the generated OrderId
                SqlCommand insertCommand = new SqlCommand("INSERT INTO ordertb (OrderId, CoffeeName, Type, Quantity, Payment) VALUES (@OrderId, @CoffeeName, @Type, @Quantity, @Payment)", con);
                insertCommand.Parameters.AddWithValue("@OrderId", newOrderId);
                insertCommand.Parameters.AddWithValue("@CoffeeName", comboBox1.Text);
                insertCommand.Parameters.AddWithValue("@Type", comboBox3.Text);
                insertCommand.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                insertCommand.Parameters.AddWithValue("@Payment", txtPayment.Text);

                insertCommand.ExecuteNonQuery();

                MessageBox.Show($"Record Saved Successfully with OrderId: {newOrderId}", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset fields after saving
                btnReset_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            con.Open();
            SqlCommand cnn = new SqlCommand("select * from ordertb", con);
            SqlDataAdapter da = new SqlDataAdapter(cnn);
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }
    }

}