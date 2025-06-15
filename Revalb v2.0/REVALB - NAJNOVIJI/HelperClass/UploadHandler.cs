namespace REVALB.HelperClass
{
    public class UploadHandler
    {
        public static (bool Success, string MessageOrPath) Upload(IFormFile file, string subfolder = "profile")
        {
            try
            {
                List<string> validExtensions = new() { ".jpg", ".png", ".jpeg", ".webp" };
                string extension = Path.GetExtension(file.FileName).ToLower();

                if (!validExtensions.Contains(extension))
                    return (false, $"Extension '{extension}' is not valid. Allowed: {string.Join(", ", validExtensions)}");

                if (file.Length > 5 * 1024 * 1024)
                    return (false, "Maximum file size is 5MB.");

                string fileName = Guid.NewGuid().ToString() + extension;

                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", subfolder);
                Directory.CreateDirectory(uploadPath); 

                string fullPath = Path.Combine(uploadPath, fileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(stream);

                string relativePath = $"/uploads/{subfolder}/{fileName}";
                return (true, relativePath);
            }
            catch (Exception ex)
            {
                return (false, "Exception: " + ex.Message);
            }
        }
    }
}