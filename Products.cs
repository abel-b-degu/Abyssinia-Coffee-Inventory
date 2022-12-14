using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Abyssinia_Coffee_Inventory
{

    public class Products
    {

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DSU\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        private TextBox _productId;
        private TextBox _productName;
        private TextBox _qty;
        private TextBox _price;
        private TextBox _description;
        private ComboBox _catCombo;

        private DataGridView _productGrid;

        public Products(DataGridView productGrid)
        {
            ProductGrid = productGrid;
        }

        public Products(TextBox productId, TextBox producrName, TextBox qty, TextBox price, TextBox description, ComboBox catCombo)
        {
            _productId = productId;
            _productName = producrName;
            _qty = qty;
            _price = price;
            _description = description;
            _catCombo = catCombo;
        }

        public TextBox ProductId { get => _productId; set => _productId = value; }
        public TextBox ProductName { get => _productName; set => _productName = value; }
        public TextBox Qty { get => _qty; set => _qty = value; }
        public TextBox Price { get => _price; set => _price = value; }
        public TextBox Description { get => _description; set => _description = value; }
        public ComboBox CatCombo { get => _catCombo; set => _catCombo = value; }
        public DataGridView ProductGrid { get => _productGrid; set => _productGrid = value; }

        public void Add()
        {
            try
            {
                Con.Open();
                MessageBox.Show("Product Successfully Added");
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values('" + ProductId.Text + "','" + ProductName.Text + "','" + Qty.Text + "','" + Price.Text + "','" + Description.Text + "','" + CatCombo.SelectedValue.ToString() + "')", Con);
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
                String Myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductGrid.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
    }

    
}
