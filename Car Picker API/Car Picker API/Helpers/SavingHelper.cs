namespace Car_Picker_API.Helpers
{
    public static class SavingHelper
    {
        public static async Task<string> SaveFileToFolder(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", folderName);

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folderName}/{fileName}";
        }
    }
}
