namespace PuppeteerAot
{
    /// <summary>
    /// Represents an environment.
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// Gets the CDPSession associated with this environment.
        /// </summary>
        CDPSession Client { get; }

        /// <summary>
        /// Gets the main realm of this environment.
        /// </summary>
        Realm MainRealm { get; }
    }
}
