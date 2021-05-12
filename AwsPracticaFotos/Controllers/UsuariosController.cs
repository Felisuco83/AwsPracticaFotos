using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AwsPracticaFotos.Helpers;
using AwsPracticaFotos.Services;
using AwsPracticaFotos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AwsPracticaFotos.Controllers
{
    public class UsuariosController : Controller
    {
        ServiceAWSDynamoDb ServiceDynamo;
        private UploadHelper uploadhelper;
        public ServiceAWSS3 ServiceS3;

        public UsuariosController (ServiceAWSDynamoDb service, UploadHelper uploadhelper, ServiceAWSS3 services3)
        {
            this.ServiceDynamo = service;
            this.uploadhelper = uploadhelper;
            this.ServiceS3 = services3;
        }

        public async  Task<IActionResult> Index()
        {
            return View(await this.ServiceDynamo.GetUsuarios());
        }

        public async Task<IActionResult> Details (int id)
        {
            return View(await this.ServiceDynamo.FindUsuario(id));
        }

        public async Task<IActionResult> Delete (int id)
        {
            await this.ServiceDynamo.DeleteUsuario(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (Usuario user, string titulo1, IFormFile file1, string titulo2, IFormFile file2)
        {
            
            user.Fotos = new List<Foto>();
            if (file1 != null) { 
                Foto foto = new Foto();
                foto.Titulo = titulo1;
                foto.Imagen = file1.FileName;
                user.Fotos.Add(foto);
                String path = await this.uploadhelper.UploadFileAsync(file1);
                //SUBIMOS EL FICHERO LOCAL A AWS
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    bool respuesta = await this.ServiceS3.UploadFileAsync(stream, file1.FileName);
                    ViewData["MENSAJE"] = "Archivo en AWS Bucket: " + respuesta;
                };
            }

            if (file2 != null)
            {
                Foto foto = new Foto();
                foto.Titulo = titulo1;
                foto.Imagen = file2.FileName;
                user.Fotos.Add(foto);
                String path = await this.uploadhelper.UploadFileAsync(file2);
                //SUBIMOS EL FICHERO LOCAL A AWS
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    bool respuesta =
                    await this.ServiceS3.UploadFileAsync(stream, file2.FileName);
                    ViewData["MENSAJE"] = "Archivo en AWS Bucket: " + respuesta;
                };
            }
            await this.ServiceDynamo.CreateUsuario(user);
            return RedirectToAction("Index");
        }
    }
}
