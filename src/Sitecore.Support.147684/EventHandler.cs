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
      Item item = Event.ExtractParameter(args, 0) as Item;
      Assert.IsNotNull(item, "item");
      if (item.BranchId == ID.Null) 
      {
        return;
      }

      Item[] ItemsArr = item.Axes.GetDescendants();

      if (ItemsArr != null)
      {
        foreach (Item itm in ItemsArr)
        {
          if (itm.BranchId != ID.Null)
          {
            using (new SecurityDisabler())
            {
              using (new EditContext(itm, false, false))
              {
                itm.BranchId = ID.Null;
              }
            }
          }
        }
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