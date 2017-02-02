using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntity;

namespace CapaNegocio
{
    public class EmpleadoBol
    {
        Connection _Connection = new Connection();       //Se inicializa instancia de objecto Connection

        //Se inicialia variable publica de tipo stringBuilder, la cual sera util para mostrar mensajes a usuario en CapaPresentación
        public readonly StringBuilder stringBuilder = new StringBuilder();  

        //METODOS//
        public void Registrar(Empleado empleado)
        {
            if (ValidarEmpleado(empleado) == true)
            {
                if (_Connection.GetByID(empleado.id) == null)
                {
                    _Connection.Insert(empleado);
                }

                else
                    _Connection.Update(empleado);
            }
        }      // Verifica datos ingresados mediante funcion ValidarEmpleado y llama a Insert o Update segun corresponda

        public List<Empleado> Todos()
        {
            return _Connection.GetAll();
        }                 // Devuelve tabla entera, llamando a funcion GetAll en CapaDatos

        public Empleado ReturnByID(int idEmpleado)
        {
            stringBuilder.Clear();

            if (idEmpleado == 0)
            {
                stringBuilder.Append("El numero de ID es invalido, vuelva a intentar");
            }

            if(stringBuilder.Length == 0)
            {
              return _Connection.GetByID(idEmpleado);
            }

            return null;
        }    // Verifica el ID y ejecuta GetByID de ser correcto

        public void DeleteByID(int idEmpleado)           // Verifica el ID y ejecuta Delete de ser correcto.
        {
            stringBuilder.Clear();

            if (idEmpleado == 0)
            {
                stringBuilder.Append("El numero de ID es invalido, vuelva a intentar");
            }

            if (stringBuilder.Length == 0)
            {
                _Connection.Delete(idEmpleado);
            }

        }

        private bool ValidarEmpleado(Empleado empleado)  // Verifica datos ingresados por Usuario, de ser correctos devuelve Boolean True 
        {
            stringBuilder.Clear();

            if (string.IsNullOrEmpty(empleado.nombre)) stringBuilder.Append("El campo 'Nombre' es obligatorio");
            if (string.IsNullOrEmpty(empleado.puesto)) stringBuilder.Append(Environment.NewLine + "El campo 'Puesto' es obligatorio");
            if (empleado.sueldo <= 0) stringBuilder.Append(Environment.NewLine + "El campo 'Sueldo' es obligatorio");

            return stringBuilder.Length == 0;
        }

    }
}
