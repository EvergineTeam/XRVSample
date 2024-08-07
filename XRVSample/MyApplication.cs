using Evergine.Framework;
using Evergine.Framework.Services;
using Evergine.Framework.Threading;
using Evergine.Platform;
using Evergine.Xrv.Core;
using Evergine.Xrv.Core.Storage;
using Evergine.Xrv.ImageGallery;
using Evergine.Xrv.ModelViewer;
using Evergine.Xrv.Painter;
using Evergine.Xrv.Ruler;
using Evergine.Xrv.StreamingViewer;
using System;
using Random = Evergine.Framework.Services.Random;

namespace XRVSample
{
    public partial class MyApplication : Application
    {
        public MyApplication()
        {
            this.Container.Register<Settings>();
            this.Container.Register<Clock>();
            this.Container.Register<TimerFactory>();
            this.Container.Register<Random>();
            this.Container.Register<ErrorHandler>();
            this.Container.Register<ScreenContextManager>();
            this.Container.Register<GraphicsPresenter>();
            this.Container.Register<AssetsDirectory>();
            this.Container.Register<AssetsService>();
            this.Container.Register<ForegroundTaskSchedulerService>();
            this.Container.Register<WorkActionScheduler>();

            BackgroundTaskScheduler.Background.Configure(this.Container);
        }

        public override void Initialize()
        {
            base.Initialize();

            this.InitializeXrv();

            // Get ScreenContextManager
            var screenContextManager = this.Container.Resolve<ScreenContextManager>();
            var assetsService = this.Container.Resolve<AssetsService>();

            // Navigate to scene
            var scene = assetsService.Load<MyScene>(EvergineContent.Scenes.MyScene_wescene);
            ScreenContext screenContext = new ScreenContext(scene);
            screenContextManager.To(screenContext);
        }

        private void InitializeXrv()
        {
            var containerUri = new Uri("https://xrvdevelopment.blob.core.windows.net/publicmodels");
            var modelsAccess = AzureBlobFileAccess.CreateFromUri(containerUri);

            containerUri = new Uri("https://xrvdevelopment.blob.core.windows.net/publicimages");
            var imageGalleryFileAccess = AzureBlobFileAccess.CreateFromUri(containerUri);

            var xrv = new XrvService()
                .AddModule(new RulerModule())
                .AddModule(new ModelViewerModule
                {
                    Repositories = new[]
                    {
                        new Repository
                        {
                            Name = "Sample Models",
                            FileAccess = modelsAccess,
                        }
                    },
                    NormalizedModelEnabled = true,
                    NormalizedModelSize = 0.2f,
                })
                .AddModule(new ImageGalleryModule
                {
                    FileAccess = imageGalleryFileAccess,
                    ImagePixelsWidth = 640,
                    ImagePixelsHeight = 640,
                })
                .AddModule(new StreamingViewerModule
                {
                    SourceURL = "http://77.222.181.11:8080/mjpg/video.mjpg",
                })
                .AddModule(new PainterModule());
            this.Container.RegisterInstance(xrv);
        }
    }
}
