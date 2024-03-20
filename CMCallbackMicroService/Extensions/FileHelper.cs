using System.Text.RegularExpressions;

namespace AccountManagementMicroService.Extensions
{
    public static class FileHelper
    {

        public static List<IFormFile> Base64ToImage(List<String> base64Files)
        {
            try
            {
                List<IFormFile> formFiles = new List<IFormFile>();

                foreach (String base64_file in base64Files)
                {
                    var spl = base64_file.Split('/')[1];
                    var format = spl.Split(';')[0];
                    var base64_string = base64_file.Replace($"data:image/{format};base64,", String.Empty);

                    var extension = base64_file.Split(';')[0].Split('/')[1];
                    byte[] bytes = Convert.FromBase64String(base64_string);
                    MemoryStream stream = new MemoryStream(bytes);
                    Guid file_name = Guid.NewGuid();
                    IFormFile file = new FormFile(stream, 0, bytes.Length, $"{file_name}.{format}", $"{file_name}.{format}");
                    //file.ContentType = "image/jpeg";
                    formFiles.Add(file);

                }
                return formFiles;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public static List<IFormFile> Base64ToFileConverter(List<String> base64Files)
        {

            List<IFormFile> formFiles = new List<IFormFile>();


            foreach (String base64_file in base64Files)
            {
                var spl = base64_file.Split('/')[1];
                var MMI_TYPE = base64_file.Split(';')[0].Split(":")[1];
                var format = spl.Split(';')[0];
                var base64_string = base64_file.Replace($"data:{MMI_TYPE};base64,", String.Empty);

                var extension = base64_file.Split(';')[0].Split('/')[1];
                byte[] bytes = Convert.FromBase64String(base64_string);
                Guid file_name = Guid.NewGuid();
                //byte[] bytes = Encoding.UTF8.GetBytes(base64_string);

                var formFile = new FormFile(
                    baseStream: new MemoryStream(bytes),
                    baseStreamOffset: 0,
                    length: bytes.Length,
                    name: $"{file_name}.{format}",
                    fileName: $"{file_name}.{format}")
                {
                    Headers = new HeaderDictionary()
                };

                formFile.ContentType = MMI_TYPE;
                //file.ContentType = "image/jpeg";
                formFiles.Add(formFile);

            }
            return formFiles;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
