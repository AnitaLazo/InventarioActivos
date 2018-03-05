using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INVENTARIO.Services.ActivosCatalogApi.ViewModels
{
    public class PaginateItemsViewModel<IEntity> where IEntity:class
    {
        public int PageSize { get; private set; }
        public int PageIndex { get; private set; }
        public long Count { get; private set; }
        public IEnumerable<IEntity> Data { get; set; }
        public PaginateItemsViewModel(int pageIndex, int pagesize, long count, IEnumerable<IEntity> data)
        {
            this.PageSize = pagesize;
            this.PageIndex = pageIndex;
            this.Count = count;
            this.Data = data;
        }
    }
}
