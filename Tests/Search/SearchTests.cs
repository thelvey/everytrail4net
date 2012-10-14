using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EveryTrailNET.Core;
using EveryTrailNET.Core.QueryResponse;
using EveryTrailNET.Objects;
using System.Net;
using System.IO;

namespace Tests.Search
{
    [TestFixture]
    public class SearchTests
    {
        [Test]
        public void BasicSearch()
        {
            string searchQuery = "\"arizona trail\"";
            SearchResponse response = Actions.Search(searchQuery);

        }
    }
}
