using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Nancy;
using Nancy.ModelBinding;
using TangentIdeas.Web.Data.Services;
using TangentIdeas.Web.Model;

namespace TangentIdeas.Web.Modules {
    public class IndexModule : NancyModule {
        public IndexModule() {
            var blogService = new BlogService();

            Get["/"] = parameters => {
                var latestBlogPost = blogService.GetWhere(a => a.ActiveStatus).OrderByDescending(a => a.CreateDate).Take(3);

                var blogModelList = latestBlogPost.Select(a => new BlogModel {
                    Subject = a.Title,
                    Date = a.CreateDate.ToString(),
                    ImageUrl = "http://blog.tangentideas.net/" + a.ImageUrl.Replace("~", ""),
                    ShortDescription = a.ShortDescription,
                    Url = $"http://blog.tangentideas.net/BlogDetail/{a.Id}"
                });
                return View["index",blogModelList];
            };

            Post["/contact"] = parameters => {
                var data = this.Bind<ContactModel>();
                //TODO: mail gönderimi olacak
                object result = null;
                var mailer = new SmtpClient {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("ofaruk.demir@tangentideas.net", "ofaruk123")
                };

                var mail = new MailMessage("ofaruk.demir@tangentideas.net", "ofaruk.demir@tangentideas.net") {
                    Body = $"You have new message about {data.Message} and received from {data.Fullname} with reason {data.Reason} and client email is {data.Email}",
                    IsBodyHtml = false,
                    Priority = MailPriority.High,
                    Subject = "New Message from website"
                };

                mailer.SendAsync(mail, result);
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