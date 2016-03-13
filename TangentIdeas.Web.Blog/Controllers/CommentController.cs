using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using TangentIdeas.Web.Data.Helpers;
using TangentIdeas.Web.Data.Models;
using TangentIdeas.Web.Data.Services;
using TangentIdeas.Web.Data.UIModel;

namespace TangentIdeas.Web.Blog.Controllers
{
    public class CommentController : Controller
    {
        private IService<Comment> _commentService;

        public CommentController(IService<Comment> commentService)
        {
            this._commentService = commentService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CommentsPartial(List<CommentUIModel> cList)
        {
            return PartialView("CommentsPartial", cList);
        }

        public ActionResult AddComment(CommentUIModel cuim)
        {
            IService<Comment> _commentService = new CommentService();
            var cm = new Comment
            {
                BlogPostId = cuim.BlogPostId,
                CommentText = cuim.CommentText,
                CreateDate = DateTime.Now.ConvertToLong(),
                Email = cuim.Email,
                Name = cuim.Name,
                Website = cuim.Website,
                ActiveStatus = true
            };

            int result = _commentService.SaveOrUpdate(cm);
            if (result > 0)
            {
                var comments = _commentService.GetWhere(w => w.BlogPostId == cuim.BlogPostId).ToList();
                List<CommentUIModel> cuList = new List<CommentUIModel>();
                foreach (var item in comments)
                {
                    CommentUIModel comUIM = new CommentUIModel
                    {
                        ActiveStatus = item.ActiveStatus,
                        BlogPostId = item.BlogPostId,
                        CommentText = item.CommentText,
                        CreateDate = item.CreateDate.ToString(CultureInfo.InvariantCulture),
                        Email = item.Email,
                        Id = item.Id,
                        Name = item.Name,
                        ParsedCreateDate = item.CreateDate.ToString(CultureInfo.InvariantCulture).ConvertStringToDate(),
                        Website = item.Website,
                    };
                    cuList.Add(comUIM);
                }
                return CommentsPartial(cuList);
            }
            else
            {
                return Content("0");
            }

        }
    }
}