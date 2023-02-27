using Windows.ApplicationModel.Core;

namespace XRVSample.UWP.MixedReality
{
    // The entry point for the app.
    internal class AppViewSource : IFrameworkViewSource
    {
        public IFrameworkView CreateView()
        {
            return new AppView();
        }
    }
}
