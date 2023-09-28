using Evergine.Framework;
using XRVSample.Services;

namespace XRVSample.Windows
{
    internal class WindowsApplication : MyApplication
    {
        public WindowsApplication()
        {
            var container = Application.Current.Container;
            container.RegisterInstance(new FakeQRWatcherService());
        }
    }
}
