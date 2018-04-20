namespace Sitecore.Support
{
  using System;

  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Events;
  using Sitecore.SecurityModel;

  public class EventHandler
  {
    [UsedImplicitly]
    public void OnItemAdded(object sender, EventArgs args)
    {
      var item = Event.ExtractParameter(args, 0) as Item;
      Assert.IsNotNull(item, nameof(item));

      if (item.BranchId == ID.Null)
      {
        return;
      }

      using (new SecurityDisabler())
      {
        using (new EditContext(item, false, false))
        {
          item.BranchId = ID.Null;
        }
      }
    }
  }
}