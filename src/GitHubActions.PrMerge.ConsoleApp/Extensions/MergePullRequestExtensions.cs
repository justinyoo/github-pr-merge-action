using System;

using Octokit;

namespace GitHubActions.PrMerge.ConsoleApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="MergePullRequest"/> class.
    /// </summary>
    public static class MergePullRequestExtensions
    {
        /// <summary>
        /// Adds commit title to PR merge request.
        /// </summary>
        /// <param name="value"><see cref="MergePullRequest"/> instance.</param>
        /// <param name="commitTitle">Commit message title.</param>
        /// <returns>Returns the <see cref="MergePullRequest"/> instance.</returns>
        public static MergePullRequest WithCommitTitle(this MergePullRequest value, string commitTitle)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.CommitTitle = commitTitle;

            return value;
        }

        /// <summary>
        /// Adds commit message to PR merge request.
        /// </summary>
        /// <param name="value"><see cref="MergePullRequest"/> instance.</param>
        /// <param name="commitMessage">Commit message.</param>
        /// <returns>Returns the <see cref="MergePullRequest"/> instance.</returns>
        public static MergePullRequest WithCommitMessage(this MergePullRequest value, string commitMessage)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.CommitMessage = commitMessage;

            return value;
        }

        /// <summary>
        /// Adds SHA value to PR merge request.
        /// </summary>
        /// <param name="value"><see cref="MergePullRequest"/> instance.</param>
        /// <param name="sha">SHA value.</param>
        /// <returns>Returns the <see cref="MergePullRequest"/> instance.</returns>
        public static MergePullRequest WithSha(this MergePullRequest value, string sha)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(sha))
            {
                throw new ArgumentNullException(nameof(sha));
            }

            value.Sha = sha;

            return value;
        }

        /// <summary>
        /// Adds <see cref="PullRequestMergeMethod"/> value to PR merge request.
        /// </summary>
        /// <param name="value"><see cref="MergePullRequest"/> instance.</param>
        /// <param name="mergeMethod"><see cref="PullRequestMergeMethod"/> value.</param>
        /// <returns>Returns the <see cref="MergePullRequest"/> instance.</returns>
        public static MergePullRequest WithMergeMethod(this MergePullRequest value, PullRequestMergeMethod mergeMethod)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.MergeMethod = mergeMethod;

            return value;
        }
    }
}
