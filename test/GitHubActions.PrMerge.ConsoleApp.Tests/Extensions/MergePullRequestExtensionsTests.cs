using System;

using FluentAssertions;

using GitHubActions.PrMerge.ConsoleApp.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Octokit;

namespace GitHubActions.PrMerge.ConsoleApp.Tests.Extensions
{
    [TestClass]
    public class MergePullRequestExtensionsTests
    {
        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Methods()
        {
            typeof(MergePullRequestExtensions)
                .Should().HaveMethod("WithCommitTitle", new[] { typeof(MergePullRequest), typeof(string) })
                ;

            typeof(MergePullRequestExtensions)
                .Should().HaveMethod("WithCommitMessage", new[] { typeof(MergePullRequest), typeof(string) })
                ;

            typeof(MergePullRequestExtensions)
                .Should().HaveMethod("WithSha", new[] { typeof(MergePullRequest), typeof(string) })
                ;

            typeof(MergePullRequestExtensions)
                .Should().HaveMethod("WithMergeMethod", new[] { typeof(MergePullRequest), typeof(PullRequestMergeMethod) })
                ;
        }

        [TestMethod]
        public void Given_Null_Parameters_When_WithCommitTitle_Invoked_Then_It_Throws_Exception()
        {
            Action action = () => MergePullRequestExtensions.WithCommitTitle(null, null);
            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow("hello world", "hello world")]
        public void Given_Null_Parameters_When_WithCommitTitle_Invoked_Then_It_Return_Value(string value, string expected)
        {
            var mpr = new MergePullRequest();

            var result = MergePullRequestExtensions.WithCommitTitle(mpr, value);

            result.CommitTitle.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Given_Null_Parameters_When_WithCommitMessage_Invoked_Then_It_Throws_Exception()
        {
            Action action = () => MergePullRequestExtensions.WithCommitMessage(null, null);
            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow("hello world", "hello world")]
        public void Given_Null_Parameters_When_WithCommitMessage_Invoked_Then_It_Return_Value(string value, string expected)
        {
            var mpr = new MergePullRequest();

            var result = MergePullRequestExtensions.WithCommitMessage(mpr, value);

            result.CommitMessage.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Given_Null_Parameters_When_WithSha_Invoked_Then_It_Throws_Exception()
        {
            var mpr = new MergePullRequest();

            Action action = () => MergePullRequestExtensions.WithSha(null, null);
            action.Should().Throw<ArgumentNullException>();

            action = () => MergePullRequestExtensions.WithSha(mpr, null);
            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("hello world", "hello world")]
        public void Given_Null_Parameters_When_WithSha_Invoked_Then_It_Return_Value(string value, string expected)
        {
            var mpr = new MergePullRequest();

            var result = MergePullRequestExtensions.WithSha(mpr, value);

            result.Sha.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Given_Null_Parameters_When_WithMergeMethod_Invoked_Then_It_Throws_Exception()
        {
            var mpr = new MergePullRequest();

            Action action = () => MergePullRequestExtensions.WithMergeMethod(null, PullRequestMergeMethod.Merge);
            action.Should().Throw<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow(PullRequestMergeMethod.Merge, PullRequestMergeMethod.Merge)]
        [DataRow(PullRequestMergeMethod.Squash, PullRequestMergeMethod.Squash)]
        [DataRow(PullRequestMergeMethod.Rebase, PullRequestMergeMethod.Rebase)]
        public void Given_Null_Parameters_When_WithMergeMethod_Invoked_Then_It_Return_Value(PullRequestMergeMethod value, PullRequestMergeMethod expected)
        {
            var mpr = new MergePullRequest();

            var result = MergePullRequestExtensions.WithMergeMethod(mpr, value);

            result.MergeMethod.Should().Be(expected);
        }
    }
}
