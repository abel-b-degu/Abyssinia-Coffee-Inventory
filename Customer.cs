using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Abyssinia_Coffee_Inventory
{
    public class Customers
    {

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DSU\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        private TextBox _customerId;
        private TextBox _customerName;
        private TextBox _customerPhone;
       

        private DataGridView _customerGrid;

        public Customers(DataGridView customerGrid)
        {
            CustomerGrid = customerGrid;
        }

        public Customers(TextBox customerId, TextBox customerName, TextBox customerPhone)
        {
            _customerId = customerId;
            _customerName = customerName;
            _customerPhone = customerPhone;
            
        }

        public TextBox CustomerId { get => _customerId; set => _customerId = value; }
        public TextBox CustomerName { get => _customerName; set => _customerName = value; }
        public TextBox CustomerPhone { get => _customerPhone; set => _customerPhone = value; }

        public DataGridView CustomerGrid { get => _customerGrid; set => _customerGrid = value; }

        public void Add()
        {
            try
            {
                Con.Open();
                MessageBox.Show("Product Successfully Added");
                SqlCommand cmd = new SqlCommand("insert into CustomerTbl values('" + CustomerId.Text + "','" + CustomerName.Text + "','" + CustomerPhone.Text +"')", Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
            }
            catch (Exception Except)
            {
                MessageBox.Show(Except.Message);
            }
        }
        public void populate()
        {
            try
            {
                Con.Open();
                String Myquery = "select * from CustomerTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomerGrid.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
      
    }
}
