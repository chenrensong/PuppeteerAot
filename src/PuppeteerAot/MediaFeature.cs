using System.Runtime.Serialization;

namespace PuppeteerAot
{
    /// <summary>
    /// Meadia Feature. See <see cref="IPage.EmulateMediaFeaturesAsync(System.Collections.Generic.IEnumerable{MediaFeatureValue})"/>.
    /// </summary>
    public enum MediaFeature
    {
        /// <summary>
        /// prefers-color-scheme media feature.
        /// </summary>
        [EnumMember(Value = "prefers-color-scheme")]
        PrefersColorScheme,

        /// <summary>
        /// prefers-reduced-motion media feature.
        /// </summary>
        [EnumMember(Value = "prefers-reduced-motion")]
        PrefersReducedMotion,
    }
}
