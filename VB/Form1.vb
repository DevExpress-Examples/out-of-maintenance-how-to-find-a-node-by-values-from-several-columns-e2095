Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList.Nodes.Operations
Imports DevExpress.XtraTreeList.Nodes
Imports DevExpress.XtraTreeList.Data
Imports DevExpress.XtraEditors

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()
		End Sub
		Protected Overrides Sub OnLoad(ByVal e As EventArgs)
			MyBase.OnLoad(e)
			Dim dt As New DataTable()
			dt.Columns.Add("ID", GetType(Integer))
			dt.Columns.Add("ParentID", GetType(Integer))
			dt.Columns.Add("ProductID", GetType(Integer))
			dt.Columns.Add("CountryID", GetType(String))

			dt.Rows.Add(New Object() { 1, DBNull.Value, 0, "us" })
			dt.Rows.Add(New Object() { 2, 1, 1, "us" })
			dt.Rows.Add(New Object() { 3, 1, 1, "uk" })
			dt.Rows.Add(New Object() { 4, 1, 2, "us" })
			dt.Rows.Add(New Object() { 5, 1, 2, "uk" })
			dt.Rows.Add(New Object() { 6, 1, 3, "ie" })
			dt.Rows.Add(New Object() { 7, DBNull.Value, 1, "uk" })
			dt.Rows.Add(New Object() { 8, 7, 4, "us" })
			dt.Rows.Add(New Object() { 9, 7, 4, "uk" })
			dt.Rows.Add(New Object() { 10, 7, 5, "us" })
			dt.Rows.Add(New Object() { 11, 7, 5, "uk" })
			dt.Rows.Add(New Object() { 12, 7, 5, "ie" })

			treeList1.KeyFieldName = "ID"
			treeList1.ParentFieldName = "ParentID"
			treeList1.DataSource = dt.DefaultView
		End Sub
		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			Dim productID As Integer = Convert.ToInt32(spinEdit1.EditValue)
			Dim countryID As String = textEdit1.Text
			'Solution1
			'int ID = 10;
			'treeList1.FocusedNode = treeList1.FindNodeByKeyID(ID);

			'Solution2
			Dim findNode As New TreeListOperationFindNodeByProductAndCountryValues(productID, countryID)
			treeList1.NodesIterator.DoOperation(findNode)
			treeList1.FocusedNode = findNode.Node
		End Sub
	End Class
	Public Class TreeListOperationFindNodeByProductAndCountryValues
		Inherits TreeListOperation
		Public Const ProductIDColumnName As String = "ProductID"
		Public Const CountryIDColumnName As String = "CountryID"
		Private nodeCore As TreeListNode
		Private productIDCore As Object
		Private countryIDCore As Object
		Private isNullCore As Boolean
		Public Sub New(ByVal productID As Object, ByVal countryID As Object)
			Me.productIDCore = productID
			Me.countryIDCore = countryID
			Me.nodeCore = Nothing
			Me.isNullCore = TreeListData.IsNull(productIDCore) OrElse TreeListData.IsNull(countryIDCore)
		End Sub
		Public Overrides Sub Execute(ByVal node As TreeListNode)
			If IsLookedFor(node.GetValue(ProductIDColumnName), node.GetValue(CountryIDColumnName)) Then
				Me.nodeCore = node
			End If
		End Sub
		Private Function IsLookedFor(ByVal productID As Object, ByVal countryID As Object) As Boolean
			If IsNull Then
				Return (productIDCore Is productID AndAlso countryIDCore Is countryID)
			End If
			Return productIDCore.Equals(productID) AndAlso countryIDCore.Equals(countryID)
		End Function
		Protected ReadOnly Property IsNull() As Boolean
			Get
				Return isNullCore
			End Get
		End Property
		Public Overrides Function CanContinueIteration(ByVal node As TreeListNode) As Boolean
			Return Me.Node Is Nothing
		End Function
		Public ReadOnly Property Node() As TreeListNode
			Get
				Return nodeCore
			End Get
		End Property
	End Class
End Namespace