﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
// 
// Licensed to the Apache Software Foundation (ASF) under one or more
// contributor license agreements. See the NOTICE file distributed with
// this work for additional information regarding copyright ownership.
// The ASF licenses this file to you under the Apache License, Version 2.0
// (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 

using System;
using System.Text;
using System.IO;

using log4net.Core;

namespace log4net.Layout.Pattern
{
    /// <summary>
    /// Writes the caller location file name to the output
    /// </summary>
    /// <remarks>
    /// <para>
    /// Writes the value of the <see cref="LocationInfo.FileName"/> for
    /// the event to the output writer.
    /// </para>
    /// </remarks>
    /// <author>Nicko Cadell</author>
    internal sealed class FileLocationPatternConverter : PatternLayoutConverter 
    {
        /// <summary>
        /// Write the caller location file name to the output
        /// </summary>
        /// <param name="writer"><see cref="TextWriter" /> that will receive the formatted result.</param>
        /// <param name="loggingEvent">the event being logged</param>
        /// <remarks>
        /// <para>
        /// Writes the value of the <see cref="LocationInfo.FileName"/> for
        /// the <paramref name="loggingEvent"/> to the output <paramref name="writer"/>.
        /// </para>
        /// </remarks>
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            writer.Write(loggingEvent.LocationInformation.FileName);
        }
    }
}
