using System;
using System.Collections.Generic;
using System.Linq;

using DevExpress.Data.Filtering;
using DevExpress.Web.ASPxGridView;

using SearchString;


public partial class SandboxSearchstring : System.Web.UI.Page {
    protected void Page_Init(object sender, EventArgs e) {
        if (CriteriaOperator.GetCustomFunction(EasySearchStatic.Name) == null)
            CriteriaOperator.RegisterCustomFunction(new EasySearchString());
    }
 
    
    protected void gvClients_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e) {
        if (e.Kind != GridViewAutoFilterEventKind.CreateCriteria)
            return;
        CriteriaOperator co = CriteriaOperator.Parse(gvClients.FilterExpression);
        FindAndRemoveCustomOperator(ref co, e.Column.FieldName);

        FunctionOperator fo = null;
        if (e.Value.Length > 0)
            fo = new FunctionOperator(FunctionOperatorType.Custom, EasySearchStatic.Name, e.Value, new OperandProperty(e.Column.FieldName));
        if (!ReferenceEquals(co, null) || !ReferenceEquals(fo, null))
            gvClients.FilterExpression = MergeCriterias(co, fo).ToString();
        else
            gvClients.FilterExpression = "";
        e.Criteria = null;
    }

    protected CriteriaOperator MergeCriterias(CriteriaOperator co, FunctionOperator fo) {
        if (ReferenceEquals(fo, null))
            return co;
        if (ReferenceEquals(co, null))
            return fo;
        var go = co as GroupOperator;
        if (ReferenceEquals(go, null) || go.OperatorType != GroupOperatorType.And)
            return GroupOperator.And(co, fo);
        go.Operands.Add(fo);
        return go;
    }

    protected FunctionOperator FindAndRemoveCustomOperator(ref CriteriaOperator co, string fieldName) {
        var fo = co as FunctionOperator;
        if (IsValidFuncOperator(fo, fieldName)) {
            co = null;
            return fo;
        }
        var go = co as GroupOperator;
        if (ReferenceEquals(go, null))
            return null;
        fo = go.Operands.FirstOrDefault(op => IsValidFuncOperator(op as FunctionOperator, fieldName)) as FunctionOperator;
        if (ReferenceEquals(fo, null))
            return null;
        go.Operands.Remove(fo);
        return fo;
    }

    protected bool IsValidFuncOperator(FunctionOperator fo, string fieldName) {
        if (ReferenceEquals(fo, null) || fo.OperatorType != FunctionOperatorType.Custom || fo.Operands.Count != 3)
            return false;

        var customNameOp = fo.Operands[0] as OperandValue;
        if (ReferenceEquals(customNameOp, null) || customNameOp.Value.ToString() != EasySearchStatic.Name)
            return false;

        var oProp = fo.Operands[2] as OperandProperty;
        if (oProp.PropertyName != fieldName)
            return false;
        return true;
    }

    protected void gvClients_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e) {
        if (string.IsNullOrEmpty(gvClients.FilterExpression) || !gvClients.FilterExpression.Contains(EasySearchStatic.Name) )          
            return;
        var co = CriteriaOperator.Parse(gvClients.FilterExpression);
        var fo = FindAndRemoveCustomOperator(ref co, e.Column.FieldName);
        if (ReferenceEquals(fo, null))
            return;
        var oValue = fo.Operands[1] as OperandValue;
        if (ReferenceEquals(oValue, null))
            return;
        e.Editor.Value = oValue.Value.ToString();
    }

}