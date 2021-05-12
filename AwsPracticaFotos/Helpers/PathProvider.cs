using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AwsPracticaFotos.Helpers
{

	public class PathProvider
	{
		IWebHostEnvironment environment;
		public PathProvider(IWebHostEnvironment environment)
		{
			this.environment = environment;
		}

		public String MapPath(String fileName)
		{
			String carpeta = "images";
			String path = Path.Combine(this.environment.WebRootPath
			, carpeta, fileName);
			return path;
		}
	}
}
