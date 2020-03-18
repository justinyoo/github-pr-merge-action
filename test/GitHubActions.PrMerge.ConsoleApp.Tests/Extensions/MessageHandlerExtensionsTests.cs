using System;
using System.Threading.Tasks;

using FluentAssertions;

using GitHubActions.PrMerge.ConsoleApp.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace GitHubActions.PrMerge.ConsoleApp.Tests.Extensions
{
    [TestClass]
    public class MessageHandlerExtensionsTests
    {
        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Methods()
        {
            typeof(MessageHandlerExtensions)
                .Should().HaveMethod("MergePrAsync", new[] { typeof(Task<IMessageHandler>), typeof(Options) })
                ;

            typeof(MessageHandlerExtensions)
                .Should().HaveMethod("DeleteBranchAsync", new[] { typeof(Task<IMessageHandler>), typeof(Options) })
                ;
        }

        [TestMethod]
        public void Given_Null_Parameters_When_MergePrAsync_Invoked_Then_It_Throws_Exception()
        {
            var handler = new Mock<IMessageHandler>();

            Func<Task> func = async () => await MessageHandlerExtensions.MergePrAsync(null, null).ConfigureAwait(false);
            func.Should().Throw<ArgumentNullException>();

            func = async () => await MessageHandlerExtensions.MergePrAsync(Task.FromResult(handler.Object), null).ConfigureAwait(false);
            func.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_Null_Parameters_When_DeleteBranchAsync_Invoked_Then_It_Throws_Exception()
        {
            var handler = new Mock<IMessageHandler>();

            Func<Task> func = async () => await MessageHandlerExtensions.DeleteBranchAsync(null, null).ConfigureAwait(false);
            func.Should().Throw<ArgumentNullException>();

            func = async () => await MessageHandlerExtensions.DeleteBranchAsync(Task.FromResult(handler.Object), null).ConfigureAwait(false);
            func.Should().Throw<ArgumentNullException>();
        }
    }
}
