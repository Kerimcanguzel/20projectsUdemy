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

namespace Project1_AdonetCustomer
{
    public partial class FrmCity : Form
    {
        public FrmCity()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        SqlConnection sqlConnection = new SqlConnection("Server=DESKTOP-0I7E7GA\\SQLEXPRESS;initial catalog=DbCustomer;integrated security=true");
        private void Listele_Click(object sender, EventArgs e)
        { 
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * from TblCity", sqlConnection);
            SqlDataAdapter adapter= new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update TblCity Set CityName=@cityName,CityCountry=@cityCountry Where CityId=@cityId", sqlConnection);
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            command.Parameters.AddWithValue("@cityCountry", txtCityCountry.Text);
            command.Parameters.AddWithValue("@cityId", txtCityId.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();  
            MessageBox.Show("Şehir başarılı bir şekilde güncellendi ", "Uyarı!",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Insert into TblCity (CityName,CityCountry) values (@cityName,@cityCountry)", sqlConnection);
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            command.Parameters.AddWithValue("@cityCountry", txtCityCountry.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Şehir başarılı bir şekilde eklendi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From TblCity Where CityId=@cityId", sqlConnection);
            command.Parameters.AddWithValue("@cityId", txtCityId.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Şehir başarılı bir şekilde silindi","Uyarı!",
                MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select * From TblCity Where CityName=@cityName", sqlConnection);
           command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            sqlConnection.Close();
        }
    }
}
