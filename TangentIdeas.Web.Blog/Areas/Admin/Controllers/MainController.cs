using System;
using System.Linq;
using System.Web.Mvc;
using TangentIdeas.Web.Blog.Areas.Admin.Models;
using TangentIdeas.Web.Data.Helpers;
using TangentIdeas.Web.Data.Models;
using TangentIdeas.Web.Data.Services;

namespace TangentIdeas.Web.Blog.Areas.Admin.Controllers {
    public class MainController : Controller {
        private readonly IService<BlogPost> _blogService;
        private readonly IService<User> _userService;
        public MainController(IService<BlogPost> blogService,IService<User> userService) {
            this._blogService = blogService;
            this._userService = userService;
        }

        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model) {
            var item = _userService.GetWhere(a => a.Username == model.Username && a.Password == model.Password).FirstOrDefault();
            if(item != null) {
                return RedirectToAction("Index","Main");
            }
            ViewBag.ErrorMessage = "Kullanıcı giriş hatası";
            return View();
        }

        public ActionResult Index() {
            return View();
        }

        public ActionResult ListBlogPosts() {
            return View(_blogService.GetAll());
        }

        public ActionResult BlogPostDetail(int id) {
            return View();
        }

        public ActionResult CreateBlogPost() {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBlogPost(BlogPostModel model) {
            var serverPath = Server.MapPath("~/Images");
            var fileName = model.ImageUrl.FileName;
            var saveName = serverPath + "/" + fileName;
            var relativePath = "~/Images/" + fileName;
            model.ImageUrl.SaveAs(saveName);

            var blogPost = new BlogPost {
                ActiveStatus = true,
                Author = model.Author,
                Content = model.Content,
                CreateDate = DateTime.Now.ConvertToLong(),
                Tags = model.Tags,
                ImageUrl = relativePath,
                Title = model.Title
            };

            ViewBag.OperationResult = _blogService.SaveOrUpdate(blogPost) > 0 ? "Kayıt başarıyla tamamlandı" : "Kayıt sırasında hata oluştu.";
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage() {
            var image = Request.Files[0];
            if (image==null) {
                return Json("OK");
            }
            var serverPath = Server.MapPath("~/Images");
            var fileName = image.FileName;
            var saveName = serverPath + "/" + fileName;
            var relativePath = "~/Images/" + fileName;
            //CKEDITOR.tools.callFunction("CKEditorFuncNum, " + relativePath + ")
            image.SaveAs(saveName);
            return Content(relativePath);
        }


        public ActionResult EditBlogPost(int id) {
            return View();
        }
        [HttpPost]
        public ActionResult EditBlogPost(BlogPostModel model) {
            return View();
        }

        public ActionResult DeleteBlogPost(int id) {
            _blogService.Delete(id);
            return new JsonResult();
        }
    }
}