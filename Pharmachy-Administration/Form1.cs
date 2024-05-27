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
using Oracle.ManagedDataAccess.Client;

namespace Pharmachy_Administration
{
    public partial class Form1 : Form
    {
        OracleConnection con = new OracleConnection("Data Source=localhost:1521/xe;User Id=system;Password=admin123;");
        OracleCommand command;
        OracleDataAdapter adapter;
        DataTable dt;

        public Form1()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            adapter = new OracleDataAdapter("select * from drugs", con);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void drug_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = drug_name.Text;
            string exp_date = drug_exp_date.Value.ToString().Split(' ')[0];
            string description = drug_description.Text;
            string price = drug_price.Text;
            string quantity_b = drug_quantity_b.Text;
            string quantity_p = drug_quantity_p.Text;

            if(name.Length == 0 )
            {
                MessageBox.Show("You should enter a name!");
                return;
            }

            if (quantity_b.Length == 0)
            {
                MessageBox.Show("You should enter the quantity of blisters!");
                return;
            }

            if (quantity_p.Length == 0)
            {
                MessageBox.Show("You should enter the quantity of pills!");
                return;
            }

            if (price.Length == 0)
            {
                MessageBox.Show("You should enter the price!");
                return;
            }

            if (description.Length == 0)
            {
                MessageBox.Show("You should enter a description!");
                return;
            }

            string getLastIdQuery = "select max(id) from drugs";

            con.Open();

            command = new OracleCommand(getLastIdQuery, con);

            object result = command.ExecuteScalar();

            int id = Convert.ToInt32(result) + 1;
 
            string insertQuery = "insert into drugs (id, name, expiration_date, price, description, quantity) values('" + id + "', '" + name + "', TO_DATE ('" + exp_date +"', 'MM-DD-YY'), '" + "', '" + price + "', '"  + description + "', '" + quantity_b + "x" + quantity_p + "')";

            command = new OracleCommand(insertQuery, con);
            command.ExecuteNonQuery();
            con.Close();
            InitializeData();
        }

        string actionIndex = null;
        private void button3_Click(object sender, EventArgs e)
        {
            if(actionIndex == null)
            {
                MessageBox.Show("You should select an id from the table!");
                return;
            }

            string deleteQuery = "delete from drugs where id = '" + actionIndex + "'";

            con.Open();
            command = new OracleCommand(deleteQuery, con);
            command.ExecuteNonQuery();
            con.Close();
            InitializeData();

            actionIndex = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                actionIndex = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
    }
}
