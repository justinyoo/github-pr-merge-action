using CommandLine;

using Octokit;

namespace GitHubActions.PrMerge.ConsoleApp
{
    /// <summary>
    /// This represents the parameters entity for the console app.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the repository owner.
        /// </summary>
        [Option('o', "owner", Required = true, HelpText = "GitHub repository owner.")]
        public virtual string Owner { get; set; }

        /// <summary>
        /// Gets or sets the repository name.
        /// </summary>
        [Option('r', "repository", Required = true, HelpText = "GitHub repository")]
        public virtual string Repository { get; set; }

        /// <summary>
        /// Gets or sets the issue ID.
        /// </summary>
        [Option('i', "issue-id", Required = true, HelpText = "GitHub issue ID for the PR.")]
        public virtual int IssueId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MergeType"/> value.
        /// </summary>
        [Option('m', "merge-method", Required = false, Default = PullRequestMergeMethod.Merge, HelpText = "Merge method to use. Possible values are Merge, Squash or Rebase. Default is Merge.")]
        public virtual PullRequestMergeMethod MergeMethod { get; set; }

        /// <summary>
        /// Gets or sets the commit title.
        /// </summary>
        [Option('t', "commit-title", Required = false, Default = "", HelpText = "Title for the automatic commit message.")]
        public virtual string CommitTitle { get; set; }

        /// <summary>
        /// Gets or sets the commit message.
        /// </summary>
        [Option('d', "commit-description", Required = false, Default = "", HelpText = "Extra detail to append to automatic commit message.")]
        public virtual string CommitMessage { get; set; }

        /// <summary>
        /// Gets or sets the commit message.
        /// </summary>
        [Option('b', "delete-branch", Required = false, Default = false, HelpText = "Value indicating whether to delete the branch after the PR merge or not.")]
        public virtual bool DeleteBranch { get; set; }
    }
}
