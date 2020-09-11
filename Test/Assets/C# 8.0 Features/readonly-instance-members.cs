﻿/*
# readonly instance members

Provide a way to specify individual instance members on a struct do not modify state, in the same way that `readonly struct` specifies no instance members modify state.
It is worth noting that `readonly instance member` != `pure instance member`. A `pure` instance member guarantees no state will be modified. A `readonly` instance member only guarantees that instance state will not be modified.
All instance members on a `readonly struct` could be considered implicitly `readonly instance members`. Explicit `readonly instance members` declared on non-readonly structs would behave in the same manner. For example, they would still create hidden copies if you called an instance member (on the current instance or on a field of the instance) which was itself not-readonly.
*/

using System;

namespace CSharp_8_Features.ReadonlyInstanceMembers
{
    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public readonly double Distance => Math.Sqrt(X * X + Y * Y);

        // NG: Because ToString accesses the Distance property, which isn't marked readonly:
        // public double Distance => Math.Sqrt(X * X + Y * Y);

        public readonly override string ToString() => $"({X}, {Y}) is {Distance} from the origin";

        // NG: The compiler does enforce the rule that readonly members don't modify state.
        // The following method won't compile unless you remove the readonly modifier:
        // public readonly void Translate(int xOffset, int yOffset)
        // {
        //     X += xOffset;
        //     Y += yOffset;
        // }
    }
}
