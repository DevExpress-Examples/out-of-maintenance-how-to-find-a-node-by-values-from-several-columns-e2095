using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Data;
using DevExpress.XtraEditors;

namespace WindowsApplication1 {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("ParentID", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("CountryID", typeof(string));

            dt.Rows.Add(new object[] { 1, DBNull.Value, 0, "us" });
            dt.Rows.Add(new object[] { 2, 1, 1, "us" });
            dt.Rows.Add(new object[] { 3, 1, 1, "uk" });
            dt.Rows.Add(new object[] { 4, 1, 2, "us" });
            dt.Rows.Add(new object[] { 5, 1, 2, "uk" });
            dt.Rows.Add(new object[] { 6, 1, 3, "ie" });
            dt.Rows.Add(new object[] { 7, DBNull.Value, 1, "uk" });
            dt.Rows.Add(new object[] { 8, 7, 4, "us" });
            dt.Rows.Add(new object[] { 9, 7, 4, "uk" });
            dt.Rows.Add(new object[] { 10, 7, 5, "us" });
            dt.Rows.Add(new object[] { 11, 7, 5, "uk" });
            dt.Rows.Add(new object[] { 12, 7, 5, "ie" });

            treeList1.KeyFieldName = "ID";
            treeList1.ParentFieldName = "ParentID";
            treeList1.DataSource = dt.DefaultView;
        }
        private void simpleButton1_Click(object sender, EventArgs e) {
            int productID    = Convert.ToInt32(spinEdit1.EditValue);
            string countryID = textEdit1.Text;
            //Solution1
            //int ID = 10;
            //treeList1.FocusedNode = treeList1.FindNodeByKeyID(ID);

            //Solution2
            TreeListOperationFindNodeByProductAndCountryValues findNode = new TreeListOperationFindNodeByProductAndCountryValues(productID, countryID);
            treeList1.NodesIterator.DoOperation(findNode);
            treeList1.FocusedNode = findNode.Node;
        }
    }
    public class TreeListOperationFindNodeByProductAndCountryValues : TreeListOperation {
        public const string ProductIDColumnName = "ProductID";
        public const string CountryIDColumnName = "CountryID";
        private TreeListNode nodeCore;
        private object productIDCore;
        private object countryIDCore;
        private bool isNullCore;
        public TreeListOperationFindNodeByProductAndCountryValues(object productID, object countryID) {
            this.productIDCore = productID;
            this.countryIDCore = countryID;
            this.nodeCore = null;
            this.isNullCore = TreeListData.IsNull(productIDCore) || TreeListData.IsNull(countryIDCore);
        }
        public override void Execute(TreeListNode node) {
            if (IsLookedFor(node.GetValue(ProductIDColumnName), node.GetValue(CountryIDColumnName)))
                this.nodeCore = node;
        }
        bool IsLookedFor(object productID, object countryID) {
            if (IsNull) return (productIDCore == productID && countryIDCore == countryID);
            return productIDCore.Equals(productID) && countryIDCore.Equals(countryID);
        }
        protected bool IsNull { get { return isNullCore; } }
        public override bool CanContinueIteration(TreeListNode node) { return Node == null; }
        public TreeListNode Node { get { return nodeCore; } }
    }
}