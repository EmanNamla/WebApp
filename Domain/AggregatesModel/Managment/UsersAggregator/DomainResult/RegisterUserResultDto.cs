namespace WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator.DomainResult
{
    public record RegisterUserResultDto(bool Success, string Message, string? UserId = null);
}
