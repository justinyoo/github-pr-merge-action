using System.Linq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Octokit;

using Program = GitHubActions.PrMerge.ConsoleApp.Program;

namespace GitHubActions.PrMerge.ConsoleApp.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Properties()
        {
            typeof(Program)
                .Should().HaveProperty<IGitHubClient>("GitHubClient")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          ;

            typeof(Program)
                .Should().HaveProperty<IMessageHandler>("MessageHandler")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          ;
        }

        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Methods()
        {
            typeof(Program)
                .Should().HaveMethod("Main", new[] { typeof(string[]) })
                ;
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        public void Given_Valid_Args_When_Main_Invoked_Then_It_Should_Return_Result(int exitCode, int expected)
        {
            var handler = new Mock<IMessageHandler>();
            handler.Setup(p => p.WithGitHubClient(It.IsAny<IGitHubClient>())).Returns(handler.Object);
            handler.Setup(p => p.FindShaAsync(It.IsAny<Options>())).ReturnsAsync(handler.Object);
            handler.Setup(p => p.MergePrAsync(It.IsAny<Options>())).ReturnsAsync(handler.Object);
            handler.Setup(p => p.DeleteBranchAsync(It.IsAny<Options>())).ReturnsAsync(exitCode);

            Program.MessageHandler = handler.Object;

            var args = new[] {
                "-o",
                "aliencube",
                "-r",
                "github-pr-merge-action",
                "-i",
                "1",
                "-m",
                "Squash",
                "-b",
                "true"
            }.ToArray();

            var result = Program.Main(args);

            result.Should().Be(expected);
        }

        [TestMethod]
        public void Given_Invalid_Args_When_Main_Invoked_Then_It_Should_Return_Result()
        {
            var handler = new Mock<IMessageHandler>();
            handler.Setup(p => p.WithGitHubClient(It.IsAny<IGitHubClient>())).Returns(handler.Object);
            handler.Setup(p => p.FindShaAsync(It.IsAny<Options>())).ReturnsAsync(handler.Object);
            handler.Setup(p => p.MergePrAsync(It.IsAny<Options>())).ReturnsAsync(handler.Object);
            handler.Setup(p => p.DeleteBranchAsync(It.IsAny<Options>())).ReturnsAsync(0);

            Program.MessageHandler = handler.Object;

            var args = new[] {
                "-o",
                "aliencube",
                "-r",
                "github-pr-merge-action",
                "-i",
                "<issue_number>",
                "-m",
                "Squash",
                "-b",
                "true"
            }.ToArray();

            var result = Program.Main(args);

            result.Should().BeGreaterThan(0);
        }
    }
}
