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
      Assert.IsNotNull(item, nameof(item));
      Item[] Descendants = item.Axes.GetDescendants();

      //Reset item's BranchId
      item.ResetBranchId();

      //If there are descendants, reset their BranchIds too
      if (Descendants != null)
      {
        foreach (Item d in Descendants)
        {
          d.ResetBranchId();
        }
      }      
    }
  }

  public static class ItemExtenxions
  {
    /// <summary>
    /// If the item's BranchId is not the Null Id, set it to the Null Id
    /// </summary>
    public static void ResetBranchId(this Item item)
    {
      if (item.BranchId != ID.Null)
      {
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
}