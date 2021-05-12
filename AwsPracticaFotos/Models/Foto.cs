using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsPracticaFotos.Models
{
    //[DynamoDBTable("")]
    public class Foto
    {
        [DynamoDBProperty("titulo")]
        public string Titulo { get; set; }
        [DynamoDBProperty("imagen")]
        public string Imagen { get; set; }
    }
}
