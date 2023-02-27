using System;
using Windows.ApplicationModel.Core;

namespace XRVSample.UWP.MixedReality
{
    /// <summary>
    /// UWP.MixedReality Holographic application using Evergine.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        [MTAThread]
        private static void Main()
        {
            var exclusiveViewApplicationSource = new AppViewSource();
            CoreApplication.Run(exclusiveViewApplicationSource);
        }
    }
}