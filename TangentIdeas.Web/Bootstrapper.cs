using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Nancy;
using HttpUtility = Nancy.Helpers.HttpUtility;

namespace TangentIdeas.Web {
    public class Bootstrapper : DefaultNancyBootstrapper {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

        private byte[] _favicon;

        protected override byte[] FavIcon => _favicon ?? (this._favicon = LoadFavIcon());

        private byte[] LoadFavIcon() {
            //var favIcon = File.ReadAllBytes(HttpContext.Current.Server.MapPath("favicon.ico"));
            //return favIcon;
            return null;
        }
    }
}