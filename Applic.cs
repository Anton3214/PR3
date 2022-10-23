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

namespace PR3
{
    public partial class Applic : Form
    {
        DataSet data;
        SqlDataAdapter For;
        SqlCommandBuilder FF;
        //Подключение к SQL Servery
        string connectionString = @"Data Source=PC_203_6\SQLEXPRESS; Initial Catalog=Clients; Integrated Security=True";
        string sql = "SELECT * FROM applicatio";
        public Applic()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                For = new SqlDataAdapter(sql, connection);

                data = new DataSet();
                For.Fill(data);
                dataGridView1.DataSource = data.Tables[0];
            }
        }


        private void Applic_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataRow row = data.Tables[0].NewRow();
            data.Tables[0].Rows.Add(row);
        }

        void openchild(Panel pen, Form emptyF)
        {
            emptyF.TopLevel = false;
            emptyF.FormBorderStyle = FormBorderStyle.None;
            emptyF.Dock = DockStyle.Fill;
            pen.Controls.Add(emptyF);
            emptyF.BringToFront();
            emptyF.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            openchild(panel1, new Applic());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                For = new SqlDataAdapter(sql, connection);
                FF = new SqlCommandBuilder(For);
                For.InsertCommand = new SqlCommand("ADD_applicatio", connection);
                For.InsertCommand.CommandType = CommandType.StoredProcedure;
                For.InsertCommand.Parameters.Add(new SqlParameter("@id_ckient", SqlDbType.Int, 0, "id_ckient"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@data_applicatio", SqlDbType.Date, 0, "data_applicatio"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@data_completion", SqlDbType.Date, 0, "data_completion"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@worker_id", SqlDbType.Int, 0, "worker_id"));
                For.InsertCommand.Parameters.Add(new SqlParameter("@type_repair_id", SqlDbType.Int, 0, "type_repair_id"));
            }
        }
    }
}
    
 
