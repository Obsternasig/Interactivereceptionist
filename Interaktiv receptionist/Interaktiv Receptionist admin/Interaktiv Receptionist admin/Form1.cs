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

namespace Interaktiv_Receptionist_admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            SqlConnection con = new SqlConnection("Server = tcp:4ndy.database.windows.net,1433; Initial Catalog = Novi8; Persist Security Info = False; User ID = { Anders }; Password ={ 40%60SUr }; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
=======
            SqlConnection con = new SqlConnection(@"Server = tcp:novi8.database.windows.net,1433; Initial Catalog = IRNovi8; Persist Security Info = False; User ID =Novi8admin; Password =password; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;");
>>>>>>> 03e91f2059ac1f99840e7f7819b731c47a59e81d
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from Login where Username ='" + textBox1.Text + "'and Password ='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1" || dt.Rows[0][0] != null)
            {
                this.Hide();
                Main Adminpage = new Main();
                Adminpage.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password","Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
