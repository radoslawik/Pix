using System.Threading.Tasks;

namespace Pix.Core.Services.Interfaces
{
    public interface IDialogService
    {
        Task<string> ShowOpenFileDialog(string title, string directory);
    }
}
