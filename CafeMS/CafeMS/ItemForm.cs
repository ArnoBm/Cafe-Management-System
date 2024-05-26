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
    public partial class ItemForm : Form
    {
        public ItemForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""E:\Project\Cafe Management System\CafeMS\CafeMS\Cafedb.mdf"";Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            Con.Open();
            string query = "select * from ItemsTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ItemsGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserOrder uOrder = new UserOrder();
            uOrder.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UsersForm item = new UsersForm();
            item.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(ItemNumTb.Text == "" || ItemNameTb.Text == "" || CatTb.Text == "" || ItemPriceTb.Text == "")
            {
                MessageBox.Show("Fill All The Data");
            }
            else
            {
                Con.Open();
                string query = "insert into ItemsTbl values ('" + ItemNumTb.Text + "', '" + ItemNameTb.Text + "', '" + CatTb.Text + "', '" + ItemPriceTb.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Added");
                Con.Close();
                populate();
            }
        }

        private void ItemsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemNumTb.Text = ItemsGV.SelectedRows[0].Cells[0].Value.ToString();
            ItemNameTb.Text = ItemsGV.SelectedRows[0].Cells[1].Value.ToString();
            CatTb.Text = ItemsGV.SelectedRows[0].Cells[2].Value.ToString();
            ItemPriceTb.Text = ItemsGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "")
            {
                MessageBox.Show("Select The Item to be Deleted");
            }
            else
            {
                Con.Open();
                string query = "delete from ItemsTbl where Item_Number = '" + ItemNumTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ItemNumTb.Text == "" || ItemNameTb.Text == "" || CatTb.Text == "" || ItemPriceTb.Text == "")
            {
                MessageBox.Show("Fill All The Fields");
            }
            else
            {
                Con.Open();
                string query = "update ItemsTbl set Item_Name = '" + ItemNameTb.Text + "', Item_Category = '" + CatTb.Text + "' where Item_Price = '" + ItemPriceTb + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated");
                Con.Close();
                populate();
            }
        }
    }
}
