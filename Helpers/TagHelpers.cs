using Microsoft.AspNetCore.Razor.TagHelpers;

namespace recipe_tracker.Helpers;

public class ModelListBinderTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var model = context.AllAttributes["for"].Value;
        var field = context.AllAttributes["field"].Value;
        var row = context.AllAttributes["row"].Value;

        output.Attributes.RemoveAll("for");
        output.Attributes.RemoveAll("field");
        output.Attributes.RemoveAll("row");

        output.TagName = "input";
        output.Attributes.SetAttribute("name", $"{model}[{row}].{field}");
    }
}