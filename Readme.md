# How to find a node by values from several columns


<p>The easiest way to find a node is to either use the ID of the underlying node object together with the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraTreeListTreeList_FindNodeByIDtopic">FindNodeByID</a> method or the value of the key field together with the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraTreeListTreeList_FindNodeByKeyIDtopic">FindNodeByKeyID</a> method of the TreeList control.<br />
However, often these identifiers are not available, and you need to identify a node, rather by values from several columns. In this instance, it's a good decision to use the <a href="http://documentation.devexpress.com/#WindowsForms/CustomDocument1472">Nodes Iterator</a>.<br />
This example demonstrates how to find and focus a node by the values from the ProductID and CountryID columns.</p><p><strong>See Also:</strong><br />
<a href="https://www.devexpress.com/Support/Center/p/A236">How to Implement an Iterator for the XtraTreeList (FindNode Example)</a></p>

<br/>


