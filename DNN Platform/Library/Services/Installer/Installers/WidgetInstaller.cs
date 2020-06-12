﻿

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
using System.IO;

using DotNetNuke.Common;

namespace DotNetNuke.Services.Installer.Installers
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The WidgetInstaller installs Widget Components to a DotNetNuke site.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// -----------------------------------------------------------------------------
    public class WidgetInstaller : FileInstaller
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets the name of the Collection Node ("widgetFiles").
        /// </summary>
        /// <value>A String.</value>
        /// -----------------------------------------------------------------------------
        protected override string CollectionNodeName
        {
            get
            {
                return "widgetFiles";
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets the name of the Item Node ("widgetFiles").
        /// </summary>
        /// <value>A String.</value>
        /// -----------------------------------------------------------------------------
        protected override string ItemNodeName
        {
            get
            {
                return "widgetFile";
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets the PhysicalBasePath for the widget files.
        /// </summary>
        /// <value>A String.</value>
        /// -----------------------------------------------------------------------------
        protected override string PhysicalBasePath
        {
            get
            {
                string widgetPath = Path.Combine("Resources\\Widgets\\User", this.BasePath);
                return Path.Combine(Globals.ApplicationMapPath, widgetPath);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets a list of allowable file extensions (in addition to the Host's List).
        /// </summary>
        /// <value>A String.</value>
        /// -----------------------------------------------------------------------------
        public override string AllowableFiles
        {
            get
            {
                return "js";
            }
        }
    }
}
