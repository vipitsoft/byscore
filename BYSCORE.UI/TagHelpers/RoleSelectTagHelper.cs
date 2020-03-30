using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BYSCORE.UI.TagHelpers
{
    [HtmlTargetElement("RoleSelect", Attributes = DefaultValueAttributeName)]
    public class RoleSelectTagHelper : TagHelper
    {
        private const string DefaultValueAttributeName = "default-value";

        private readonly RoleService _roleService;
        public RoleSelectTagHelper(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HtmlAttributeName(DefaultValueAttributeName)]
        public int DefaultValue { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Content.Clear();

            IEnumerable<Role> roles = await _roleService.GetList();
            foreach (var item in roles)
            {
                var selected = "";
                if (item.Id == DefaultValue)
                {
                    selected = "selected =\"selected\"";
                }

                var listItem = $"<option value=\"{item.Id}\" {selected}>{item.CName}</option> ";
                output.Content.AppendHtml(listItem);
            }
        }
    }
}
