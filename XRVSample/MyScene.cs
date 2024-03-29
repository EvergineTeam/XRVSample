using Evergine.Framework;
using Evergine.MRTK.Scenes;
using Evergine.Xrv.Core;
using Evergine.Xrv.Core.Networking;
using System;

namespace XRVSample
{
    public class MyScene : XRScene
    {
        protected override Guid CursorMatPressed => EvergineContent.MRTK.Materials.Cursor.CursorPinch;

        protected override Guid CursorMatReleased => EvergineContent.MRTK.Materials.Cursor.CursorBase;

        protected override Guid HoloHandsMat => EvergineContent.MRTK.Materials.Hands.QuestHands;

        protected override Guid SpatialMappingMat => Guid.Empty;

        protected override Guid HandRayTexture => EvergineContent.MRTK.Textures.line_dots_png;

        protected override Guid HandRaySampler => EvergineContent.MRTK.Samplers.LinearWrapSampler;

        protected override Guid LeftControllerModelPrefab => EvergineContent.MRTK.Prefabs.DefaultLeftController_weprefab;

        protected override Guid RightControllerModelPrefab => EvergineContent.MRTK.Prefabs.DefaultRightController_weprefab;

        protected override float MaxFarCursorLength => 0.5f;

        public override void RegisterManagers()
        {
            base.RegisterManagers();
            this.Managers.AddManager(new global::Evergine.Bullet.BulletPhysicManager3D());
        }

        protected override void OnPostCreateXRScene()
        {
            base.OnPostCreateXRScene();
            var xrv = Application.Current.Container.Resolve<XrvService>();
            xrv.Initialize(this);

            var configuration = new NetworkConfigurationBuilder()
                .ForApplication(nameof(XRVSample))
                .UsePort(12345)
                .SetQrCodeForSession("This is XRV!")
                .Build();
            xrv.Networking.Configuration = configuration;

            // Uncomment to enable networking. Sessions are only supported for 
            // ImageGallery and Ruler modules
            //xrv.Networking.NetworkingAvailable = true;
            xrv.Services.Passthrough.EnablePassthrough = true;
        }
    }
}