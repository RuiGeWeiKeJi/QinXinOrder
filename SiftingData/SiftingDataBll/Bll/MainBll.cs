using System . Collections . Generic;
using System . Data;

namespace SiftingDataBll . Bll
{
    public class MainBll
    {
        private readonly Dao.MainDao dal=null;

        public MainBll ( )
        {
            dal = new Dao . MainDao ( );
        }

        /// <summary>
        /// 获取单头数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableQuery ( string strWhere )
        {
            return dal . getTableQuery ( strWhere );
        }

        /// <summary>
        /// 获取主件品号信息
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public DataTable getTableQueryPin ( string orderNum ,string strWhere)
        {
            return dal . getTableQueryPin ( orderNum ,strWhere );
        }

        /// <summary>
        /// 获取品号
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePNum ( )
        {
            return dal . getTablePNum ( );
        }

        /// <summary>
        /// 获取子表
        /// </summary>
        /// <param name="gdNum"></param>
        /// <returns></returns>
        public DataTable getTableView ( string gdNum )
        {
            return dal . getTableView ( gdNum );
        }

        /// <summary>
        /// 根据子件品号获取批号
        /// </summary>
        /// <param name="pNum"></param>
        /// <returns></returns>
        public DataTable getTablePiHao ( string pNum )
        {
            return dal . getTablePiHao ( pNum );
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oderNum"></param>
        /// <returns></returns>
        public bool Save ( DataTable table  ,string remark,string orderNum,string num,string piNum,string numOfOrder)
        {
            return dal . Save ( table ,remark ,orderNum ,num ,piNum ,numOfOrder );
        }

        /// <summary>
        /// 更改
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oderNum"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Update ( DataTable table ,string oderNum ,List<int> idxList ,string remark,string oddNum ,string num,string piNum ,string numOfOrder )
        {
            return dal . Update ( table ,oderNum ,idxList ,remark ,oddNum ,num ,piNum,numOfOrder );
        }

        /// <summary>
        /// 工单是否审核
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public bool ExistsSH ( string orderNum )
        {
            return dal . ExistsSH ( orderNum );
        }

        /// <summary>
        /// 是否已经生成JSKLIA  JSKLIB
        /// </summary>
        /// <returns></returns>
        public bool ExistsJSK ( string orderNum ,string proName )
        {
            return dal . ExistsJSK ( orderNum ,proName );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public bool Delete ( string orderNum ,string orderNums,string num)
        {
            return dal . Delete ( orderNum ,orderNums ,num );
        }

        /// <summary>
        /// 插入工单单身数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int SaveOrder ( string oddNum )
        {
            return dal . SaveOrder ( oddNum );
        }

        /// <summary>
        /// 生成领料单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int SavePick ( string oddNum )
        {
            return dal . SavePick ( oddNum );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            return dal . getTablePrintOne ( oddNum );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum )
        {
            return dal . getTablePrintTwo ( oddNum );
        }

        /// <summary>
        /// 是否LIA961='T'  如果是false  则不可以反审核
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public bool Exists ( string orderNum ,string piNum )
        {
            return dal . Exists ( orderNum ,piNum );
        }

        /// <summary>
        /// 审核  反审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Examine ( SiftingDataEntity . MainEntity model )
        {
            return dal . Examine ( model );
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public DataTable getOddTable ( )
        {
            return dal . getOddTable ( );
        }

        /// <summary>
        /// 得到单号
        /// </summary>
        /// <returns></returns>
        public string getOdd ( )
        {
            return dal . getOdd ( );
        }

    }
}
