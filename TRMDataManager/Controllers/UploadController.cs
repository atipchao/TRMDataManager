using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TRMDataManager.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class UploadController : ApiController
    {
        // POST: Upload
        [HttpPost]
        public Task<HttpResponseMessage> Upload(string userStmp)
        {
            List<string> savedFilePath = new List<string>();
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string rootPath = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFileStreamProvider(rootPath);
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach (MultipartFileData item in provider.FileData)
                    {
                        try
                        {
                            
                            string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            string OriginalFileName = name;
                            string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                            File.Move(item.LocalFileName, Path.Combine(rootPath, newFileName));

                            Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                            string fileRelativePath = "~/App_Data/" + newFileName;
                            Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                            savedFilePath.Add(fileFullPath.ToString());
                            // Save file info. to the database
                            DataFile dataFile = new DataFile();
                            dataFile.FileName = newFileName;
                            dataFile.FileExtension = Path.GetExtension(name);
                            dataFile.FileOrgName = name;
                            dataFile.FileStatus = "A"; // Newly inserted file - active
                            dataFile.FileURI = fileFullPath.ToString();
                            dataFile.FileScanId = "erwerwedvsfdsfsdf"; // fake scaned id
                            dataFile.FileDateCreate = DateTime.Now;
                            dataFile.FileDateUpdate = DateTime.Now;
                            dataFile.FileUserStmp = RequestContext.Principal.Identity.GetUserId();
                            savedFilePath.Add(dataFile.FileScanId);
                            TRMDbEntities _db = new TRMDbEntities();
                            using (_db)
                            {
                                _db.DataFiles.Add(dataFile);
                                _db.SaveChanges();
                            }


                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                });
            return task;
        }
    }
}
