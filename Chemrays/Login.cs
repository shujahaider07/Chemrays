using System;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace Chemrays
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        String cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          SqlConnection sql = new SqlConnection(cs);    
            sql.Open();
            string qry = "select * from login where username = @user and password = @pass";
            SqlCommand cmd = new SqlCommand(qry,sql);
            cmd.Parameters.AddWithValue("@user",textBox1.Text);
            cmd.Parameters.AddWithValue("@pass",textBox2.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                MessageBox.Show("Login Sucessfully! ");

                Form1 frm = new Form1();

                frm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Incorrect password/Username! ");
            }


            sql.Close();


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
