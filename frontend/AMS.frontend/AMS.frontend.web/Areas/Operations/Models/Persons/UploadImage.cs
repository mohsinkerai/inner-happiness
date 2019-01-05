using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Models.Persons
{
    public class UploadImage
    {
        public List<string> ListFormNumbers { get; set; }

        public IFormFile ImageUpload { get; set; }

        public UploadImage()
        {
            ListFormNumbers = new List<string>();
        }

    }

}
