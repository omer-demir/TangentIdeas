namespace TangentIdeas.Web.Data.Models {
    public abstract class BaseEntity {
        public int Id { get; set; }
        public long CreateDate { get; set; }
        public bool ActiveStatus{ get; set; }
    }
}
