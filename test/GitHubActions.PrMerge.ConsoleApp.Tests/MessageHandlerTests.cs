using System;
using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.AutoMoq;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Octokit;

namespace GitHubActions.PrMerge.ConsoleApp.Tests
{
    [TestClass]
    public class MessageHandlerTests
    {
        private IFixture _fixture;

        [TestInitialize]
        public void Init()
        {
            this._fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        }

        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Constructors()
        {
            typeof(MessageHandler)
                .Should().HaveDefaultConstructor()
                ;
        }

        [TestMethod]
        public void Given_Type_Then_It_Should_Implement_Interfaces()
        {
            typeof(MessageHandler)
                .Should().Implement<IMessageHandler>()
                ;
        }

        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Properties()
        {
            typeof(MessageHandler)
                .Should().HaveProperty<IGitHubClient>("GitHubClient")
                    .Which.Should().BeVirtual()
                    .And.BeReadable()
                    .And.BeWritable()
                    .And.BeVirtual()
                    ;

            typeof(MessageHandler)
                .Should().HaveProperty<string>("Sha")
                    .Which.Should().BeVirtual()
                    .And.BeReadable()
                    .And.BeWritable()
                    .And.BeVirtual()
                    ;

            typeof(MessageHandler)
                .Should().HaveProperty<string>("Ref")
                    .Which.Should().BeVirtual()
                    .And.BeReadable()
                    .And.BeWritable()
                    .And.BeVirtual()
                    ;

            typeof(MessageHandler)
                .Should().HaveProperty<bool>("IsMerged")
                    .Which.Should().BeVirtual()
                    .And.BeReadable()
                    .And.BeWritable()
                    .And.BeVirtual()
                    ;
        }

        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Methods()
        {
            typeof(MessageHandler)
                .Should().HaveMethod("WithGitHubClient", new[] { typeof(IGitHubClient) })
                ;

            typeof(MessageHandler)
                .Should().HaveMethod("FindShaAsync", new[] { typeof(Options) } )
                ;

            typeof(MessageHandler)
                .Should().HaveMethod("MergePrAsync", new[] { typeof(Options) } )
                ;

            typeof(MessageHandler)
                .Should().HaveMethod("DeleteBranchAsync", new[] { typeof(Options) } )
                ;
        }

