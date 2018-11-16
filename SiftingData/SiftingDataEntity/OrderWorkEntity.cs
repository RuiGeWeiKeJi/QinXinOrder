using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace SiftingDataEntity
{
    public class OrderWorkEntity
    {
        #region Model
        private string _rab001;
        private string _rab002;
        private string _rab003;
        private string _rab004;
        private string _rab005;
        private string _rab006;
        private decimal? _rab007;
        private decimal? _rab008;
        private decimal? _rab009;
        private string _rab010;
        private string _rab011;
        private string _rab012;
        private string _rab013;
        private string _rab014;
        private string _rab015;
        private string _rab016;
        private string _rab017;
        private decimal? _rab018;
        private decimal? _rab019;
        private string _rab020;
        private decimal? _rab021;
        private string _rab901;
        private string _rab902;
        private string _rab903;
        private string _rab904;
        private int? _rab905;
        private string _rab960;
        private string _rab961;
        private string _rab962;
        private string _rab963;
        private string _rab964;
        private string _rab965;
        private decimal? _rab980;
        private decimal? _rab981;
        private decimal? _rab982;
        private decimal? _rab983;
        private decimal? _rab984;
        private decimal? _rab985;

        /// <summary>
        /// 工单单号
        /// </summary>
        public string RAB001
        {
            set
            {
                _rab001 = value;
            }
            get
            {
                return _rab001;
            }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string RAB002
        {
            set
            {
                _rab002 = value;
            }
            get
            {
                return _rab002;
            }
        }
        /// <summary>
        /// 子件品号
        /// </summary>
        public string RAB003
        {
            set
            {
                _rab003 = value;
            }
            get
            {
                return _rab003;
            }
        }
        /// <summary>
        /// 品名(预留)
        /// </summary>
        public string RAB004
        {
            set
            {
                _rab004 = value;
            }
            get
            {
                return _rab004;
            }
        }
        /// <summary>
        /// 单位(预留)
        /// </summary>
        public string RAB005
        {
            set
            {
                _rab005 = value;
            }
            get
            {
                return _rab005;
            }
        }
        /// <summary>
        /// 仓库
        /// </summary>
        public string RAB006
        {
            set
            {
                _rab006 = value;
            }
            get
            {
                return _rab006;
            }
        }
        /// <summary>
        /// 预计用量
        /// </summary>
        public decimal? RAB007
        {
            set
            {
                _rab007 = value;
            }
            get
            {
                return _rab007;
            }
        }
        /// <summary>
        /// 已领料量
        /// </summary>
        public decimal? RAB008
        {
            set
            {
                _rab008 = value;
            }
            get
            {
                return _rab008;
            }
        }
        /// <summary>
        /// 入库耗用量
        /// </summary>
        public decimal? RAB009
        {
            set
            {
                _rab009 = value;
            }
            get
            {
                return _rab009;
            }
        }
        /// <summary>
        /// 结束
        /// </summary>
        public string RAB010
        {
            set
            {
                _rab010 = value;
            }
            get
            {
                return _rab010;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string RAB011
        {
            set
            {
                _rab011 = value;
            }
            get
            {
                return _rab011;
            }
        }
        /// <summary>
        /// 审核码
        /// </summary>
        public string RAB012
        {
            set
            {
                _rab012 = value;
            }
            get
            {
                return _rab012;
            }
        }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string RAB013
        {
            set
            {
                _rab013 = value;
            }
            get
            {
                return _rab013;
            }
        }
        /// <summary>
        /// 可替代
        /// </summary>
        public string RAB014
        {
            set
            {
                _rab014 = value;
            }
            get
            {
                return _rab014;
            }
        }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string RAB015
        {
            set
            {
                _rab015 = value;
            }
            get
            {
                return _rab015;
            }
        }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string RAB016
        {
            set
            {
                _rab016 = value;
            }
            get
            {
                return _rab016;
            }
        }
        /// <summary>
        /// 预留字段
        /// </summary>
        public string RAB017
        {
            set
            {
                _rab017 = value;
            }
            get
            {
                return _rab017;
            }
        }
        /// <summary>
        /// 预留字段
        /// </summary>
        public decimal? RAB018
        {
            set
            {
                _rab018 = value;
            }
            get
            {
                return _rab018;
            }
        }
        /// <summary>
        /// 预留字段
        /// </summary>
        public decimal? RAB019
        {
            set
            {
                _rab019 = value;
            }
            get
            {
                return _rab019;
            }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string RAB020
        {
            set
            {
                _rab020 = value;
            }
            get
            {
                return _rab020;
            }
        }
        /// <summary>
        /// 被替代量
        /// </summary>
        public decimal? RAB021
        {
            set
            {
                _rab021 = value;
            }
            get
            {
                return _rab021;
            }
        }
        /// <summary>
        /// 录入者编号
        /// </summary>
        public string RAB901
        {
            set
            {
                _rab901 = value;
            }
            get
            {
                return _rab901;
            }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public string RAB902
        {
            set
            {
                _rab902 = value;
            }
            get
            {
                return _rab902;
            }
        }
        /// <summary>
        /// 更改者编号
        /// </summary>
        public string RAB903
        {
            set
            {
                _rab903 = value;
            }
            get
            {
                return _rab903;
            }
        }
        /// <summary>
        /// 更改时间
        /// </summary>
        public string RAB904
        {
            set
            {
                _rab904 = value;
            }
            get
            {
                return _rab904;
            }
        }
        /// <summary>
        /// 更新标记
        /// </summary>
        public int? RAB905
        {
            set
            {
                _rab905 = value;
            }
            get
            {
                return _rab905;
            }
        }
        /// <summary>
        /// 自定文字1
        /// </summary>
        public string RAB960
        {
            set
            {
                _rab960 = value;
            }
            get
            {
                return _rab960;
            }
        }
        /// <summary>
        /// 自定文字2
        /// </summary>
        public string RAB961
        {
            set
            {
                _rab961 = value;
            }
            get
            {
                return _rab961;
            }
        }
        /// <summary>
        /// 自定文字3
        /// </summary>
        public string RAB962
        {
            set
            {
                _rab962 = value;
            }
            get
            {
                return _rab962;
            }
        }
        /// <summary>
        /// 自定文字4
        /// </summary>
        public string RAB963
        {
            set
            {
                _rab963 = value;
            }
            get
            {
                return _rab963;
            }
        }
        /// <summary>
        /// 自定文字5
        /// </summary>
        public string RAB964
        {
            set
            {
                _rab964 = value;
            }
            get
            {
                return _rab964;
            }
        }
        /// <summary>
        /// 自定文字6
        /// </summary>
        public string RAB965
        {
            set
            {
                _rab965 = value;
            }
            get
            {
                return _rab965;
            }
        }
        /// <summary>
        /// 自定数字1
        /// </summary>
        public decimal? RAB980
        {
            set
            {
                _rab980 = value;
            }
            get
            {
                return _rab980;
            }
        }
        /// <summary>
        /// 自定数字2
        /// </summary>
        public decimal? RAB981
        {
            set
            {
                _rab981 = value;
            }
            get
            {
                return _rab981;
            }
        }
        /// <summary>
        /// 自定数字3
        /// </summary>
        public decimal? RAB982
        {
            set
            {
                _rab982 = value;
            }
            get
            {
                return _rab982;
            }
        }
        /// <summary>
        /// 自定数字4
        /// </summary>
        public decimal? RAB983
        {
            set
            {
                _rab983 = value;
            }
            get
            {
                return _rab983;
            }
        }
        /// <summary>
        /// 自定数字5
        /// </summary>
        public decimal? RAB984
        {
            set
            {
                _rab984 = value;
            }
            get
            {
                return _rab984;
            }
        }
        /// <summary>
        /// 自定数字6
        /// </summary>
        public decimal? RAB985
        {
            set
            {
                _rab985 = value;
            }
            get
            {
                return _rab985;
            }
        }
        #endregion Model
    }
}
