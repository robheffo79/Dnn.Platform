﻿

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
using System;

namespace DotNetNuke.UI.WebControls
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IsReadOnlyAttribute : Attribute
    {
        private readonly bool _IsReadOnly;

        /// <summary>
        /// Initializes a new instance of the ReadOnlyAttribute class.
        /// </summary>
        /// <param name="read">A boolean that indicates whether the property is ReadOnly.</param>
        public IsReadOnlyAttribute(bool read)
        {
            this._IsReadOnly = read;
        }

        public bool IsReadOnly
        {
            get
            {
                return this._IsReadOnly;
            }
        }
    }
}
