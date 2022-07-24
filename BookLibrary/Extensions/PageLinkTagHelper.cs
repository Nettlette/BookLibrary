using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using BookLibrary.Models;
using BookLibrary.Pages.BooksRead;

namespace BookLibrary.Extensions
{
    [HtmlTargetElement("div", Attributes="search-view-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public Pages.BooksRead.IndexModel BooksReadViewModel { get; set; }
        public string PageName { get; set; }
        public string PageAction { get; set; }
        public string PageController { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public PagingInfo PagingModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            int iIndex = 1;
            int iEndIndex = 1;

            if(PagingModel.CurrentPage > 1)
            {
                result.InnerHtml.AppendHtml(GenerateTag(1, "<<"));
                result.InnerHtml.AppendHtml(GenerateTag(PagingModel.CurrentPage - 1, "<"));
            }
            iIndex = Math.Max(PagingModel.CurrentPage - 2, 1);
            iEndIndex = Math.Min(PagingModel.CurrentPage + 2, PagingModel.TotalPages);

            for (int i=iIndex; i<=iEndIndex; i++)
            {
                result.InnerHtml.AppendHtml(GenerateTag(i, i.ToString()));
            }

            if (PagingModel.CurrentPage < PagingModel.TotalPages)
            {
                result.InnerHtml.AppendHtml(GenerateTag(PagingModel.CurrentPage + 1, ">"));
                result.InnerHtml.AppendHtml(GenerateTag(PagingModel.TotalPages, ">>"));
            }
            output.Content.AppendHtml(result.InnerHtml);
        }

        private TagBuilder GenerateTag(int i, string sText)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.Attributes["href"] = "/" + PageController;
            if(PageAction != null)
            {
                tag.Attributes["href"] += "/" + PageAction;
            }
            tag.Attributes["href"] = "?" + PageName + "=" + i;
            if (!String.IsNullOrEmpty(PagingModel.Params))
            {
                tag.Attributes["href"] += PagingModel.Params;
            }

            if (PageClassesEnabled)
            {
                tag.AddCssClass(PageClass);
                if (i == PagingModel.CurrentPage && sText == i.ToString())
                    tag.AddCssClass(PageClassSelected);
                else
                    tag.AddCssClass(PageClassNormal);
            }
            tag.InnerHtml.Append(sText);
            return tag;
        }
    }
}
