namespace Fiorello.Helper
{
    public class DeleteFileHelper
    {
        public static void DeleteFile(string folder,string fileName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(currentDirectory, "wwwroot", folder, fileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
