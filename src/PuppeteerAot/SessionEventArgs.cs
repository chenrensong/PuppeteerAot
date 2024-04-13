namespace PuppeteerAot
{
    /// <summary>
    /// Session event arguments.
    /// </summary>
    public class SessionEventArgs
    {
        public SessionEventArgs(ICDPSession session)
        {
            Session = session;
        }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        public ICDPSession Session { get; set; }
    }
}
