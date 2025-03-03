﻿using System;
using System.Collections.Generic;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// Container Boundary
    /// </summary>
    public sealed record ContainerBoundary(string Alias, string Label) : Structure(Alias, Label)
    {
        public IEnumerable<Component> Components { get; init; }
        public IEnumerable<Relationship> Relationships { get; init; } = Array.Empty<Relationship>();
    }
}