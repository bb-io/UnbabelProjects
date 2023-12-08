using Apps.UnbabelProjects.Models.Entities;

namespace Apps.UnbabelProjects.Models.Response.Order;

public record ListOrdersResponse(List<OrderEntity> Orders);