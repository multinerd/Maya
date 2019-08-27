using System.Threading.Tasks;
using Maya.AspNetCore.TagHelpers.Core.Attributes.Controls;
using Maya.AspNetCore.TagHelpers.Core.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Extensions;
using Maya.AspNetCore.TagHelpers.Metronic.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json.Linq;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Forms
{
    [GenerateId("dropzone_", true)]
    [HtmlTargetElement("dropzone", TagStructure = TagStructure.WithoutEndTag)]
    [OutputElementHint("div")]
    public class DropzoneTagHelper : MeconsFormTagHelperBase
    {
        private const string UrlAttributeName = "bc-url";

        private const string TitleAttributeName = "bc-title";

        private const string DescriptionAttributeName = "bc-desc";

        private const string MaxFileSizeAttributeName = "bc-max-file-size";

        private const string MaxFilesAttributeName = "bc-max-files";

        private const string ClickableAttributeName = "bc-clickable";

        private const string AcceptedFilesAttributeName = "bc-accepted";

        private const string RemoveFilesAttributeName = "bc-remove-files";

        private const string ColorAttributeName = "bc-color";

        [HtmlAttributeName("bc-accepted")]
        public string AcceptedFiles { get; set; }

        [HtmlAttributeName("bc-color")]
        public Color Color { get; set; } = Color.Secondary;

        [HtmlAttributeName("bc-desc")]
        public string Description
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-clickable")]
        public bool IsClickable { get; set; } = true;

        [HtmlAttributeName("bc-max-files")]
        public int MaxFiles
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-max-file-size")]
        public int MaxFileSize { get; set; } = -1;

        [HtmlAttributeName("bc-remove-files")]
        public bool RemoveFiles
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-title")]
        public string Title
        {
            get;
            set;
        }

        [HtmlAttributeName("bc-url")]
        [Mandatory]
        public string Url
        {
            get;
            set;
        }

        public DropzoneTagHelper()
        {
        }

        private TagBuilder BuildMessage()
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("m-dropzone__msg dz-message needsclick");
            if (string.IsNullOrEmpty(this.Title))
            {
                tagBuilder.InnerHtml.AppendHtml(string.Concat("<h3 class=\"m-dropzone__msg-title\">", (this.IsClickable ? Resources.Dropzone_Title_Clickable : Resources.Dropzone_Title_NotClickable), "</h3>"));
            }
            else
            {
                tagBuilder.InnerHtml.AppendHtml(string.Concat("<h3 class=\"m-dropzone__msg-title\">", this.Title, "</h3>"));
            }
            if (!string.IsNullOrEmpty(this.Description))
            {
                tagBuilder.InnerHtml.AppendHtml(string.Concat("<span class=\"m-dropzone__msg-desc\">", this.Description, "</span>"));
            }
            return tagBuilder;
        }

        private void ProcessJavascript(TagHelperOutput output)
        {
            dynamic jObjects = new JObject();
            jObjects.url = this.Url;
            jObjects.paramName = (!string.IsNullOrEmpty(base.Name) ? base.Name : "file");
            jObjects.clickable = this.IsClickable;
            jObjects.addRemoveLinks = this.RemoveFiles;
            if (this.MaxFileSize > 0)
            {
                jObjects.maxFilesize = this.MaxFileSize;
            }
            if (this.MaxFiles > 0)
            {
                jObjects.maxFiles = this.MaxFiles;
            }
            if (!string.IsNullOrEmpty(this.AcceptedFiles))
            {
                jObjects.acceptedFiles = this.AcceptedFiles;
            }
            var str = string.Format("<script type='text/javascript'>Dropzone.autoDiscover = false; var {0}; $(function() {{ {1} = new Dropzone('#{2}', {3}); }});</script>", new object[] { base.Id, base.Id, base.Id, jObjects });
            output.PostElement.AppendHtml(str);
        }

        protected override async Task RenderProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.RenderProcessAsync(context, output);

            string str;
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddCssClass("m-dropzone dropzone");
            var tagHelperOutput = output;
            str = (this.Color != Color.None ? string.Concat("m-dropzone--", this.Color.GetColorInfo().Name) : string.Empty);
            tagHelperOutput.AddCssClass(str);
            output.Content.AppendHtml(this.BuildMessage());
            this.ProcessJavascript(output);
        }
    }
}