using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitTestApi.BookRepositoryTest
{
    [CollectionDefinition("InMemoryDatabaseCollection")]
    public class InMemoryDatabaseCollection : ICollectionFixture<InMemoryDatabaseFixture>
    {
    }
}
