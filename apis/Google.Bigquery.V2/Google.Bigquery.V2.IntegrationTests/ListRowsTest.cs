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


using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Google.Bigquery.V2.IntegrationTests
{
    /// <summary>
    /// Tests specifically for ListRows functionality, separate to queries.
    /// </summary>
    [Collection(nameof(BigqueryFixture))]
    public class ListRowsTest
    {
        private readonly BigqueryFixture _fixture;

        public ListRowsTest(BigqueryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ComplexTypes()
        {
            var client = BigqueryClient.Create(_fixture.ProjectId);
            var dataset = client.GetDataset(_fixture.DatasetId);
            var table = dataset.GetTable(_fixture.ComplexTypesTableId);
            var guid = Guid.NewGuid().ToString();
            var insertRow = new InsertRow
            {
                ["guid"] = guid,
                ["tags"] = new[] { "a", "b" },
                ["position"] = new InsertRow { ["x"] = 10L, ["y"] = 20L },
                ["names"] = new[] {
                    new InsertRow { ["first"] = "a", ["last"] = "b" },
                    new InsertRow { ["first"] = "x", ["last"] = "y" }
                }
            };
            table.Insert(insertRow);
            var result = table.ListRows();
            var row = result.Rows.Single(r => (string)r["guid"] == guid);
            var tags = (string[])row["tags"];
            Assert.Equal(new[] { "a", "b" }, tags);

            var position = (Dictionary<string, object>)row["position"];
            Assert.Equal(new Dictionary<string, object> { ["x"] = 10L, ["y"] = 20L }, position);

            var names = (Dictionary<string, object>[])row["names"];
            Assert.Equal(new[] {
                new Dictionary<string, object> { ["first"] = "a", ["last"] = "b" },
                new Dictionary<string, object> { ["first"] = "x", ["last"] = "y" }
            }, names);
        }
    }
}
