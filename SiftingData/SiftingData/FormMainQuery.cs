using System . Collections . Generic;
using System . Data;
using System . Windows . Forms;

namespace SiftingData
{
    public partial class FormMainQuery :DevExpress . XtraEditors . XtraForm
    {
        SiftingDataBll.Bll.MainBll _bll=null;
        DataTable tableView;
        Dictionary<int,string> dicSpe=new Dictionary<int, string>();
        Dictionary<int,string> dicWare=new Dictionary<int, string>();
        
        public FormMainQuery ( string pinNum ,string text)
        {
            InitializeComponent ( );

            _bll = new SiftingDataBll . Bll . MainBll ( );
            Utility . GridViewMoHuSelect . SetFilter ( gridView1 );
            tableView = _bll . getTablePiHao ( pinNum );
            gridControl1 . DataSource = tableView;

            this . Text = text;
        }
        
        private void gridControl1_KeyPress ( object sender ,KeyPressEventArgs e )
        {
            if ( e . KeyChar == 13 )
            {
                int [ ] selectRows = gridView1 . GetSelectedRows ( );
                if ( selectRows . Length < 1 )
                    return;

                int l = 0;
                foreach ( int k in selectRows )
                {
                    DataRow row = gridView1 . GetDataRow ( k );
                    if ( row != null )
                    {
                        if ( !dicSpe . ContainsKey ( k ) )
                        {
                            dicSpe . Add ( l ,row [ "LOA011" ] . ToString ( ) );
                            dicWare . Add ( l ,row [ "LOA002" ] . ToString ( ) );
                            l++;
                        }
                    }
                }
                if ( dicSpe != null && dicSpe . Count > 0 )
                    this . DialogResult = DialogResult . OK;
            }
        }

        public Dictionary<int ,string> getSpe
        {
            get
            {
                return dicSpe;
            }
        }

        public Dictionary<int ,string> getWare
        {
            get
            {
                return dicWare;
            }
        }

    }
}