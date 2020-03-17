using Octokit;

namespace GitHubActionsPrMerge.ConsoleApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="GitHubClient"/> class.
    /// </summary>
    public static class GitHubClientExtensions
    {
        /// <summary>
        /// Adds <see cref="Credentials"/> instance.
        /// </summary>
        /// <param name="value"><see cref="GitHubClient"/> instance.</param>
        /// <param name="token">GitHub auth token.</param>
        /// <returns>Returns the <see cref="GitHubClient"/> instance.</returns>
        public static GitHubClient WithCredentials(this GitHubClient value, string token)
        {
            value.Credentials = new Credentials(token);

            return value;
        }

        /// <summary>
        /// Adds <see cref="Credentials"/> instance.
        /// </summary>
        /// <param name="value"><see cref="GitHubClient"/> instance.</param>
        /// <param name="username">GitHub username.</param>
        /// <param name="password">GitHub password.</param>
        /// <returns>Returns the <see cref="GitHubClient"/> instance.</returns>
        public static GitHubClient WithCredentials(this GitHubClient value, string username, string password)
        {
            value.Credentials = new Credentials(username, password);

            return value;
        }
    }
}
