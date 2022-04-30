using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BattleTechTracking.Models
{
    public class GroupedGameElement : ObservableCollection<IDisplayMatchedListView>
    {
        public string Key { get; private set; }
        public EventHandler Invalidated { get; set; }
        public IList<IDisplayMatchedListView> GameElements
        {
            get => this.Items;
            set
            {
                Items.Clear();
                foreach (var item in value)
                {
                    Items.Add(item);
                }
            }
        }

        public GroupedGameElement(string unitAction, IEnumerable<IDisplayMatchedListView> source)
        {
            Key = unitAction;
            foreach (var item in source)
            {
                item.Invalidated += (sender, args) => Invalidated?.Invoke(this, args);
                this.Items.Add(item);
            }
        }
    }
}
