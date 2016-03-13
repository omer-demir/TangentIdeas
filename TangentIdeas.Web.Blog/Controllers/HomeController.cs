using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TangentIdeas.Web.Data.Models;
using TangentIdeas.Web.Data.Services;
using TangentIdeas.Web.Data.UIModel;
using WebGrease.Css.Extensions;

namespace TangentIdeas.Web.Blog.Controllers
{
    public class HomeController : Controller
    {
        private IService<BlogPost> _blogService;
        public HomeController(IService<BlogPost> blogService)
        {
            this._blogService = blogService;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var uiModelList = new List<BlogPostUIModel>();
            _blogService.GetAll().ForEach(a =>
            {
                uiModelList.Add(BlogPostUIModel.MapFromEntity(a));
            });
            return View(uiModelList);
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult BlogDetail(int id)
        {
            var uiModel = BlogPostUIModel.MapFromEntity(_blogService.GetFirstOrDefault(id));
            return View(uiModel);
        }

        public ActionResult AboutUsPartial()
        {
            return PartialView();
        }
        

        public ActionResult ArchivesPartial()
        {
            var uiModelList = new List<BlogPostUIModel>();
            _blogService.GetAll().ForEach(a =>
            {
                uiModelList.Add(BlogPostUIModel.MapFromEntity(a));
            });

            var groupedList = uiModelList.GroupBy(a => new
            {
                a.ParsedCreateDate.Month,
                a.ParsedCreateDate.Year,
            }).Select(grp => new ArchiveUIModel
            {
                Month = grp.Key.Month,
                Year = grp.Key.Year,
                Count = grp.Count()
            }).ToList();
            return PartialView(groupedList);
        }

        public ActionResult TagsPartial()
        {
            var uiModelList = new List<BlogPostUIModel>();
            _blogService.GetAll().ForEach(a =>
            {
                uiModelList.Add(BlogPostUIModel.MapFromEntity(a));
            });

            var splittedTags = new List<string>();
            uiModelList.Select(a => a.Tags).ForEach(a =>
            {
                var tempVals = a.Split(',');
                tempVals.ForEach(b =>
                {
                    splittedTags.Add(b);
                });
            });
            return PartialView(splittedTags.Distinct().ToList());
        }

        public ActionResult SinglePostTagsPartial(int id)
        {
            var uiModel = BlogPostUIModel.MapFromEntity(_blogService.GetFirstOrDefault(id));

            var splittedTags = new List<string>();
            var tempVals = uiModel.Tags.Split(',');
            tempVals.ForEach(b =>
            {
                splittedTags.Add(b);
            });
            return PartialView(splittedTags.Distinct().ToList());
        }

        public ActionResult FilteredBlogPosts(string tagName, string currentYear, string currentMonth)
        {
            var uiModelList = new List<BlogPostUIModel>();
            if (String.IsNullOrEmpty(currentYear) && String.IsNullOrEmpty(currentMonth))
            {
                //filtered with tag
                _blogService.GetWhere(a => a.Tags.Contains(tagName)).ToList().ForEach(a =>
                {
                    uiModelList.Add(BlogPostUIModel.MapFromEntity(a));
                });
                return View(uiModelList);
            }
            _blogService.GetWhere(GetDateFilter(currentYear, currentMonth)).ToList().ForEach(a =>
            {
                uiModelList.Add(BlogPostUIModel.MapFromEntity(a));
            });
            return View(uiModelList);
        }

        private Func<BlogPost, bool> GetDateFilter(string currentYear, string currentMonth)
        {
            var parsedYear = int.Parse(currentYear);
            var parsedMonth = int.Parse(currentMonth);
            var lastDayOfMonth = DateTime.DaysInMonth(parsedYear, parsedMonth);
            var upLimit = long.Parse(String.Format("{0}0{1}{2}000000", currentYear, currentMonth, lastDayOfMonth));
            var downLimit = long.Parse(String.Format("{0}0{1}01000000", currentYear, currentMonth));

            return (a => a.CreateDate <= upLimit && a.CreateDate >= downLimit);
        }

    }

}
