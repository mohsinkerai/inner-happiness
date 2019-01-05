using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class ImageController : BaseController
    {
        public IActionResult Index()
        {
            return View(new UploadImage());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UploadImage model)
        {

            //Need this zipFilePath from UI.
            string zipFilePath = @"C:/Users/'/Desktop/Desktop.zip";

            List<String> filesNotUploaded = new List<string>();

            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        using (var unzippedEntryStream = entry.Open())
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await unzippedEntryStream.CopyToAsync(memoryStream);
                                string image = Convert.ToBase64String(memoryStream.ToArray());
                                string formNumber = Path.GetFileNameWithoutExtension(entry.FullName);

                                UploadImageContext context = HttpContext.RequestServices.GetService(typeof(UploadImageContext)) as UploadImageContext;
                                bool success = context.uploadImageToDB(image, formNumber);

                                if (!success)
                                {
                                    filesNotUploaded.Add(entry.FullName);
                                }
                            }
                        }

                    }
                }

                model.ListFormNumbers.AddRange(filesNotUploaded);
            }
            catch (Exception ex)
            {

            }

            return View(model);
        }
    }
}