using NUnit.Framework;
using DependencyInjectionContainerLib;

namespace NUnitTestDependencyInjectionContainer
{
    interface ITest { }

    public class TestImpl : ITest
    {

    }

    public class Tests
    {
        [Test]
        public void Test1()
        {
            var dependencies = new DependenciesConfiguration();
            dependencies.Register<ITest, TestImpl>();

            var result = dependencies.GetDependencies();
            result.Add(4, null);
            result = dependencies.GetDependencies();
            Assert.AreEqual(1, result.Count);
        }
    }
}