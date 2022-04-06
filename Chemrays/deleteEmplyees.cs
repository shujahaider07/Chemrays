using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Chemrays
{
    public partial class deleteEmplyees : Form
    {
        public deleteEmplyees()
        {
            InitializeComponent();
        }
        String cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        private void deleteEmplyees_Load(object sender, EventArgs e)
        {
            bindGridView(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection sql = new SqlConnection(cs);
            sql.Open();
            String qry = "DELETE FROM sheet WHERE id = '" +textBox1.Text + "'";

            SqlCommand cmd = new SqlCommand(qry, sql);
            cmd.Parameters.AddWithValue("@name", nametxt.Text);
            cmd.Parameters.AddWithValue("@salaries", Salarytxt.Text);
            cmd.Parameters.AddWithValue("@advanceloan", advanceloantxt.Text);
            cmd.Parameters.AddWithValue("@previousbalanceloan", previousbalanceloantxt.Text);
            cmd.Parameters.AddWithValue("@totalloan", totalloantxt.Text);
            cmd.Parameters.AddWithValue("@deduction", deductiontxt.Text);
            cmd.Parameters.AddWithValue("@balanceloancf", balancesalarycftxt.Text);
            cmd.Parameters.AddWithValue("@balancesalary", balancesalarytxt.Text);
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Deleted");
                bindGridView();
            }
            else
            {
                MessageBox.Show("Data DEleted failed");
            }
            sql.Close();
        }
        public void bindGridView()
        {
            SqlConnection sql = new SqlConnection(cs);
            sql.Open();
            String qry = "select * from sheet";
            SqlDataAdapter da = new SqlDataAdapter(qry, sql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sql.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.CurrentRow.Selected = true;
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                nametxt.Text = dataGridView1.Rows[e.RowIndex].Cells["name"].Value.ToString();
                Salarytxt.Text = dataGridView1.Rows[e.RowIndex].Cells["salaries"].Value.ToString();
                advanceloantxt.Text = dataGridView1.Rows[e.RowIndex].Cells["advanceloan"].Value.ToString();
                previousbalanceloantxt.Text = dataGridView1.Rows[e.RowIndex].Cells["previousbalanceloan"].Value.ToString();
                totalloantxt.Text = dataGridView1.Rows[e.RowIndex].Cells["totalloan"].Value.ToString();
                deductiontxt.Text = dataGridView1.Rows[e.RowIndex].Cells["deduction"].Value.ToString();
                balancesalarytxt.Text = dataGridView1.Rows[e.RowIndex].Cells["balancesalary"].Value.ToString();
                balancesalarycftxt.Text = dataGridView1.Rows[e.RowIndex].Cells["balanceloancf"].Value.ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
