using System;
using System . Collections . Generic;
using System . Text;
using DevExpress . XtraGrid . Columns;
using DevExpress . XtraEditors;

namespace Utility
{
    public class GridViewMoHuSelect
    {
        /// <summary>
        /// 设置girid为每一列都模糊搜索
        /// </summary>
        /// <param name="gdv"></param>
        public static void SetFilter ( DevExpress . XtraGrid . Views . Grid . GridView gdv )
        {
            gdv . OptionsView . ShowAutoFilterRow = true;
            //gdv.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = true;
            foreach ( DevExpress . XtraGrid . Columns . GridColumn item in gdv . Columns )
            {
                item . OptionsFilter . AutoFilterCondition = DevExpress . XtraGrid . Columns . AutoFilterCondition . Contains;   //筛选条件设置为包含  
                item . OptionsFilter . FilterPopupMode = FilterPopupMode . CheckedList;//设置为过滤是可以多选
            }
        }



    }
}