        [TestMethod]
        public void Given_Null_Parameters_When_WithGitHubClient_Invoked_Then_It_Should_Throw_Exception()
        {
            var handler = new MessageHandler();

            Action action = () => handler.WithGitHubClient(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_Parameters_When_WithGitHubClient_Invoked_Then_It_Should_Return_Property()
        {
            var handler = new MessageHandler();

            handler.GitHubClient.Should().BeNull();

            var client = new Mock<IGitHubClient>();
            handler.WithGitHubClient(client.Object);

            handler.GitHubClient.Should().NotBeNull();
        }

        [TestMethod]
        public void Given_Null_Parameters_When_FindShaAsync_Invoked_Then_It_Should_Throw_Exception()
        {
            var handler = new MessageHandler();

            Func<Task> func = async () => await handler.FindShaAsync(null).ConfigureAwait(false);
            func.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_No_GitHubClient_When_FindShaAsync_Invoked_Then_It_Should_Throw_Exception()
        {
            var handler = new MessageHandler();
            var options = new Mock<Options>();

            Func<Task> func = async () => await handler.FindShaAsync(options.Object).ConfigureAwait(false);
            func.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public async Task Given_Parameters_When_FindShaAsync_Invoked_Then_It_Should_Return_Property()
        {
            var sha = this._fixture.Create<string>();
            var @ref = this._fixture.Create<string>();

            var head = new GitReference().SetValue("Sha", sha).SetValue("Ref", @ref);
            var pr = new PullRequest().SetValue("Head", head);

            var prc = new Mock<IPullRequestsClient>();
            prc.Setup(p => p.Get(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(pr);

            var client = new Mock<IGitHubClient>();
            client.SetupGet(p => p.PullRequest).Returns(prc.Object);

            var handler = new MessageHandler()
                              .WithGitHubClient(client.Object);

            var options = new Options();

            await handler.FindShaAsync(options).ConfigureAwait(false);

            handler.Sha.Should().BeEquivalentTo(pr.Head.Sha);
            handler.Ref.Should().BeEquivalentTo(pr.Head.Ref);
        }

        [TestMethod]
        public void Given_Null_Parameters_When_MergePrAsync_Invoked_Then_It_Should_Throw_Exception()
        {
            var handler = new MessageHandler();

            Func<Task> func = async () => await handler.MergePrAsync(null).ConfigureAwait(false);
            func.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_No_GitHubClient_When_MergePrAsync_Invoked_Then_It_Should_Throw_Exception()
        {
            var handler = new MessageHandler();
            var options = new Mock<Options>();

            Func<Task> func = async () => await handler.MergePrAsync(options.Object).ConfigureAwait(false);
            func.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public async Task Given_Parameters_When_MergePrAsync_Invoked_Then_It_Should_Return_Property()
        {
            var sha = this._fixture.Create<string>();
            var merged = this._fixture.Create<bool>();

            var prm = new PullRequestMerge().SetValue("Merged", merged);

            var prc = new Mock<IPullRequestsClient>();
            prc.Setup(p => p.Merge(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<MergePullRequest>())).ReturnsAsync(prm);

            var client = new Mock<IGitHubClient>();
            client.SetupGet(p => p.PullRequest).Returns(prc.Object);

            var options = new Options();

            var handler = new MessageHandler()
                              .SetValue("Sha",  sha)
                              .WithGitHubClient(client.Object);

            await handler.MergePrAsync(options).ConfigureAwait(false);

            handler.IsMerged.Should().Be(prm.Merged);
        }

        [TestMethod]
        public void Given_Null_Parameters_When_DeleteBranchAsync_Invoked_Then_It_Should_Throw_Exception()
        {
            var handler = new MessageHandler();

            Func<Task> func = async () => await handler.DeleteBranchAsync(null).ConfigureAwait(false);
            func.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_No_GitHubClient_When_DeleteBranchAsync_Invoked_Then_It_Should_Throw_Exception()
        {
            var handler = new MessageHandler();
            var options = new Mock<Options>();

            Func<Task> func = async () => await handler.DeleteBranchAsync(options.Object).ConfigureAwait(false);
            func.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public async Task Given_IsNotMerged_When_DeleteBranchAsync_Invoked_Then_It_Should_Return_1()
        {
            var client = new Mock<IGitHubClient>();
            var handler = new MessageHandler()
                              .SetValue("IsMerged", false)
                              .WithGitHubClient(client.Object);
            var options = new Options();

            var result = await handler.DeleteBranchAsync(options);

            result.Should().Be(1);
        }

        [TestMethod]
        public async Task Given_NotDeleteBranch_When_DeleteBranchAsync_Invoked_Then_It_Should_Return_0()
        {
            var client = new Mock<IGitHubClient>();
            var handler = new MessageHandler()
                              .SetValue("IsMerged", true)
                              .WithGitHubClient(client.Object);
            var options = new Options().SetValue("DeleteBranch", false);

            var result = await handler.DeleteBranchAsync(options);

            result.Should().Be(0);
        }

        [TestMethod]
        public async Task Given_Parameters_When_DeleteBranchAsync_Invoked_Then_It_Should_Return_1()
        {
            var sha = this._fixture.Create<string>();
            var merged = this._fixture.Create<bool>();

            var reference = new Mock<IReferencesClient>();
            reference.Setup(p => p.Delete(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());

            var git = new Mock<IGitDatabaseClient>();
            git.SetupGet(p => p.Reference).Returns(reference.Object);

            var client = new Mock<IGitHubClient>();
            client.SetupGet(p => p.Git).Returns(git.Object);

            var options = new Options().SetValue("DeleteBranch", true);

            var handler = new MessageHandler()
                              .SetValue("Sha", sha)
                              .SetValue("IsMerged", true)
                              .WithGitHubClient(client.Object);

            var result = await handler.DeleteBranchAsync(options).ConfigureAwait(false);

            result.Should().Be(1);
        }

        [TestMethod]
        public async Task Given_Parameters_When_DeleteBranchAsync_Invoked_Then_It_Should_Return_0()
        {
            var sha = this._fixture.Create<string>();
            var merged = this._fixture.Create<bool>();

            var reference = new Mock<IReferencesClient>();

            var git = new Mock<IGitDatabaseClient>();
            git.SetupGet(p => p.Reference).Returns(reference.Object);

            var client = new Mock<IGitHubClient>();
            client.SetupGet(p => p.Git).Returns(git.Object);

            var options = new Options().SetValue("DeleteBranch", true);

            var handler = new MessageHandler()
                              .SetValue("Sha", sha)
                              .SetValue("IsMerged", true)
                              .WithGitHubClient(client.Object);

            var result = await handler.DeleteBranchAsync(options).ConfigureAwait(false);

            result.Should().Be(0);
        }
    }
}
