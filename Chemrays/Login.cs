using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace Chemrays
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox1.KeyUp += TextBox1_KeyUp;
            textBox2.KeyUp += TextBox2_KeyUp;

        }

        private void TextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox3.Focus();
            }
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
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
            SqlCommand cmd = new SqlCommand(qry, sql);
            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
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
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Insert Password");

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            {
                SqlConnection sql = new SqlConnection(cs);
                sql.Open();
                string qry = "select * from login where username = @user and password = @pass";
                SqlCommand cmd = new SqlCommand(qry, sql);
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Fill the Box First");
                }
                else
                {
                    if (dr.HasRows)
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
                }

                sql.Close();


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Insert Username");

            }
        }



    }
}


