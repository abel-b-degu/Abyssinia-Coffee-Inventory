using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Abyssinia_Coffee_Inventory
{
    public partial class ManageCustomer : Form
    {
        public ManageCustomer()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DSU\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            try
            {
                Con.Open();
                String Myquery = "select * from CustomerTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomerGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                MessageBox.Show("Customer Successfully Added");
                SqlCommand cmd = new SqlCommand("insert into CustomerTbl values('" + Customerid.Text + "','" + CustomernameTb.Text + "','" + CustomerPhoneTb.Text + "')", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
            }
            catch(Exception Except)
            {
                MessageBox.Show(Except.Message);
            }

        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void CustomerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
                Customerid.Text = CustomerGV.SelectedRows[0].Cells[0].Value.ToString();
                CustomernameTb.Text = CustomerGV.SelectedRows[0].Cells[1].Value.ToString();
                CustomerPhoneTb.Text = CustomerGV.SelectedRows[0].Cells[2].Value.ToString();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CustomerPhoneTb.Text == "")
            {
                MessageBox.Show("Enter The Customer Phone Number");
            }
            else
            {
                Con.Open();
                MessageBox.Show("Customer successfully deleted");
                string myquery = "delete from CustomerTbl where CustPhone= '" + CustomerPhoneTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                
                Con.Close();
                populate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CustomernameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CustomerPhoneTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Customerid_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            Home gohome = new Home();
            gohome.Show();
            this.Hide();
        }
    }
}
