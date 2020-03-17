using System;
using System.Threading.Tasks;

using GitHubActionsPrMerge.ConsoleApp.Extensions;

using Octokit;

namespace GitHubActionsPrMerge.ConsoleApp
{
    /// <summary>
    /// This represents the console app entity.
    /// </summary>
    public class MessageHandler : IMessageHandler
    {
        /// <inheritdoc />
        public virtual IGitHubClient GitHubClient { get; private set; }

        /// <inheritdoc />
        public virtual string Sha { get; private set; }

        /// <inheritdoc />
        public IMessageHandler WithGitHubClient(IGitHubClient client)
        {
            this.GitHubClient = client ?? throw new ArgumentNullException(nameof(client));

            return this;
        }

        /// <inheritdoc />
        public async Task<IMessageHandler> FindShaAsync(Options options)
        {
            var pr = await this.GitHubClient
                               .PullRequest
                               .Get(options.Owner, options.Repository, options.IssueId)
                               .ConfigureAwait(false);

            this.Sha = pr.Head.Sha;

            return this;
        }

        /// <inheritdoc />
        public async Task<int> MergePrAsync(Options options)
        {
            var mpr = new MergePullRequest()
                          .WithCommitTitle(options.CommitTitle)
                          .WithCommitMessage(options.CommitMessage)
                          .WithSha(this.Sha)
                          .WithMergeMethod(options.MergeMethod);

            var result = await this.GitHubClient
                                   .PullRequest
                                   .Merge(options.Owner, options.Repository, options.IssueId, mpr)
                                   .ConfigureAwait(false);

            if (result.Merged)
            {
                return 0;
            }

            return 1;
        }
    }
}
