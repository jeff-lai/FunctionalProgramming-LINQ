﻿namespace Tutorial.GettingStarted
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    internal static partial class Linq
    {
        internal static void Dynamic()
        {
            IEnumerable<int> source = new int[] { 4, 3, 2, 1, 0, -1 }; // Get source.
            IEnumerable<dynamic> query =
                from dynamic value in source
                where value.ByPass.Compiler.Check > 0
                orderby value.ByPass().Compiler().Check()
                select value & new object(); // Define query.
            foreach (dynamic result in query) // Execute query.
            {
                Trace.WriteLine(result);
            }
        }
    }

    internal static partial class Linq
    {
        internal static void DelegateTypesWithQueryExpression()
        {
            Assembly coreLibrary = typeof(object).Assembly;
            IEnumerable<IGrouping<string, Type>> delegateGroups =
                from type in coreLibrary.ExportedTypes
                where type.BaseType == typeof(MulticastDelegate)
                group type by type.Namespace into delegateGroup
                orderby delegateGroup.Count() descending, delegateGroup.Key
                select delegateGroup;

            foreach (IGrouping<string, Type> delegateGroup in delegateGroups) // Output.
            {
                Trace.Write(delegateGroup.Count() + " in " + delegateGroup.Key + ":");
                foreach (Type delegateType in delegateGroup)
                {
                    Trace.Write(" " + delegateType.Name);
                }
                Trace.Write(Environment.NewLine);
            }
        }
    }

    internal static partial class Linq
    {
        internal static void DelegateTypesWithQueryMethods()
        {
            Assembly coreLibrary = typeof(object).Assembly;
            IEnumerable<IGrouping<string, Type>> delegateGroups = coreLibrary.ExportedTypes
                .Where(type => type.BaseType == typeof(MulticastDelegate))
                .GroupBy(type => type.Namespace)
                .OrderByDescending(delegateGroup => delegateGroup.Count())
                .ThenBy(delegateGroup => delegateGroup.Key);

            foreach (IGrouping<string, Type> delegateGroup in delegateGroups) // Output.
            {
                Trace.Write(delegateGroup.Count() + " in " + delegateGroup.Key + ":");
                foreach (Type delegateType in delegateGroup)
                {
                    Trace.Write(" " + delegateType.Name);
                }
                Trace.Write(Environment.NewLine);
            }
        }
    }
}
