using System.Threading.Tasks;

namespace GitHubActionsPrMerge.ConsoleApp.Extensions
{
    /// <summary>
    /// This represents the extension entity for the <see cref="MessageHandler"/> class.
    /// </summary>
    public static class MessageHandlerExtensions
    {
        /// <summary>
        /// Merges PR.
        /// </summary>
        /// <param name="value"><see cref="Task{IMessageHandler}"/> instance.</param>
        /// <param name="options"><see cref="Options"/> instance.</param>
        /// <returns>Returns exit code.</returns>
        public static async Task<int> MergePrAsync(this Task<IMessageHandler> value, Options options)
        {
            var instance = await value.ConfigureAwait(false);

            return await instance.MergePrAsync(options).ConfigureAwait(false);
        }
    }
}
