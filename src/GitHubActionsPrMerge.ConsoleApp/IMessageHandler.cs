using System.Threading.Tasks;

using Octokit;

namespace GitHubActionsPrMerge.ConsoleApp
{
    /// <summary>
    /// This provides interfaces to the <see cref="MessageHandler" /> class.
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Gets the <see cref="IGitHubClient"/> instance.
        /// </summary>
        IGitHubClient GitHubClient { get; }

        /// <summary>
        /// Gets the SHA value.
        /// </summary>
        string Sha { get; }

        /// <summary>
        /// Gets the HEAD ref value.
        /// </summary>
        /// <value></value>
        string Ref { get; }

        /// <summary>
        /// Gets the value indicating whether the PR has been merged or not.
        /// </summary>
        bool IsMerged { get; }

        /// <summary>
        /// Sets the <see cref="IGitHubClient"/> instance.
        /// </summary>
        /// <param name="client"><see cref="IGitHubClient"/> instance.</param>
        /// <returns>Returns <see cref="IMessageHandler"/> instance.</returns>
        IMessageHandler WithGitHubClient(IGitHubClient client);

        /// <summary>
        /// Gets the SHA value of the PR commit head.
        /// </summary>
        /// <param name="options"><see cref="Options"/> instance.</param>
        /// <returns>Returns <see cref="IMessageHandler"/> instance.</returns>
        Task<IMessageHandler> FindShaAsync(Options options);

        /// <summary>
        /// Performs the PR merge.
        /// </summary>
        /// <param name="options"><see cref="Options"/> instance.</param>
        /// <returns>Returns <see cref="IMessageHandler"/> instance.</returns>
        Task<IMessageHandler> MergePrAsync(Options options);

        /// <summary>
        /// Deletes the branch after the PR merge.
        /// </summary>
        /// <param name="options"><see cref="Options"/> instance.</param>
        /// <returns>Returns the exit code. 0 means success.</returns>
        Task<int> DeleteBranchAsync(Options options);
    }
}
