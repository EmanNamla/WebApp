namespace WebApp.Core.Domain.AggregatesModel.Managment.UsersAggregator.DomainResult
{
    public record LoginUserResultDto(bool Success, string Message, string? Token = null);
}
