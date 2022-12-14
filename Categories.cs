using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Abyssinia_Coffee_Inventory
{
    public class Categories
    {

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DSU\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        private TextBox _catid;
        private TextBox _catname;
        

        private DataGridView _categoryGrid;

        public Categories(DataGridView categoryGrid)
        {
            CategoryGrid = categoryGrid;
        }

        public Categories(TextBox catid, TextBox catname)
        {
            _catid = catid;
            _catname = catname;
            

        }

        public TextBox Catid { get => _catid; set => _catid = value; }
        public TextBox Catname { get => _catname; set => _catname= value; }
      

        public DataGridView CategoryGrid { get => _categoryGrid; set => _categoryGrid = value; }

        public void Add()
        {
            try
            {
                Con.Open();
                MessageBox.Show("User Successfully Added");
                SqlCommand cmd = new SqlCommand("insert into CategoryTbl values('" + Catid.Text + "','" + Catname.Text +"')", Con);
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
                String Myquery = "select * from CategoryTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategoryGrid.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
    }
}
