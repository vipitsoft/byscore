using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BYSCORE.UI.TagHelpers
{

    [HtmlTargetElement("DepartmentSelect", Attributes = DefaultValueAttributeName)]
    [HtmlTargetElement("DepartmentSelect", Attributes = DefaultNameAttributeName)]
    public class DepartmentSelectTagHelper : TagHelper
    {
        private const string DefaultValueAttributeName = "default-value";
        private const string DefaultNameAttributeName = "default-name";

        private readonly ConfigInfoService _configInfoService;
        public DepartmentSelectTagHelper(ConfigInfoService configInfoService)
        {
            _configInfoService = configInfoService;
        }

        [HtmlAttributeName(DefaultValueAttributeName)]
        public int DefaultValue { get; set; }

        [HtmlAttributeName(DefaultNameAttributeName)]
        public string DefaultName { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Content.Clear();

            IEnumerable<ConfigInfo> configInfos = await _configInfoService.GetListByType((int)ConfigInfoType.Department);

            if (DefaultName.IsNotNullOrEmpty())
            {
                output.Content.AppendHtml(string.Format("<option value=\"{0}\">{1}</option>", 0, DefaultName));
            }

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

