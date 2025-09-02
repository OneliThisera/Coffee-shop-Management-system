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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = @"C:\Users\User\source\repos\coffee shop management system\coffee shop management system\Stock_Repo.rpt";
        }
    }
}
