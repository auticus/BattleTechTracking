using System;
using Xamarin.Forms;

namespace BattleTechTracking.Behaviors
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            entry.Unfocused += EntryOnUnfocused;
            base.OnAttachedTo(entry);
        }

        private void EntryOnUnfocused(object sender, FocusEventArgs e)
        {
            var textField = (Entry)sender;
            if (string.IsNullOrEmpty(textField.Text))
            {
                textField.Text = "0";
            }
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(args.NewTextValue)) return;

            double result;
            bool valid = double.TryParse(args.NewTextValue, out result);
            if (!valid)
            {
                ((Entry)sender).Text = args.OldTextValue;
            }
        }
    }
}
