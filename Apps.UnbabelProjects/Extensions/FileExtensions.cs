namespace Apps.UnbabelProjects.Extensions;

public static class FileExtensions
{
    public static async Task<byte[]> ReadFromMultipartFormData(this byte[] file, string contentType)
    {
        var stream = new MemoryStream(file);
        var provider = await ReadFormDataAsync(stream, contentType);

        return await provider.Contents.First().ReadAsByteArrayAsync();
    }
    
    static Task<MultipartMemoryStreamProvider> ReadFormDataAsync(Stream stream, string contentType)
    {
        var provider = new MultipartMemoryStreamProvider();
        var content = new StreamContent(stream);
        content.Headers.Add("Content-Type", contentType);
        
        return content.ReadAsMultipartAsync(provider);
    }
}