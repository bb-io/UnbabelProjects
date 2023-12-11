using Apps.UnbabelProjects.Models.Entities;

namespace Apps.UnbabelProjects.Models.Response.Project;

public record ListProjectsResponse(List<ProjectEntity> Projects);