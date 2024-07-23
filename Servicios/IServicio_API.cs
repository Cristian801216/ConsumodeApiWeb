using ConsumodeApi.Models;
using System.ComponentModel;

namespace ConsumodeApi.Servicios
{
    public interface IServicio_API
    {
        Task<List<Fincas>> Lista();

        Task<Fincas> Obtener(int idFinca);

        Task<bool> Guardar(Fincas objeto);

        Task<bool> Editar(Fincas objeto);

        Task<bool> Eliminar(int idFinca);



    }
}
