# Create a custom ASPxGridView filter insensitive to the number of spaces and punctuation


<p>This example shows how to apply a custom filter to the text in the Auto Filter Row editor. </p><p>When the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_ProcessColumnAutoFiltertopic"><u>ASPxGridView.ProcessColumnAutoFilter</u></a> event is raised before the filter criterion is applied, we parse the <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_FilterExpressiontopic"><u>ASPxGridView.FilterExpression</u></a> and take only a custom filter for the current column using the <strong>FindAndRemoveCustomOperator </strong>function.</p><p>In the<strong> MergeCriterias</strong> function, we make a custom criterion and then apply it. </p><p>When the AutoFilterCellEditorInitialize event is raised, we get a custom filter for a certain column from the <strong>FindAndRemoveCustomOperator</strong> function and pass it to the Auto Filter Row editor.</p><p><strong>See also</strong>:<br />
<a href="https://www.devexpress.com/Support/Center/p/E4099">How to register a custom filter function and use it in ASPxGridView</a></p>

<br/>


