using Apps.UnbabelProjects.Models.Entities;

namespace Apps.UnbabelProjects.Models.Response.File;

public record ListFilesResponse(List<FileEntity> Files);