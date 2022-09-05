using Xunit;
using MarkupReader;

namespace MarkupReader_Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string name = @"C:\Users\Jadu\source\repos\Jadugar89\TIA_Generator\po.xml";
            var markup = new Markup(name).ReadMarkupFile();
            
        }
    }
}