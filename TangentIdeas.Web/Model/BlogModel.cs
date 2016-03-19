using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TangentIdeas.Web.Model {
    public class BlogModel {
        public string Subject { get; set; }
        public string ShortDescription { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }
}