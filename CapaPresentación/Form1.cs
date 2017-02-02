using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntity;
using CapaNegocio;


namespace CapaPresentación
{
    public partial class Form1 : Form
    {
        private Empleado _empleado = new Empleado();
        private EmpleadoBol _empleadoBol = new EmpleadoBol();
        

        public Form1()
        {
            InitializeComponent();
        }

        //METODOS//

        private void InsertData ()
        {
            try
            {
                if (_empleado == null) _empleado = new Empleado();

                if(intID.Text != null)
                _empleado.id = Convert.ToInt32(intID.Text);

                _empleado.nombre = txtNombre.Text;
                _empleado.puesto = txtPuesto.Text;
                _empleado.sueldo = Convert.ToInt32(intSueldo.Text);

                _empleadoBol.Registrar(_empleado);

                if (_empleadoBol.stringBuilder.Length != 0)
                {
                    MessageBox.Show(_empleadoBol.stringBuilder.ToString(), "Para continuar:");
                }

                else
                    MessageBox.Show("Producto registrado/actualizado con éxito");
            }

            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void ShowAll()
        {
            List<Empleado> listaEmpleados = _empleadoBol.Todos();

            if(listaEmpleados.Count > 0)
            {
                dataGridEmpleados.DataSource = listaEmpleados;
            }
            else
            {
                MessageBox.Show("No existen empleados registrados");
            }
        }

        private void ShowByID(int id)
        {
            try
            {
                _empleado = _empleadoBol.ReturnByID(id);

                if(_empleado != null)
                {
                    txtNombre.Text = _empleado.nombre;
                    txtPuesto.Text = _empleado.puesto;
                    intID.Text = Convert.ToString(_empleado.id);
                    intSueldo.Text = Convert.ToString(_empleado.sueldo);
                }
                else MessageBox.Show("El producto solicitado no existe");
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }

        private void Delete (int id)
        {
            try
            {
                _empleadoBol.DeleteByID(id);
                

                if(_empleadoBol.stringBuilder.Length != 0)
                {

                     MessageBox.Show(_empleadoBol.stringBuilder.ToString());

                }
                else
                     MessageBox.Show("Registro eliminado con exito.");
            }

            catch (Exception ex)
            {
                      MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            InsertData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete(Convert.ToInt32(intID.Text));
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            ShowAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
            ShowByID(Convert.ToInt32(intID.Text));
        }

       private void main()
        {
            ShowAll();
        }

    }
}