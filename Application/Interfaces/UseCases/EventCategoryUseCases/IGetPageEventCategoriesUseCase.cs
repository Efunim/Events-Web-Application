using Events.Application.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Interfaces.UseCases
{
    public interface IGetPageEventCategoriesUseCase
    {
        public Task<IEnumerable<EventCategoryResponseDto>> ExecuteAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
