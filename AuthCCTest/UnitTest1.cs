using System;
using System.Threading.Tasks;
using Xunit;

namespace AuthCCTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task AuthCCTest()
        {
            await AuthCC.Program.Main().ConfigureAwait(false);
        }
    }
}
