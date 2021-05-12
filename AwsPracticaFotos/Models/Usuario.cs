using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsPracticaFotos.Models
{
    [DynamoDBTable("Usuarios")]
    public class Usuario
    {
        [DynamoDBProperty("idusuario")]
        [DynamoDBHashKey]
        public int IdUsuario { get; set; }
        [DynamoDBProperty("nombre")]
        public string Nombre { get; set; }
        [DynamoDBProperty("descripcion")]
        public string Descripcion { get; set; }
        [DynamoDBProperty("fechaalta")]
        public string FechaAlta { get; set; }
        [DynamoDBProperty("fotos")]
        public List<Foto> Fotos { get; set; }
    }
}
