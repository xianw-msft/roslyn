﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable enable

using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.Options
{
    internal interface IOption2
#if !CODE_STYLE
        : IOption
#endif
    {
#if CODE_STYLE
        string Feature { get; }
        string Name { get; }
        Type Type { get; }
        object? DefaultValue { get; }
        bool IsPerLanguage { get; }

        ImmutableArray<OptionStorageLocation2> StorageLocations { get; }
#endif
    }
}
