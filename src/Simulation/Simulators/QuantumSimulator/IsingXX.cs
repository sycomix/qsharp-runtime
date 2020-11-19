// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Runtime.InteropServices;
using Microsoft.Quantum.Simulation.Core;

namespace Microsoft.Quantum.Simulation.Simulators
{
    public partial class QuantumSimulator
    {
        public Func<(double, Qubit, Qubit), QVoid> IsingXX_Body() => (args) =>
        {
            var (angle, qubit1, qubit2) = args;
            var paulis = new Pauli[]{ Pauli.PauliX, Pauli.PauliX };
            var targets = new QArray<Qubit>(new Qubit[]{ qubit1, qubit2 });
            CheckAngle(angle);
            this.CheckQubits(targets);

            Exp(this.Id, (uint)targets.Length, paulis, angle * 2.0, targets.GetIds());

            return QVoid.Instance;
        };

        public Func<(double, Qubit, Qubit), QVoid> IsingXX_AdjointBody() => (args) =>
        {
            var (angle, qubit1, qubit2) = args;

            return IsingXX_Body().Invoke((-angle, qubit1, qubit2));
        };

        public Func<(IQArray<Qubit>, (double, Qubit, Qubit)), QVoid> IsingXX_ControlledBody() => (args) =>
        {
            var (ctrls, (angle, qubit1, qubit2)) = args;

            if (ctrls == null || ctrls.Length == 0)
            {
                IsingXX_Body().Invoke((angle, qubit1, qubit2));
            }
            else
            {
                var targets = new QArray<Qubit>(new Qubit[]{ qubit1, qubit2 });
                var paulis = new Pauli[]{ Pauli.PauliX, Pauli.PauliX };
                CheckAngle(angle);
                this.CheckQubits(QArray<Qubit>.Add(ctrls, targets));

                MCExp(this.Id, (uint)targets.Length, paulis, angle * 2.0, (uint)ctrls.Length, ctrls.GetIds(), targets.GetIds());
            }

            return QVoid.Instance;
        };

        public Func<(IQArray<Qubit>, (double, Qubit, Qubit)), QVoid> IsingXX_ControlledAdjointBody() => (args) =>
        {
            var (ctrls, (angle, qubit1, qubit2)) = args;

            return IsingXX_ControlledBody().Invoke((ctrls, (-angle, qubit1, qubit2)));
        };
    }
}