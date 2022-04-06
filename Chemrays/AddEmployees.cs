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
    public partial class AddEmployees : Form
    {
        public AddEmployees()
        {
            InitializeComponent();
        }

        String cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlConnection sql = new SqlConnection(cs);
            sql.Open();
            String qry = "Insert into sheet values (@name , @salaries , @advanceLoan , @previousbalanceloan , @TotalLoan , @deduction , @balanceSalary , @balanceloancf )";
           
             SqlCommand cmd = new SqlCommand(qry,sql);
            cmd.Parameters.AddWithValue("@name",nametxt.Text);
            cmd.Parameters.AddWithValue("@salaries",Salarytxt.Text);
            cmd.Parameters.AddWithValue("@advanceloan", advanceloantxt.Text);
            cmd.Parameters.AddWithValue("@previousbalanceloan", previousbalanceloantxt.Text);
            cmd.Parameters.AddWithValue("@totalloan", totalloantxt.Text);
            cmd.Parameters.AddWithValue("@deduction", deductiontxt.Text);
            cmd.Parameters.AddWithValue("@balanceloancf", balancesalarycftxt.Text);
            cmd.Parameters.AddWithValue("@balancesalary", balancesalarytxt.Text);
            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data inserted");
                bindGridView();
            }
            else
            {
                MessageBox.Show("Data inserted failed");
            }
            sql.Close();
        }

        private void AddEmployees_Load(object sender, EventArgs e)
        {


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

        private void AddEmployees_Activated(object sender, EventArgs e)
        {
            bindGridView();
        }
    }
}
