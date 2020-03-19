using System.Reflection;

namespace GitHubActions.PrMerge.ConsoleApp.Tests
{
    /// <summary>
    /// This represents the extension entity for the <see cref="PropertyInfo"/> class.
    /// </summary>
    internal static class PropertyInfoExtensions
    {
        /// <summary>
        /// Sets the property value that has a non-accessible setter.
        /// </summary>
        /// <typeparam name="T">Type of the instance.</typeparam>
        /// <param name="instance">Instance to set the property value.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="value">Property value.</param>
        /// <returns>Returns the instance.</returns>
        public static T SetValue<T>(this T instance, string propertyName, object value)
        {
            var pi = typeof(T).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            pi.SetValue(instance, value);

            return instance;
        }
    }
}
