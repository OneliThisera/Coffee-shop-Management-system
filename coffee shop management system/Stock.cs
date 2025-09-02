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
    public partial class Stock : Form
    {
        private object cnn;

        public Stock()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        

        private void Stock_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM stocktb", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
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

        private void button5_Click(object sender, EventArgs e)
        {
            // Validate inputs before proceeding
            if (!ValidateInputs()) return;

            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");

            try
            {
                con.Open();

                SqlCommand insertCommand = new SqlCommand(
                    "INSERT INTO stocktb (Ingredients, Quantity, Type) VALUES (@Ingredients, @Quantity, @Type)", con);

                insertCommand.Parameters.AddWithValue("@Ingredients", txtIngredients.Text);
                insertCommand.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                insertCommand.Parameters.AddWithValue("@Type", comboBox1.Text);

                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Record Saved Successfully", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void button1_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (!ValidateInputs()) return;

            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");

            try
            {
                con.Open();

                // Define the SQL command for adding new records
                SqlCommand cmd = new SqlCommand("INSERT INTO stocktb (Ingredients, Quantity, Type) VALUES (@Ingredients, @Quantity, @Type)", con);

                // Add parameters
                cmd.Parameters.AddWithValue("@Ingredients", txtIngredients.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@Type", comboBox1.Text);

                // Execute the query
                cmd.ExecuteNonQuery();

                // Notify the user
                MessageBox.Show("Item Added Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optionally, refresh the DataGridView
                RefreshGrid(con);
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

        // Refresh the DataGridView to reflect new data
        private void RefreshGrid(SqlConnection con)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM stocktb", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (!ValidateInputs()) return;

            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE stocktb SET Quantity = @Quantity, Type = @Type WHERE Ingredients = @Ingredients", con);

                cmd.Parameters.AddWithValue("@Ingredients", txtIngredients.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@Type", comboBox1.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record Updated Successfully", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No Record Found with the Given Ingredients", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Validate Ingredients field
            if (string.IsNullOrWhiteSpace(txtIngredients.Text))
            {
                MessageBox.Show("Ingredients cannot be empty for deletion.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIngredients.Focus();
                return;
            }

            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM stocktb WHERE Ingredients = @Ingredients", con);
                cmd.Parameters.AddWithValue("@Ingredients", txtIngredients.Text);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No Record Found with the Given Ingredients", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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




        private void button4_Click(object sender, EventArgs e)
        {
           
                SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM stocktb", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt; // Assuming you have a DataGridView named 'dataGridView1'
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
        private bool ValidateInputs()
        {
            // Validate Ingredients
            if (string.IsNullOrWhiteSpace(txtIngredients.Text))
            {
                MessageBox.Show("Ingredients cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIngredients.Focus();
                return false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(txtIngredients.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Ingredients can only contain letters and spaces.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIngredients.Focus();
                return false;
            }

            // Validate Quantity
            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Quantity cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }
            else if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Quantity must be a positive integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            // Validate Type (ComboBox)
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Please select a valid Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }

            return true; // All validations passed
        }

    }
}
