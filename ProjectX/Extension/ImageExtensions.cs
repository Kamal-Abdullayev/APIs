using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectX.Extension
{
    public static class ImageExtension
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool ValidSize(this IFormFile file, int size)
        {
            return file.Length > size * 1024 * 1024;
        }

        public static async Task<string> SaveImage(this IFormFile file, IWebHostEnvironment env, string folder, string name=null)
        {
            string currentPath = env.WebRootPath;
            string imageFolder = folder;
            string imageName = Guid.NewGuid().ToString() + name; ;
            

            string imagePath = Path.Combine(currentPath, imageFolder, imageName);

            using (FileStream fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return imageName;
        }
    }
}
