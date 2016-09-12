﻿// Copyright 2016 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Api.Gax;

namespace Google.Datastore.V1
{
    public partial class KindExpression
    {
        /// <summary>
        /// Constructs a <see cref="KindExpression"/> directly from the name of the kind.
        /// </summary>
        /// <param name="name">The name of the kind. Must not be null.</param>
        public KindExpression(string name) : this()
        {
            Name = GaxPreconditions.CheckNotNull(name, nameof(name)); ;
        }
    }
}
