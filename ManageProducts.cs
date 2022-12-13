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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DSU\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void fillcategory()
        {
            string query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();  
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }
        void fillSearchCombo()
        {
            string query = "select * from CategoryTbl where CatName='"+SearchCombo.SelectedValue.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                Con.Close();
            }
            catch
            {

            }
        }
        private void ManageProducts_Load(object sender, EventArgs e)
        {
            fillcategory();
            populate();

        }
        void populate()
        {
            try
            {
                Con.Open();
                String Myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        void filterbycategory()
        {
            try
            {
                Con.Open();
                String Myquery = "select * from ProductTbl where ProdCat='"+SearchCombo.SelectedValue.ToString()+"'";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                Con.Open();
                MessageBox.Show("Product Successfully Added");
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values('" + ProductidTb.Text + "','" + ProductNameTb.Text + "','" + QtyTb.Text + "','" + PriceTb.Text + "','" + DescriptionTb.Text + "','" + CatCombo.SelectedValue.ToString() + "')", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
            }
            catch (Exception Except)
            {
                MessageBox.Show(Except.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ProductidTb.Text == "")
            {
                MessageBox.Show("Enter The Product Id");
            }
            else
            {
                MessageBox.Show("Product successfully Deleted");
                Con.Open();
                string myquery = "delete from ProductTbl where Prodid= '" + ProductidTb.Text + "';";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                
                Con.Close();
                populate();
            }
        }

        private void ProductGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductidTb.Text = ProductGV.SelectedRows[0].Cells[0].Value.ToString();
            ProductNameTb.Text = ProductGV.SelectedRows[0].Cells[1].Value.ToString();
            QtyTb.Text = ProductGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ProductGV.SelectedRows[0].Cells[3].Value.ToString();
            DescriptionTb.Text = ProductGV.SelectedRows[0].Cells[4].Value.ToString();
            CatCombo.SelectedValue = ProductGV.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void SearchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
