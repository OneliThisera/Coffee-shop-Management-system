using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace coffee_shop_management_system
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void dashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            Orders or = new Orders();
            or.Show();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            Stock nv = new Stock();
            nv.Show();
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            Billing bi = new Billing();
            bi.Show();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report re = new Report();
            re.Show();

        }

        

       
        

        

       

       
    }
}
