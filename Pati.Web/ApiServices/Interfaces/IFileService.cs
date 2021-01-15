using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pati.Web.ApiServices.Interfaces
{
    public interface IFileService
    {
        Task<string> GenerateFileName(string prefix);
        Task<List<string>> UploadFile(List<IFormFile> files);
        Task<byte[]> ConvertToByte(IFormFile formFile);
    }
}
