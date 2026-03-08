using System.Windows.Controls;

namespace JurysBarManagementSystem.Views
{
    public partial class TncView : UserControl
    {
        public TncView()
        {
            InitializeComponent();

            LoadTnc();
        }

        private void LoadTnc()
        {
            TncBox.Text =
@"JURYS BAR MANAGEMENT SYSTEM TERMS AND CONDITIONS

1. System usage is restricted to authorized personnel only.

2. Sales transactions must be properly recorded.

3. Inventory updates should follow operational rules.

4. Unauthorized modification of data is prohibited.

5. All activities are subject to system audit policies.

6. User login credentials must be kept confidential.

7. System administrators may monitor system usage.";
        }
    }
}