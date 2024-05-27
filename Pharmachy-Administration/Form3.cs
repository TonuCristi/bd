using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Oracle.ManagedDataAccess.Client;



namespace Pharmachy_Administration
{
    public partial class Form3 : Form
    {
        OracleConnection con = new OracleConnection("Data Source=localhost:1521/xe;User Id=system;Password=admin123;");
        OracleCommand command;
        OracleDataAdapter adapter;
        DataTable dt;

        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox4.Text;
            string email = textBox5.Text;
            string phone_number = textBox6.Text;
            string password = textBox3.Text;

            if (username.Length == 0)
            {
                MessageBox.Show("You should enter a name!");
                return;
            }

            if (email.Length == 0)
            {
                MessageBox.Show("You should enter an email!");
                return;
            }

            if (phone_number.Length == 0)
            {
                MessageBox.Show("You should enter a phone number!");
                return;
            }

            if (password.Length == 0)
            {
                MessageBox.Show("You should enter a password!");
                return;
            }

            string getMaxIdQuery = "select max(id) from users";

            con.Open();

            command = new OracleCommand(getMaxIdQuery, con);

            object result = command.ExecuteScalar();

            int id = Convert.ToInt32(result) + 1;

            string insertQuery = "insert into users (id, name, password, email, phone_number) values('" + id + "', '" + username + "', '" + password + "', '" + email + "', '" + phone_number + "')";

            command = new OracleCommand(insertQuery, con);
            command.ExecuteNonQuery();
            con.Close();

            Form1 form = new Form1();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (username.Length == 0)
            {
                MessageBox.Show("You should enter a name!");
                return;
            }

            if (password.Length == 0)
            {
                MessageBox.Show("You should enter a password!");
                return;
            }

            string query = "SELECT COUNT(*) FROM users WHERE name = '" + username + "' AND password = '" + password + "'";

            con.Open();

            command = new OracleCommand(query, con);

            int count = Convert.ToInt32(command.ExecuteScalar());

            command.ExecuteNonQuery();
            con.Close();

            if (count > 0)
            {
                Form1 form = new Form1();
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog();

                this.Close();
            }
        }
    }
}
