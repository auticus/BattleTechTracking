using Xamarin.Forms;

namespace BattleTechTracking.Behaviors
{
    internal class NullableNumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
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
