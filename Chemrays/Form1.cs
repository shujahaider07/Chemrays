using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace Chemrays
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        int id = 0;
        String cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        private void Form1_Load(object sender, EventArgs e)
        {
            style();
            getnames();




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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(cs);
            sql.Open();

            String qry = "update sheet set name = '" + nametxt.Text + "' , salaries = '" + Salarytxt.Text + "' , advanceloan = '" + advanceloantxt.Text + "' ,previousbalanceloan = '" + previousbalanceloantxt.Text + "' , totalloan = '" + totalloantxt.Text + "' , deduction = '" + deductiontxt.Text + "' , balancesalary = '" + balancesalarytxt.Text + "' , balanceloancf = '" + balancesalarycftxt.Text + "'  where id = '" + textBox1.Text + "'";


            SqlCommand cmd = new SqlCommand(qry, sql);
            cmd.Parameters.AddWithValue("@name", nametxt.Text);
            cmd.Parameters.AddWithValue("@salaries", Salarytxt.Text);
            cmd.Parameters.AddWithValue("@advanceloan", advanceloantxt.Text);
            cmd.Parameters.AddWithValue("@previousbalanceloan", previousbalanceloantxt.Text);
            cmd.Parameters.AddWithValue("@totalloan", totalloantxt.Text);
            cmd.Parameters.AddWithValue("@deduction", deductiontxt.Text);
            cmd.Parameters.AddWithValue("@balancesalary", balancesalarytxt.Text);
            //cmd.Parameters.AddWithValue("@balanceloancf", balancesalarycftxt.Text);
            cmd.Parameters.AddWithValue("@balanceloancf", balancesalarycftxt.Text);


            int a = cmd.ExecuteNonQuery();
            if (a > 0)
            {
                MessageBox.Show("Data Inserted successfully");
                bindGridView();
                clearinsertdata();
            }
            else
            {
                MessageBox.Show("Failed to Insert Data");
            }
            sql.Close();


        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            nametxt.Focus();
            bindGridView();
            getnames();


        }


        public void clearinsertdata()
        {
            nametxt.Text = "";
            Salarytxt.Text = "";
            advanceloantxt.Text = "";
            previousbalanceloantxt.Text = "";
            deductiontxt.Text = "";
            balancesalarytxt.Text = "";
            totalloantxt.Text = "";
            balancesalarycftxt.Text = "";
            textBox2.Text = "";
        }

        public void getsalaries()
        {

            if (nametxt.SelectedItem == null)
            {

            }
            else
            {
                try
                {


                    SqlConnection sql = new SqlConnection(cs);
                    sql.Open();
                    int salary = 0;
                    string qry = "select id, salaries from sheet where name = @name";
                    SqlDataAdapter da = new SqlDataAdapter(qry, sql);
                    da.SelectCommand.Parameters.AddWithValue("@name", nametxt.SelectedItem.ToString());
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        salary = Convert.ToInt32(dt.Rows[0]["salaries"]);
                        id = Convert.ToInt32(dt.Rows[0]["id"]);


                    }

                    Salarytxt.Text = salary.ToString();
                    textBox1.Text = id.ToString();

                }
                catch
                {

                }

            }

        }
        public void getnames()
        {
            nametxt.Items.Clear();
            SqlConnection sql = new SqlConnection(cs);
            sql.Open();
            string qry = "select * from sheet";
            SqlCommand cmd = new SqlCommand(qry, sql);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string names = dr.GetString(1);
                nametxt.Items.Add(names);

            }

            nametxt.Sorted = true;


            sql.Close();
        }



        private void nametxt_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            getsalaries();
            advanceloan();
            PreviousBalanceLoan();


        }

        private void totalloantxt_TextChanged(object sender, EventArgs e)
        {
            try
            {


                int advanceloan = Convert.ToInt32(advanceloantxt.Text);
                int previousloan = Convert.ToInt32(previousbalanceloantxt.Text);


                int subtotal = advanceloan + previousloan;

                totalloantxt.Text = subtotal.ToString();


            }
            catch (Exception ex)
            {

            }

        }


        public void advanceloan()
        {
            try
            {
                if (nametxt.SelectedItem == null)
                {

                }
                else
                {
                    SqlConnection sql = new SqlConnection(cs);
                    int adloan = 0;
                    sql.Open();
                    string qry = "select advanceloan from sheet where name = @name";
                    SqlDataAdapter da = new SqlDataAdapter(qry, sql);
                    da.SelectCommand.Parameters.AddWithValue("@name", nametxt.SelectedItem.ToString());
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        adloan = Convert.ToInt32(dt.Rows[0]["advanceloan"].ToString());

                    }
                    advanceloantxt.Text = adloan.ToString();

                    sql.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void PreviousBalanceLoan()
        {
            try
            {
                if (nametxt.SelectedItem == null)
                {

                }
                else
                {
                    SqlConnection sql = new SqlConnection(cs);
                    int pbloan = 0;
                    sql.Open();
                    string qry = "select previousbalanceloan from sheet where name = @name";
                    SqlDataAdapter da = new SqlDataAdapter(qry, sql);
                    da.SelectCommand.Parameters.AddWithValue("@name", nametxt.SelectedItem.ToString());
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        pbloan = Convert.ToInt32(dt.Rows[0]["previousbalanceloan"].ToString());

                    }
                    previousbalanceloantxt.Text = pbloan.ToString();

                    sql.Close();
                }
            }
            catch (Exception ex)
            {

            }
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
        public void deduction()
        {
            try
            {

                int dudct = Convert.ToInt32(deductiontxt.Text);
                int totallaon = Convert.ToInt32(totalloantxt.Text);
                int salary = Convert.ToInt32(Salarytxt.Text);

                int result = totallaon - dudct;



                textBox2.Text = result.ToString();

                int local = Convert.ToInt32(textBox2.Text);
                salary = salary - local;

                balancesalarytxt.Text = salary.ToString();

            }
            catch (Exception ex)
            {

            }
        }

        private void balancesalarytxt_TextChanged(object sender, EventArgs e)
        {
            deduction();
        }

        public void style()
        {

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.Bisque;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkSlateGray;
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Century Gothic, 9pt", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;


        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {

                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = xcelApp.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
                worksheet = workbook.Sheets[1];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Chemrays Salaries";

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }


                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }

                xcelApp.Columns.AutoFit();
                var saveFileDialoge = new SaveFileDialog();
                saveFileDialoge.FileName = "Salary";
                saveFileDialoge.DefaultExt = ".xlsx";

                if (saveFileDialoge.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                }
                xcelApp.Quit();

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearinsertdata();
        }

        private void balancesalarycftxt_TextChanged(object sender, EventArgs e)
        {
            try
            {

                int dudct = Convert.ToInt32(deductiontxt.Text);
                int totallaon = Convert.ToInt32(totalloantxt.Text);
                int salary = Convert.ToInt32(Salarytxt.Text);

                int result = totallaon - dudct;



                balancesalarycftxt.Text = result.ToString();

                int local = Convert.ToInt32(balancesalarycftxt.Text);
                salary = salary - local;


            }
            catch (Exception ex)
            {

            }



        }

        private void Salarytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;

            }
            else if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void advanceloantxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;

            }
            else if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }



        private void previousbalanceloantxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;

            }
            else if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void totalloantxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;

            }
            else if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void deductiontxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;

            }
            else if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void balancesalarytxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;

            }
            else if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void balancesalarycftxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch) == true)
            {
                e.Handled = false;

            }
            else if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(cs);
            string qry = "SELECT * from sheet WHERE name LIKE '%" + textBox3.Text + "%' ";
            SqlDataAdapter da = new SqlDataAdapter (qry, sql);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void addEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEmployees ad = new AddEmployees();
            ad.ShowDialog();
            
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process.Start("C://Windows//System32/calc.exe");
        }

        private void deleteEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteEmplyees de = new deleteEmplyees();
            de.ShowDialog();
        }

        //public void SumGridView()
        //    {
        //        SqlConnection sql = new SqlConnection(cs);
        //        sql.Open();

        //        String qry = "select sum(balancesalary) from sheet ";
        //        SqlCommand cmd = new SqlCommand(qry,sql);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            label9.Text = (dr[0].ToString());
        //        }
        //        sql.Close();

        //    }

    }
}
