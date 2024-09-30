using Learning.Domain.DTO.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Domain.DTO.Permission
{
    public class FilterMasterDTO : BasePaging
    {
        #region properies
        public string Search { get; set; }
        public List<MastersDTO> MastersDTOs{ get; set; }
        #endregion

        #region methoods
        public FilterMasterDTO SetMasters(List<MastersDTO> mastersDTOs)
        {
            this.MastersDTOs = mastersDTOs;
            return this;
        }
        public FilterMasterDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;
            return this;
        }
        #endregion

    }
}
