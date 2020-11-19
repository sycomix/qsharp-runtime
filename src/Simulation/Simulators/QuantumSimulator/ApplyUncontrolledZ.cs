﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Runtime.InteropServices;
using Microsoft.Quantum.Simulation.Core;

namespace Microsoft.Quantum.Simulation.Simulators
{
    public partial class QuantumSimulator
    {
        public Func<Qubit, QVoid> ApplyUncontrolledZ_Body() => (q1) =>
        {
            this.CheckQubit(q1);

            Z(this.Id, (uint)q1.Id);

            return QVoid.Instance;
        };
    }
}