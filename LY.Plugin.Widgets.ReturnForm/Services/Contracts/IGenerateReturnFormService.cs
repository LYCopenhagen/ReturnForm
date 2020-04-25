using Nop.Core.Domain.Orders;
using System.IO;

namespace LY.Plugin.Widgets.ReturnForm.Services.Contracts
{
    public interface IGenerateReturnFormService
    {
        void CreateDocument(Order order, in MemoryStream stream);
    }
}
