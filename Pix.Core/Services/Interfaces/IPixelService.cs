using Pix.Core.Models;
using System.Threading.Tasks;

namespace Pix.Core.Services.Interfaces
{
    public interface IPixelService
    {
        (int, int) Initialize(string file);
        void SaveBitmap(string file);
        Task<int[]> GetPixelizedStream(int blockSize, bool forceAllColors, bool orderByLuminance, ColorModel[] palette);
    }
}
