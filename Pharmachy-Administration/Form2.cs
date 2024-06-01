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
        int user_id;
        string user_name = "";
        string[] id_arr = {};

        public Form2(int id, string username)
        {
            InitializeComponent();
            user_id = id;
            user_name = username;
            label2.Text = username;
            InitializeData();
        }

        public void InitializeData()
        {
            adapter = new OracleDataAdapter("select * from sales", con);
            DataTable sales = new DataTable();
            adapter.Fill(sales);
            DataTable drug = new DataTable();

            foreach (DataRow sale in sales.Rows)
            {
                adapter = new OracleDataAdapter("select * from drugs where id = '" + sale[2] + "'", con);
                adapter.Fill(drug);
            }

            dataGridView1.DataSource = drug;
        }

        string actionIndex = null;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                actionIndex = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        /*private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }*/

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(actionIndex);

            if (actionIndex == null)
            {
                MessageBox.Show("You should select an id from the table!");
                return;
            }

            string deleteQuery = "delete from sales where drug_id = '" + actionIndex + "'";

            con.Open();
            command = new OracleCommand(deleteQuery, con);
            command.ExecuteNonQuery();
            con.Close();
            InitializeData();

            actionIndex = null;
        }
    }
}
