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


namespace Lab2
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        DataSet ds;
        SqlDataAdapter daCountries, daClients;
        SqlCommandBuilder cbClients;
        BindingSource bsCountries, bsClients;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-MGSMSKT\SQLEXPRESS; Initial Catalog=ArtGalleryDB; "
+ "Integrated Security=SSPI;");
            ds = new DataSet();
            daCountries = new SqlDataAdapter("SELECT * FROM Country", conn);
            daClients = new SqlDataAdapter("SELECT * FROM Client", conn);
            cbClients = new SqlCommandBuilder(daClients);
            daCountries.Fill(ds, "Country");
            daClients.Fill(ds, "Client");

            DataRelation dr=new DataRelation("FK_Client_Countries",ds.Tables["Country"].Columns["CountryID"],
                ds.Tables["Client"].Columns["CountryID"]);

            ds.Relations.Add(dr);

            bsCountries = new BindingSource();
            bsCountries.DataSource = ds;
            bsCountries.DataMember = "Country";

            bsClients= new BindingSource();
            bsClients.DataSource = bsCountries;
            bsClients.DataMember = "FK_Client_Countries";

            dgCountries.DataSource = bsCountries;
            dgClients.DataSource = bsClients;
        }

        private void btn_SaveChanges_Click(object sender, EventArgs e)
        {
            daClients.Update(ds, "Client");
        }



        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
