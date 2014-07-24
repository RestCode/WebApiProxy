using NUnit.Framework;

namespace WebApiProxy.Tasks.FunctionalTests
{
    [TestFixture]
    public class ProxyGenerationTaskTest
    {
        [Test]
        public void Execute_WebApiUrl_ClientGenerated()
        {
            var target = new ProxyGenerationTask();
            target.Filename = "test.cs";
            target.Execute();
        }
    }
}
