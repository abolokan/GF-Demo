﻿using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}
