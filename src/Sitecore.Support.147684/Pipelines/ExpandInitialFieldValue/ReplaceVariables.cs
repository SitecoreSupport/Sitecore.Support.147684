using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.ExpandInitialFieldValue;
using System.Reflection;

namespace Sitecore.Support.Pipelines.ExpandInitialFieldValue
{
  public class ReplaceVariables : ExpandInitialFieldValueProcessor
  {
    public override void Process(ExpandInitialFieldValueArgs args)
    {
      Assert.ArgumentNotNull(args, "args");
      MasterVariablesReplacer masterVariablesReplacer = Factory.GetMasterVariablesReplacer();

      string text = args.SourceField.GetValue(true, true, true);

      if (args.SourceField.SharedLanguageFallbackEnabled)
      {
        var fieldItem = args.SourceField.GetType().GetField("item", BindingFlags.Instance | BindingFlags.NonPublic);
        fieldItem.SetValue(args.SourceField, args.TargetItem);
      }

      if (masterVariablesReplacer == null)
      {
        args.Result = text;
      }
      else
      {
        args.Result = masterVariablesReplacer.Replace(text, args.TargetItem);
      }
    }
  }
}