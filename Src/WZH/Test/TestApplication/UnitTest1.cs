using System.Threading.Tasks;
using WZH.Application.SyncData;
using Xunit;
using Xunit.Abstractions;

namespace TestApplication
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _outputHelper;
        private readonly ISyncDataAppCmd _syncDataAppCmd;

        public UnitTest1(ISyncDataAppCmd syncDataAppCmd, ITestOutputHelper outputHelper)
        {
            _syncDataAppCmd = syncDataAppCmd;
            _outputHelper = outputHelper;
        }

        /// <summary>
        /// ≤‚ ‘
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test1()
        {
            var data = await _syncDataAppCmd.Dept();
            _outputHelper.WriteLine(data.ToString());
            Assert.True(!data.success, "÷¥––≥…π¶£°");
        }
    }
}