using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiTpv19CatsAndDog
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["TiTpv19CatsAndDog.Properties.Settings.PetsConnectionString"].ConnectionString;
        }
    

    private void Form1_Load(object sender, EventArgs e)
    {
        PopulatePetTypeTable();
    }

    private void PopulatePetTypeTable()
    {
        using (connection = new SqlConnection(connectionString))
        using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PetType", connection))
        {
            DataTable petTypeTable = new DataTable();
            adapter.Fill(petTypeTable);

            Types.DisplayMember = "petTypeName";
            Types.ValueMember = "Id";
            Types.DataSource = petTypeTable;
        }
    }

        private void Types_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePetName();
        }

        private void PopulatePetName()
        {
            string query = "SELECT Pet.Name, PetTypeName FROM Pet INNER JOIN PetType ON Pet.TypeId = PetType.Id WHERE PetType.Id = @TypeId";
            using(connection = new SqlConnection(connectionString))
            using(SqlCommand command = new SqlCommand(query, connection))
            using(SqlDataAdapter Adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@TypeId", Types.SelectedValue);
                DataTable PetNameTable = new DataTable();
                Adapter.Fill(PetNameTable);

                Names.DisplayMember = "Name";
                Names.ValueMember = "Id";
                Names.DataSource = PetNameTable;

            }
        }
    }
}