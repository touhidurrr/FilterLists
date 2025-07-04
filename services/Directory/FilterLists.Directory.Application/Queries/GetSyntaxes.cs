﻿using FilterLists.Directory.Infrastructure.Persistence.Queries.Context;
using Microsoft.EntityFrameworkCore;

namespace FilterLists.Directory.Application.Queries;

public static class GetSyntaxes
{
    public class Query(QueryDbContext ctx)
    {
        public async Task<List<Response>> ExecuteAsync(CancellationToken ct)
        {
            return await ctx.Syntaxes
                .Where(s => s.FilterListSyntaxes.Any())
                .OrderBy(s => s.Id)
                .Select(s => new Response
                (
                    s.Id,
                    s.Name,
                    s.Description,
                    s.Url
                ))
                .TagWith(nameof(GetSyntaxes))
                .ToListAsync(ct);
        }
    }

    /// <param name="Id" example="3">The identifier.</param>
    /// <param name="Name" example="Adblock Plus">The unique name.</param>
    /// <param name="Description" example="null">The description.</param>
    /// <param name="Url" example="https://adblockplus.org/filters">The URL of the home page.</param>
    public sealed record Response(
        short Id,
        string Name,
        string? Description,
        Uri? Url);
}