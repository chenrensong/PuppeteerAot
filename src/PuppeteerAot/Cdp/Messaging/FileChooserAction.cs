using System.Runtime.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    public enum FileChooserAction
    {
        /// <summary>
        /// Accept.
        /// </summary>
        [EnumMember(Value = "accept")]
        Accept,

        /// <summary>
        /// Fallback.
        /// </summary>
        [EnumMember(Value = "fallback")]
        Fallback,

        /// <summary>
        /// Cancel.
        /// </summary>
        [EnumMember(Value = "cancel")]
        Cancel,
    }
}
