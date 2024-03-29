﻿using FS.TimeTracking.ReportServer.Abstractions.DTOs.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace FS.TimeTracking.ReportServer.Core.Interfaces.Application.Services.Shared;

/// <summary>
/// Information services.
/// </summary>
public interface IInformationService
{
    /// <summary>
    /// Gets the name, version and copyright of the product.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    Task<ProductInformationDto> GetProductInformation(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the name of the product.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    Task<string> GetProductName(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the product version.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    Task<string> GetProductVersion(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the copyright for the product.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    Task<string> GetProductCopyright(CancellationToken cancellationToken = default);
}