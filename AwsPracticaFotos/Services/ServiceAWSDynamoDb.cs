using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AwsPracticaFotos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsPracticaFotos.Services
{
    public class ServiceAWSDynamoDb
    {
        private DynamoDBContext context;

        public ServiceAWSDynamoDb()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            this.context = new DynamoDBContext(client);
        }

        public async Task CreateUsuario (Usuario user)
        {
            await this.context.SaveAsync<Usuario>(user);
        }

        public async Task UpdateUsuario(Usuario usr)
        {
            Usuario user =  await this.context.LoadAsync<Usuario>(usr.IdUsuario);
            user.Descripcion = usr.Descripcion;
            user.FechaAlta = usr.FechaAlta;
            if(usr.Fotos != null)
            {
                user.Fotos = usr.Fotos;
            }
            user.Nombre = usr.Nombre;
            await this.context.SaveAsync(user);
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            //primero recuperamos la tabla que será sencillo al haber mapeado el modelo
            var tabla = this.context.GetTargetTable<Usuario>();
            var scanOptions = new ScanOperationConfig();
            //scanOptions.PaginationToken = "";//DA ERROR
            var results = tabla.Scan(scanOptions);
            //LOS DATOS RECUPERADOS SON UNA COLECCION DE DOCUMENT
            List<Document> data = await results.GetNextSetAsync();
            //DEBEMOS TRANSFORMAR DICHOS DATOS A SU TIPADO, SERÁ AUTOMÁTICO MEDIANTE UN MÉTODO
            IEnumerable<Usuario> users = this.context.FromDocuments<Usuario>(data);
            return users.ToList();
        }

        public async Task<Usuario> FindUsuario(int id)
        {
            //SI ESTAMOS BUSCANDO POR PARTITION KEY (PK) HASHKEY SOLAMENTE DEBEMOS HACERLO CON LOAD
            // ESTO ES EQUIVALENTE A BUSCAR CON CONSULTA
            return await this.context.LoadAsync<Usuario>(id);
        }

        public async Task DeleteUsuario (int id)
        {
            await this.context.DeleteAsync<Usuario>(id);
        }
    }
}
