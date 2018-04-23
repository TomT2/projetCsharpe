using System;
using Xunit;
using Isen.DotNet.Library;

namespace Isen.DotNet.Tests
{
    public class HelloTest
    {
        [Fact]
        public void World()
        {
            var result = Hello.World;
            var expected = "Hello, World";
            Assert.True(result == expected);
        }
        [Fact]
        public void Greet()
        {
            var result = Hello.Greet("Kall");
            Assert.StartsWith("Hello Kall, it is", result);
        }
        [Fact]
        public void GreetUpper()
        {
            var result = Hello.GreetUpper("Kall");
            Assert.StartsWith("Hello KALL, it is", result);
        }
    }
}
