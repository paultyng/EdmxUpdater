// Guids.cs
// MUST match guids.h
using System;

namespace EdmxUpdater
{
    static class GuidList
    {
        public const string guidEdmxUpdaterPkgString = "def90417-aedb-485f-ac29-023e4ac7905a";
        public const string guidEdmxUpdaterCmdSetString = "ba40262e-5287-4c95-88fd-5403334fa9c4";

        public static readonly Guid guidEdmxUpdaterCmdSet = new Guid(guidEdmxUpdaterCmdSetString);
    };
}