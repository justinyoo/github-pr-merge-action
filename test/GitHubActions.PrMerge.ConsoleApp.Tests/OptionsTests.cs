using CommandLine;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Octokit;

namespace GitHubActions.PrMerge.ConsoleApp.Tests
{
    [TestClass]
    public class OptionsTests
    {
        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Properties()
        {
            typeof(Options)
                .Should().HaveProperty<string>("Owner")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          .And.BeVirtual()
                          ;

            typeof(Options)
                .Should().HaveProperty<string>("Repository")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          .And.BeVirtual()
                          ;

            typeof(Options)
                .Should().HaveProperty<int>("IssueId")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          .And.BeVirtual()
                          ;

            typeof(Options)
                .Should().HaveProperty<PullRequestMergeMethod>("MergeMethod")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          .And.BeVirtual()
                          ;

            typeof(Options)
                .Should().HaveProperty<string>("CommitTitle")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          .And.BeVirtual()
                          ;

            typeof(Options)
                .Should().HaveProperty<string>("CommitMessage")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          .And.BeVirtual()
                          ;

            typeof(Options)
                .Should().HaveProperty<bool>("DeleteBranch")
                    .Which.Should().BeReadable()
                          .And.BeWritable()
                          .And.BeVirtual()
                          ;
        }

        [TestMethod]
        public void Given_Type_Then_It_Should_Have_Decorators()
        {
            typeof(Options)
                .Should().HaveProperty<string>("Owner")
                    .Which.Should().BeDecoratedWith<OptionAttribute>(
                        p => p.ShortName.Equals("o") &&
                             p.LongName.Equals("owner") &&
                             p.Required == true)
                    ;

            typeof(Options)
                .Should().HaveProperty<string>("Repository")
                    .Which.Should().BeDecoratedWith<OptionAttribute>(
                        p => p.ShortName.Equals("r") &&
                             p.LongName.Equals("repository") &&
                             p.Required == true)
                    ;

            typeof(Options)
                .Should().HaveProperty<int>("IssueId")
                    .Which.Should().BeDecoratedWith<OptionAttribute>(
                        p => p.ShortName.Equals("i") &&
                             p.LongName.Equals("issue-id") &&
                             p.Required == true)
                    ;

            typeof(Options)
                .Should().HaveProperty<PullRequestMergeMethod>("MergeMethod")
                    .Which.Should().BeDecoratedWith<OptionAttribute>(
                        p => p.ShortName.Equals("m") &&
                             p.LongName.Equals("merge-method") &&
                             p.Required == false)
                    ;

            typeof(Options)
                .Should().HaveProperty<string>("CommitTitle")
                    .Which.Should().BeDecoratedWith<OptionAttribute>(
                        p => p.ShortName.Equals("t") &&
                             p.LongName.Equals("commit-title") &&
                             p.Required == false &&
                             p.Default as string == string.Empty)
                    ;

            typeof(Options)
                .Should().HaveProperty<string>("CommitMessage")
                    .Which.Should().BeDecoratedWith<OptionAttribute>(
                        p => p.ShortName.Equals("d") &&
                             p.LongName.Equals("commit-description") &&
                             p.Required == false &&
                             p.Default as string == string.Empty)
                    ;

            typeof(Options)
                .Should().HaveProperty<bool>("DeleteBranch")
                    .Which.Should().BeDecoratedWith<OptionAttribute>(
                        p => p.ShortName.Equals("b") &&
                             p.LongName.Equals("delete-branch") &&
                             p.Required == false &&
                             (bool) p.Default == false)
                    ;
        }
    }
}
