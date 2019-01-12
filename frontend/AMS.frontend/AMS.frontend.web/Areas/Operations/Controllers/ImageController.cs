using AMS.frontend.web.Areas.Operations.Models;
using AMS.frontend.web.Areas.Operations.Models.Persons;
using AMS.frontend.web.Helpers.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace AMS.frontend.web.Areas.Operations.Controllers
{
    [Area(AreaNames.Operations)]
    public class ImageController : BaseController
    {
        public IActionResult Index()
        {
            ViewBag.MessageType = TempData["MessageType"];
            ViewBag.Message = TempData["Message"];

            return View(new UploadImage());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UploadImage model)
        {
            List<string> filesNotUploaded = new List<string>();

            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    model.ImageUpload.CopyTo(stream);

                    using (ZipArchive archive = new ZipArchive(stream))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            using (Stream unzippedEntryStream = entry.Open())
                            {
                                using (MemoryStream memoryStream = new MemoryStream())
                                {
                                    await unzippedEntryStream.CopyToAsync(memoryStream);
                                    string image = Convert.ToBase64String(memoryStream.ToArray());
                                    string formNumber = Path.GetFileNameWithoutExtension(entry.FullName);

                                    UploadImageContext context =
                                        HttpContext.RequestServices.GetService(typeof(UploadImageContext)) as
                                            UploadImageContext;
                                    bool success = context.uploadImageToDB(image, formNumber);

                                    if (!success)
                                    {
                                        filesNotUploaded.Add(entry.FullName);
                                    }
                                }
                            }

                        }
                    }
                }

                model.ListFormNumbers.AddRange(filesNotUploaded);
            }
            catch (Exception)
            {
                //TempData["MessageType"] = MessageTypes.Error;
                //TempData["Message"] = Messages.GeneralError;

                ViewBag.MessageType = MessageTypes.Error;
                ViewBag.Message = Messages.GeneralError;
            }

            if (filesNotUploaded.Count == 0)
            {
                //TempData["MessageType"] = MessageTypes.Success;
                //TempData["Message"] = Messages.AllImagesUploaded;

                ViewBag.MessageType = MessageTypes.Success;
                ViewBag.Message = Messages.AllImagesUploaded;
            }
            else
            {
                //TempData["MessageType"] = MessageTypes.Warn;
                //TempData["Message"] = Messages.FewImagesUploaded;

                ViewBag.MessageType = MessageTypes.Warn;
                ViewBag.Message = Messages.FewImagesUploaded;
            }

            return View(model);
        }
    }
}