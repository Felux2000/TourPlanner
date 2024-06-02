using System.Windows.Forms;

namespace TourPlanner.HelperLayer.Services
{
    public class DialogService
    {
        public DialogResult ShowMessageBox(string message, string title, bool error = false)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, error ? MessageBoxIcon.Error : MessageBoxIcon.Warning);
        }

        public SaveFileDialog GetSaveDialog()
        {
            return new SaveFileDialog();
        }

        public OpenFileDialog GetOpenDialog()
        {
            return new OpenFileDialog();
        }
    }
}
