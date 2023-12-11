namespace Apps.UnbabelProjects.Models.Request.File;

public class UploadFileRequest
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Extension { get; set; }
    
    public UploadFileRequest(UploadFileInput input)
    {
        Name = input.Name;
        Description = input.Description;
        Extension = Path.GetExtension(input.Name).Replace(".", string.Empty);
    }
}