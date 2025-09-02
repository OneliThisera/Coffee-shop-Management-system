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
    public partial class Billing : Form
    {
        private int BillNo = 1; // Start Bill No
       

        public Billing()
        {
            InitializeComponent();
            AutoGenerateBillNo();
            printDocument1.PrintPage += printDocument1_PrintPage;
        }


        private void Billing_Load(object sender, EventArgs e)
        {
            AutoGenerateBillNo();
            LoadBillingData();
        }

        // Auto-generate Bill Number
        private void AutoGenerateBillNo()
        {
            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(BillNo), 0) FROM billingtb", con);
                object result = cmd.ExecuteScalar();
                BillNo = Convert.ToInt32(result) + 1; // Start with the next bill number
                txtBillNo.Text = $"{BillNo:D4}";
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


        // Load billing data into DataGridView
        private void LoadBillingData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM billingtb", con);
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

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtPrice.Text, out decimal price) && int.TryParse(txtQuantity.Text, out int quantity))
            {
                txtTotal.Text = (price * quantity).ToString("");
            }
            else
            {
                MessageBox.Show("Please enter valid Price and Quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCalculateBalance_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtCash.Text, out decimal cash) && decimal.TryParse(txtTotal.Text, out decimal total))
            {
                if (cash >= total)
                {
                    txtBalance.Text = (cash - total).ToString();
                }
                else
                {
                    MessageBox.Show("Insufficient cash provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid Cash.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
                if (ValidateFields())
                {
                    SqlConnection con = new SqlConnection(@"Data Source=THISERA;Initial Catalog=coffeeshopdb;Integrated Security=True");

                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO billingtb (BillNo, Price, Quantity, Total, Cash, Balance) VALUES (@BillNo, @Price, @Quantity, @Total, @Cash, @Balance)", con);
                        cmd.Parameters.AddWithValue("@BillNo", txtBillNo.Text);
                        cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                        cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                        cmd.Parameters.AddWithValue("@Total", txtTotal.Text);
                        cmd.Parameters.AddWithValue("@Cash", txtCash.Text);
                        cmd.Parameters.AddWithValue("@Balance", txtBalance.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Billing record saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AutoGenerateBillNo(); // Regenerate the next bill number
                        LoadBillingData();
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
            

        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtTotal.Text) ||
                string.IsNullOrWhiteSpace(txtCash.Text) ||
                string.IsNullOrWhiteSpace(txtBalance.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
                {
                    Document = printDocument1,
                    Width = 800,
                    Height = 600
                };
                printPreviewDialog.ShowDialog();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
                Graphics graphics = e.Graphics;
                Font font = new Font("Arial", 12);
                float lineHeight = font.GetHeight() + 2;
                float x = 50;
                float y = 50;

                graphics.DrawString("COFFEE SHOP ", new Font("Arial", 18, FontStyle.Bold), Brushes.Black, x, y);
                y += lineHeight * 2;

                graphics.DrawString($"Bill No: {txtBillNo.Text}", font, Brushes.Black, x, y);
                y += lineHeight;

                graphics.DrawString($"Price: {txtPrice.Text}", font, Brushes.Black, x, y);
                y += lineHeight;

                graphics.DrawString($"Quantity: {txtQuantity.Text}", font, Brushes.Black, x, y);
                y += lineHeight;

                graphics.DrawString($"Total: {txtTotal.Text}", font, Brushes.Black, x, y);
                y += lineHeight;

                graphics.DrawString($"Cash: {txtCash.Text}", font, Brushes.Black, x, y);
                y += lineHeight;

                graphics.DrawString($"Balance: {txtBalance.Text}", font, Brushes.Black, x, y);
                y += lineHeight;

                graphics.DrawString("Thank you for your purchase!", font, Brushes.Black, x, y + 20);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPrice.Clear();
            txtQuantity.Clear();
            txtTotal.Clear();
            txtCash.Clear();
            txtBalance.Clear();

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtBillNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
