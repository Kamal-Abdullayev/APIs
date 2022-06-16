using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ProjectX.Helpers
{
    public static class DeleteImage
    {
        public static void DeleteImg(IWebHostEnvironment env, string folder, string name)
        {
            string currentPath = env.WebRootPath;

            string imgPath = Path.Combine(currentPath, folder, name);

            if (System.IO.File.Exists(imgPath))
            {
                System.IO.File.Delete(imgPath);
            }
        }
    }
}
