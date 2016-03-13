using System.Web;
using System.Web.Mvc;

namespace TangentIdeas.Web.Blog.Helpers {
    public static class HtmlHelpers {
        public static IHtmlString MonthWithString(this HtmlHelper helper, int month) {
            string monthName = "";
            switch (month) {
                case 1:
                    monthName = "Ocak";
                    break;
                case 2:
                    monthName = "Şub";
                    break;
                case 3:
                    monthName = "Mart";
                    break;
                case 4:
                    monthName = "Nisa";
                    break;
                case 5:
                    monthName = "May";
                    break;
                case 6:
                    monthName = "Haz";
                    break;
                case 7:
                    monthName = "Tem";
                    break;
                case 8:
                    monthName = "Ağu";
                    break;
                case 9:
                    monthName = "Eylül";
                    break;
                case 10:
                    monthName = "Ekim";
                    break;
                case 11:
                    monthName = "Kas";
                    break;
                case 12:
                    monthName = "Aralık";
                    break;

            }
            return new HtmlString(monthName);
        }
    }
}