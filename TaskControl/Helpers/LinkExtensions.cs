using System.Web.Mvc;

namespace TaskControl.Helpers
{
    public static class LinkExtensions
    {
        /// <summary>
        /// Extension method to do image link
        /// </summary>
        /// <param name="html"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="imagePath"></param>
        /// <param name="width"></param>
        /// <param name="alt"></param>
        /// <returns></returns>
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, string controller, string imagePath, string width, string alt, string title)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("title", title);
            imgBuilder.MergeAttribute("alt", alt);
            imgBuilder.MergeAttribute("width", width + "px");
            
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, controller));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }
    }
}