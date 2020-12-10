// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.Quantum.Simulation.Core;

namespace Microsoft.Quantum.Intrinsic.Interfaces
{
    public interface IGate_ApplyUncontrolledRy : IOperationFactory
    {
        void ApplyUncontrolledRy_Body(double angle, Qubit target);

        void ApplyUncontrolledRy_AdjointBody(double angle, Qubit target);
    }
}