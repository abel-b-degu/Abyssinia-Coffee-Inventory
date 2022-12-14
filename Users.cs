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
    public class Users
    {

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DSU\Documents\Inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        private TextBox _uname;
        private TextBox _fname;
        private TextBox _password;
        private TextBox _phone;

        private DataGridView _userGrid;

        public Users(DataGridView userGrid)
        {
            UserGrid = userGrid;
        }

        public Users(TextBox uname, TextBox fname, TextBox password, TextBox phone)
        {
            _uname = uname;
            _fname = fname;
            _password = password;
            _phone = phone;

        }

        public TextBox Uname { get => _uname; set => _uname = value; }
        public TextBox Fname { get => _fname; set => _fname = value; }
        public TextBox Password { get => _password; set => _password = value; }
        public TextBox Phone { get => _phone; set => _phone = value; }

        public DataGridView UserGrid { get => _userGrid; set => _userGrid = value; }

        public void Add()
        {
            try
            {
                Con.Open();
                MessageBox.Show("User Successfully Added");
                SqlCommand cmd = new SqlCommand("insert into UserTbl values('" + Uname.Text + "','" + Fname.Text + "','" + Password.Text + "','" + Phone.Text + "')", Con);
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
                String Myquery = "select * from UserTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UserGrid.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
    }
}
