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

namespace CafeMS
{
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\Project\Cafe Management System\CafeMS\CafeMS\Cafedb.mdf"";Integrated Security=True;Connect Timeout=30");
       
        void populate()
        {
            Con.Open();
            string query = "select * from UsersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UsersGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserOrder uOrder = new UserOrder();
            uOrder.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ItemForm item = new ItemForm();
            item.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "insert into UsersTbl values ('" + unameTb.Text + "', '" + uphoneTb.Text + "', '" + upassTb.Text + "')";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("User Successfully Created");
            Con.Close();
            populate();
        }

        private void UsersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Con.Open();
            string query = "select * from UsersTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            UsersGV.DataSource = table;
            Con.Close();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void UsersGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            unameTb.Text = UsersGV.SelectedRows[0].Cells[0x0].Value.ToString();
            uphoneTb.Text = UsersGV.SelectedRows[0].Cells[1].Value.ToString();
            upassTb.Text = UsersGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(uphoneTb.Text == "")
            {
                MessageBox.Show("Select The User to be Deleted");
            }
            else
            {
                Con.Open();
                string query = "delete from UsersTbl where uphone = '" + uphoneTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(uphoneTb.Text == "" || upassTb.Text == "" || unameTb.Text == "")
            {
                MessageBox.Show("Fill All The Fields");
            }
            else
            {
                Con.Open();
                string query = "update UsersTbl set Uname = '" + unameTb.Text + "', Upassword = '" + upassTb.Text + "' where Uphone = '"+ uphoneTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated");
                Con.Close();
                populate();
            }
        }
    }
}
