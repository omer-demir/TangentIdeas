using Nancy;
using Nancy.ModelBinding;

namespace TangentIdeas.Web.Modules {
    public class IndexModule : NancyModule {
        public IndexModule() {
            Get["/"] = parameters => View["index"];

            Post["/contact"] = parameters => {
                var data = this.Bind<ContactModel>();
                //TODO: mail gönderimi olacak
                return View["index"];
            };
        }
    }

    public class ContactModel {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
    }
}