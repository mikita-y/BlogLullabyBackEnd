using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlogLullaby.WEB_API.Infrastructure
{
    public class FileSavingHelper
    {
        private IHostingEnvironment _appEnvironment;
        private AppConfig _appConfig;
        public FileSavingHelper(IHostingEnvironment appEnvironment, IOptions<AppConfig> appConfig)
        {
            _appEnvironment = appEnvironment;
            _appConfig = appConfig.Value;
        }

        public async Task<string> SaveFormFile(IFormFile savingFile, string relativePath = null)
        {
            if (savingFile == null)
                return null;
            var contentType = savingFile.ContentType;
            string[] words = contentType.Split(new char[] { '/' });
            var extension = words[words.Length - 1];
            var guidFileName = $"{Guid.NewGuid()}.{extension}";
            var folderPath = relativePath == null ? "" : $"{relativePath}/";
            string path = $"{_appEnvironment.WebRootPath}/{folderPath}{guidFileName}";
            // сохраняем файл в папку в каталоге wwwroot
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await savingFile.CopyToAsync(fileStream);
            }
            var fileUrl = _appConfig.Host + '/' + folderPath + guidFileName;
            return fileUrl;
        }
    }
}
