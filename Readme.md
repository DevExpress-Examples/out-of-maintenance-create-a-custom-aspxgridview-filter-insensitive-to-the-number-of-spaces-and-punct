<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128537072/13.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E4836)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [EasySearchString.cs](./CS/WebSite/App_Code/EasySearchString.cs) (VB: [EasySearchString.vb](./VB/WebSite/App_Code/EasySearchString.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# Create a custom ASPxGridView filter insensitive to the number of spaces and punctuation
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e4836/)**
<!-- run online end -->


<p>This example shows how to apply a custom filter to the text in the Auto Filter Row editor. </p><p>When the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_ProcessColumnAutoFiltertopic"><u>ASPxGridView.ProcessColumnAutoFilter</u></a> event is raised before the filter criterion is applied, we parse the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_FilterExpressiontopic"><u>ASPxGridView.FilterExpression</u></a> and take only a custom filter for the current column using the <strong>FindAndRemoveCustomOperator </strong>function.</p><p>In the<strong> MergeCriterias</strong> function, we make a custom criterion and then apply it. </p><p>When the AutoFilterCellEditorInitialize event is raised, we get a custom filter for a certain column from the <strong>FindAndRemoveCustomOperator</strong> function and pass it to the Auto Filter Row editor.</p><p><strong>See also</strong>:<br />
<a href="https://www.devexpress.com/Support/Center/p/E4099">How to register a custom filter function and use it in ASPxGridView</a></p>

<br/>


