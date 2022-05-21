using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EstudianteForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            leerData();
        }

        String conString = "datasource=localhost;port=3306;username=root;";
        int id;

        private void leerData()
        {
            string consulta = "select * from escuela.estudiante;";
            MySqlConnection con = new MySqlConnection(conString);
            MySqlCommand command = new MySqlCommand(consulta, con);
            try
            {
                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                tableEstudiante.DataSource = dt;
                con.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("Error al leer data " + ex.Message);
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            txtGrado.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            int edad = int.Parse(txtEdad.Text);
            int grado = int.Parse(txtGrado.Text);

            string insert = "insert into escuela.estudiante(nombre, apellido, edad, grado) " +
                "VALUES ('" + nombre + "','" + apellido + "'," + edad + "," + grado + ")";

            try
            {
                MySqlConnection connection = new MySqlConnection(conString);
                MySqlCommand command = new MySqlCommand(insert, connection);
                connection.Open();

                command.ExecuteNonQuery();
                limpiar();
                MessageBox.Show("Guardado");

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
            leerData();
        }

        private void tableEstudiante_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = tableEstudiante.SelectedRows[0];
            id = int.Parse(row.Cells[0].Value.ToString());
            txtNombre.Text = row.Cells[1].Value.ToString();
            txtApellido.Text = row.Cells[2].Value.ToString();
            txtEdad.Text = row.Cells[3].Value.ToString();
            txtGrado.Text = row.Cells[4].Value.ToString();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            int edad = int.Parse(txtEdad.Text);
            int grado = int.Parse(txtGrado.Text);

            string update = "update escuela.estudiante " +
                "set nombre = '" + nombre + "'," +
                "apellido = '" + apellido + "'," +
                "grado = " + grado + "," +
                "edad = " + edad + " " +
                "where id = " + id + ";";

            try
            {
                MySqlConnection connection = new MySqlConnection(conString);
                MySqlCommand command = new MySqlCommand(update, connection);
                connection.Open();

                command.ExecuteNonQuery();
                limpiar();
                leerData();
                MessageBox.Show("Modificado");

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Modificar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string delete = "delete from escuela.estudiante " +
                    " where id = " + id + ";";

            try
            {
                MySqlConnection connection = new MySqlConnection(conString);
                MySqlCommand command = new MySqlCommand(delete, connection);
                connection.Open();

                command.ExecuteNonQuery();
                limpiar();
                leerData();
                MessageBox.Show("Eliminado");

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Eliminar: " + ex.Message);
            }
        }
    }
}
