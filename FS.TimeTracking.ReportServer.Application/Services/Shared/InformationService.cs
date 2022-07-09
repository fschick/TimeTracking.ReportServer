using System.Threading;
using System.Threading.Tasks;
using FS.TimeTracking.ReportServer.Abstractions.DTOs.Shared;
using FS.TimeTracking.ReportServer.Core.Extensions;
using FS.TimeTracking.ReportServer.Core.Interfaces.Application.Services.Shared;

namespace FS.TimeTracking.ReportServer.Application.Services.Shared;

/// <inheritdoc />
public class InformationService : IInformationService
{
    /// <inheritdoc />
    public Task<ProductInformationDto> GetProductInformation(CancellationToken cancellationToken = default)
        => Task.FromResult(new ProductInformationDto
        {
            Name = GetProductName(cancellationToken).GetAwaiter().GetResult(),
            Version = GetProductVersion(cancellationToken).GetAwaiter().GetResult(),
            Copyright = GetProductCopyright(cancellationToken).GetAwaiter().GetResult(),
        });

    /// <inheritdoc />
    public Task<string> GetProductName(CancellationToken cancellationToken = default)
        => Task.FromResult(AssemblyExtensions.GetProgramProduct());

    /// <inheritdoc />
    public Task<string> GetProductVersion(CancellationToken cancellationToken = default)
        => Task.FromResult(AssemblyExtensions.GetProgramProductVersion());

    /// <inheritdoc />
    public Task<string> GetProductCopyright(CancellationToken cancellationToken = default)
        => Task.FromResult(AssemblyExtensions.GetProgramCopyright());
}