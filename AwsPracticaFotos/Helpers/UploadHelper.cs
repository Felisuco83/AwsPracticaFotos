﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AwsPracticaFotos.Helpers
{
	public class UploadHelper
	{
		PathProvider pathprovider;

		public UploadHelper(PathProvider pathprovider)
		{
			this.pathprovider = pathprovider;
		}

		public async Task<String> UploadFileAsync(IFormFile formFile)
		{
			String fileName = formFile.FileName;
			String path = this.pathprovider.MapPath(fileName);
			using (var stream = new FileStream(path, FileMode.Create))
			{
				await formFile.CopyToAsync(stream);
			};
			return path;
		}
	}
}