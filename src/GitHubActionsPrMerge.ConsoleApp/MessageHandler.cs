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
        public virtual string Ref { get; private set; }

        /// <inheritdoc />
        public virtual bool IsMerged { get; private set; }

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
            this.Ref = pr.Head.Ref;

            return this;
        }

        /// <inheritdoc />
        public async Task<IMessageHandler> MergePrAsync(Options options)
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

            this.IsMerged = result.Merged;

            return this;
        }

        /// <inheritdoc />
        public async Task<int> DeleteBranchAsync(Options options)
        {
            if (!this.IsMerged)
            {
                return 1;
            }

            if (!options.DeleteBranch)
            {
                return 0;
            }

            try
            {
                await this.GitHubClient
                          .Git
                          .Reference
                          .Delete(options.Owner, options.Repository, $"heads/{this.Ref}")
                          .ConfigureAwait(false);

                return 0;
            }
            catch
            {
                return 1;
            }
        }
    }
}
