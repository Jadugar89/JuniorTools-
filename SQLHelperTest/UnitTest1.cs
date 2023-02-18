using Xunit;
using BaseSQL;

namespace SQLHelperTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            BaseSQL.SqlManager.GetDataSources();

        }
    }
}