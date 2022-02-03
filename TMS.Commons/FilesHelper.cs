using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Commons
{
    public static class FilesHelper
    {
        public static async Task<IEnumerable<(string uid, string filename)>> SaveAsync(List<IFormFile> files, string folderPath)
        {
            var result = new List<(string, string)>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileUid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    using (var stream = File.Create(Path.Combine(folderPath, fileUid)))
                    {
                        await file.CopyToAsync(stream);
                    }

                    result.Add((fileUid, file.FileName));
                }
            }

            return result;
        }
    }
}
