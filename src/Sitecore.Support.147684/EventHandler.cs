namespace Sitecore.Support
{
  using System;

  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Events;

  public class EventHandler
  {
    [UsedImplicitly]
    public void OnItemAdded(object sender, EventArgs args)
    {
      var item = Event.ExtractParameter(args, 0) as Item;
      Assert.IsNotNull(item, nameof(item));
    }
  }
}