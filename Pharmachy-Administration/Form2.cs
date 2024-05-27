using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Pharmachy_Administration
{
    public partial class Form2 : Form
    {
        OracleConnection con = new OracleConnection("Data Source=localhost:1521/xe;User Id=system;Password=admin123;");
        OracleCommand command;
        OracleDataAdapter adapter;
        DataTable dt;

        public Form2()
        {
            InitializeComponent();
        }

        public void InitializeData()
        {
            adapter = new OracleDataAdapter("select * from sales", con);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
