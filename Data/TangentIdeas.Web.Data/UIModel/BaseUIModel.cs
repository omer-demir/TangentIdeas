using System;

namespace TangentIdeas.Web.Data.UIModel {
    public abstract class BaseUIModel {
        public int Id { get; set; }
        public string CreateDate { get; set; }
        public DateTime ParsedCreateDate { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
