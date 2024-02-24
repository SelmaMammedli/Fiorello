namespace Fiorello.Extensions
{
    public static class Extension
    {
        public static bool CheckFile(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool CheckSize(this IFormFile file,int size)
        {
            return file.Length / 1024 > size;
        }
        public static string SaveFile(this IFormFile file,string folderName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var newGuid = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fullPath = Path.Combine(currentDirectory, "wwwroot", folderName, newGuid);

            using FileStream fileStream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(fileStream);
            return newGuid;
        }
    }
}
