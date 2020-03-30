using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BYSCORE.UI.TagHelpers
{
    [HtmlTargetElement("AreasSelect", Attributes = DefaultValueAttributeName)]
    public class AreasSelectTagHelper : TagHelper
    {
        private const string DefaultValueAttributeName = "default-value";

        private readonly ConfigInfoService _configInfoService;
        public AreasSelectTagHelper(ConfigInfoService configInfoService)
        {
            _configInfoService = configInfoService;
        }

        [HtmlAttributeName(DefaultValueAttributeName)]
        public int DefaultValue { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Content.Clear();

            IEnumerable<ConfigInfo> configInfos = await _configInfoService.GetListByType((int)ConfigInfoType.Area);
            foreach (var item in configInfos)
            {
                var selected = "";
                if (item.Id == DefaultValue)
                {
                    selected = "selected =\"selected\"";
                }

                var listItem = $"<option value=\"{item.Id}\" {selected}>{item.Name}</option> ";
                output.Content.AppendHtml(listItem);
            }
        }
    }

}
