using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Helper
{
    public static class FileUploader
    {

        public static string UploadFile(string folderName, IFormFile file)
        {
            try
            {
                //1- get directory
                var folderPath = Directory.GetCurrentDirectory() + Path.Combine(@"\wwwroot\Docs\", folderName);

                //2- get file name
                var FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);

                //3- contact directory and file name
                var FinalPath = Path.Combine(folderPath, FileName); //compine to solve if a proplem in the path 

                //4- save
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    file.CopyTo(Stream);
                } //we use (using) as fileStreem (stream) is disposable


                return FileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static void RemoveFile(string folderName, string fileName)
        {
            var path = Directory.GetCurrentDirectory() + Path.Combine(@"\wwwroot\Docs\", folderName , fileName);

            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}
